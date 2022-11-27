using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public GameObject mazeMarkerPrefab;
    public GameObject mazeWallPrefab;
    public RectTransform mazeZone;
    private HashSet<Vector3> markerPositions = new HashSet<Vector3>();
    private Vector2 markerSize;
    private bool solved = false;
    private AudioSource audioSource;
    public AudioClip solveClip;
    public AudioClip closeClip;
    private MusicManager musicManager;
    private const float hideY = -400;
    private const float showY = 0;
    private const float showTime = 0.75f;
    private const float deleteWaitTime = 2;
    [HideInInspector]
    public Boat boat;
    private Player player;

    private int mazeSize;
    private int cellSize;
    private int mazeCells;
    private Vector2 halfCellUp;
    private Vector2 halfCellRight;
    private enum Direction
    {
        N = 1,
        S = 2,
        E = 4,
        W = 8,
    }

    private void Start()
    {
        StartCoroutine(ScrollIn());
        audioSource = GetComponent<AudioSource>();
        musicManager = FindObjectOfType<MusicManager>();
        musicManager.SetMusicOverride(musicManager.shopMusicSrc);
        player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.paused = true;
        }

        markerSize = mazeMarkerPrefab.GetComponent<BoxCollider2D>().size;
        mazeSize = (int)mazeZone.rect.size.x;
        cellSize = (int)mazeWallPrefab.GetComponent<BoxCollider2D>().size.y;
        mazeCells = mazeSize / cellSize;
        halfCellUp = new Vector2(0, cellSize / 2);
        halfCellRight = new Vector2(cellSize / 2, 0);
        GenerateMaze();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 position = Input.mousePosition;
            if (!markerPositions.Contains(position))
            {
                Collider2D[] colls = Physics2D.OverlapBoxAll(position, markerSize, 0, LayerMask.GetMask("UI"));
                foreach(Collider2D coll in colls)
                {
                    if (coll.gameObject.CompareTag("MazePaper"))
                    {
                        Collider2D wall = Physics2D.OverlapBox(position, markerSize, 0, LayerMask.GetMask("MazeWall"));
                        if (wall == null)
                        {
                            MazeMarker mazeMarker = Instantiate(mazeMarkerPrefab, position, Quaternion.identity, transform).GetComponent<MazeMarker>();
                            mazeMarker.maze = this;
                            markerPositions.Add(position);
                        }
                        break;
                    }
                }
            }
        }
    }

    private void GenerateMaze()
    {
        //Based on https://weblog.jamisbuck.org/2010/12/27/maze-generation-recursive-backtracking
        int[][] cells = new int[mazeCells][];
        for (int i = 0; i < mazeCells; i++)
        {
            cells[i] = new int[mazeCells];
        }
        CarvePath(0, mazeCells / 2, cells);
        cells[mazeCells / 2][0] |= (int)Direction.W;
        cells[mazeCells / 2][mazeCells - 1] |= (int)Direction.E;

        Direction[] directions = (Direction[])Enum.GetValues(typeof(Direction));
        for (int y = 0; y < mazeCells; y++)
        {
            for (int x = 0; x < mazeCells; x++)
            {
                int cell = cells[y][x];
                foreach (Direction dir in directions)
                {
                    if ((cell & (int)dir) == 0)
                    {
                        PlaceWall(CellPos(x, y) + new Vector2(cellSize / 2 * DirX(dir), cellSize / 2 * DirY(dir)), DirY(dir) != 0);
                    }
                }
            }
        }
    }

    private void CarvePath(int x, int y, int[][] cells)
    {
        Direction[] directions = (Direction[])Enum.GetValues(typeof(Direction));
        Shuffle(directions);
        foreach (Direction dir in directions)
        {
            int nx = x + DirX(dir);
            int ny = y + DirY(dir);
            if (nx >= 0 && nx < mazeCells && ny >= 0 && ny < mazeCells && cells[ny][nx] == 0)
            {
                cells[y][x] |= (int)dir;
                cells[ny][nx] |= (int)Opposite(dir);
                CarvePath(nx, ny, cells);
            }
        }
    }

    private Direction Opposite(Direction dir)
    {
        return dir switch
        {
            Direction.N => Direction.S,
            Direction.S => Direction.N,
            Direction.E => Direction.W,
            Direction.W => Direction.E,
            _ => Direction.N,
        };
    }

    private int DirX(Direction dir)
    {
        return dir switch
        {
            Direction.N => 0,
            Direction.S => 0,
            Direction.E => 1,
            Direction.W => -1,
            _ => 0,
        };
    }

    private int DirY(Direction dir)
    {
        return dir switch
        {
            Direction.N => 1,
            Direction.S => -1,
            Direction.E => 0,
            Direction.W => 0,
            _ => 0,
        };
    }

    private void Shuffle<T>(T[] ts)
    {
        //from https://forum.unity.com/threads/clever-way-to-shuffle-a-list-t-in-one-line-of-c-code.241052/#post-1596795
        int count = ts.Length;
        int last = count - 1;
        for (int i = 0; i < last; i++)
        {
            int r = UnityEngine.Random.Range(i, count);
            T tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }

    private Vector2 CellPos(int x, int y)
    {
        return new Vector2(cellSize * (x - mazeCells / 2.0f), cellSize * (y - mazeCells / 2.0f)) + halfCellUp + halfCellRight;
    }

    private void PlaceWall(Vector2 position, bool horizontal)
    {
        GameObject wall = Instantiate(mazeWallPrefab, mazeZone);
        wall.transform.localPosition = position;
        if (horizontal)
        {
            wall.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }

    public void Solve()
    {
        if (solved) return;
        solved = true;
        if (boat != null) boat.Restore();
        Close(true);
    }

    public void Cancel()
    {
        print("CANCEL");
        Close(false);
    }

    private void Close(bool success)
    {
        musicManager.StopMusicOverride();
        audioSource.PlayOneShot(success ? solveClip : closeClip);
        if (player != null)
        {
            player.paused = false;
        }
        StartCoroutine(ScrollOut());
    }

    private IEnumerator ScrollOut()
    {
        for (float t = 0; t < showTime; t += Time.deltaTime)
        {
            float y = Mathf.Lerp(showY, hideY, t / showTime);
            transform.position = new Vector2(transform.position.x, y);
            yield return null;
        }
        transform.position = new Vector2(transform.position.x, hideY);
        yield return new WaitForSeconds(deleteWaitTime);
        Destroy(gameObject);
    }

    private IEnumerator ScrollIn()
    {
        RectTransform transform = GetComponent<RectTransform>();
        transform.anchoredPosition = new Vector2(transform.anchoredPosition.x, hideY);
        for (float t = 0; t < showTime; t += Time.deltaTime)
        {
            float y = Mathf.Lerp(hideY, showY, t / showTime);
            transform.anchoredPosition = new Vector2(transform.anchoredPosition.x, y);
            yield return null;
        }
        transform.anchoredPosition = new Vector2(transform.anchoredPosition.x, showY);
    }
}
