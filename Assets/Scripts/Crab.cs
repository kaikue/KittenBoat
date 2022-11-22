using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour
{
    public Sprite altSprite;
    public GameObject spawnedDialog;

    private const float speed = 7;

    private SpriteRenderer sr;
    private Sprite normalSprite;
    private const float walkTime = 0.15f;
    private float walkTimer = 0;
    private bool walkSpriteUsed = false;

    private Player player;
    private GameObject item;

    private const int pathCount = 5;
    private const int maxPathLength = 30;
    private const float pathTurnChance = 0.2f;
    private Vector2[] directions = { Vector2.left, Vector2.right, Vector2.up, Vector2.down };
    private List<Vector2> currentPath;
    private int currentPathStep;
    private Vector2 size = new Vector2(0.85f, 0.85f);
    private const float snapDist = 0.2f;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        normalSprite = sr.sprite;
        player = FindObjectOfType<Player>();
        FindPaths();
        MusicManager musicManager = FindObjectOfType<MusicManager>();
        musicManager.SetMusicOverride(musicManager.crabMusicSrc);
        Dialog newDialog = Instantiate(spawnedDialog).GetComponent<Dialog>();
        newDialog.player = player;
    }

    private void Update()
    {
        walkTimer += Time.deltaTime;
        if (walkTimer > walkTime)
        {
            walkTimer = 0;
            walkSpriteUsed = !walkSpriteUsed;
            sr.sprite = walkSpriteUsed ? altSprite : normalSprite;
        }
    }

    private void FixedUpdate()
    {
        Vector2 currentPos = transform.position;
        Vector2 goalPos = currentPath[currentPathStep];
        if (Vector2.Distance(currentPos, goalPos) < snapDist)
        {
            transform.position = goalPos;
            currentPathStep++;
            if (currentPathStep >= currentPath.Count)
            {
                currentPathStep = 0;
                FindPaths();
            }
        }
        else
        {
            Vector2 diff = goalPos - currentPos;
            Vector2 dir = diff.normalized;
            Vector2 move = dir * Time.fixedDeltaTime * speed;
            transform.position = currentPos + move;
        }
    }

    private void FindPaths()
    {
        Vector2 currentPos = transform.position;//new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        List<List<Vector2>> paths = new List<List<Vector2>>();
        for (int i = 0; i < pathCount; i++)
        {
            List<Vector2> path = new List<Vector2>();
            Vector2 dir = directions[Random.Range(0, 4)];
            Vector2 nextStep = currentPos + dir;
            int pathLen = 0;
            while (IsFree(nextStep) && pathLen < maxPathLength)
            {
                path.Add(nextStep);
                pathLen++;
                if (Random.Range(0f, 1f) < pathTurnChance)
                {
                    dir = directions[Random.Range(0, 4)];
                }
                nextStep += dir;
            }
            if (pathLen == 0)
            {
                path.Add(currentPos);
            }
            paths.Add(path);
        }

        List<Vector2> bestPath = FindBestPath(paths);
        currentPath = bestPath;
    }

    private bool IsFree(Vector2 pos)
    {
        Collider2D collider = Physics2D.OverlapBox(pos, size, 0, LayerMask.GetMask("Default", "RockTiles", "WaterTiles", "Pushable", "Pit"));
        return collider == null;
    }

    private List<Vector2> FindBestPath(List<List<Vector2>> paths)
    {
        Vector2 playerPos = player.transform.position;
        float bestScore = -1;
        List<Vector2> bestPath = null;
        foreach (List<Vector2> path in paths)
        {
            float totalDist = 0;
            foreach (Vector2 pathPoint in path)
            {
                totalDist += Vector2.Distance(playerPos, pathPoint);
            }
            float score = totalDist / path.Count;
            if (score > bestScore)
            {
                bestScore = score;
                bestPath = path;
            }
        }
        return bestPath;
    }

    public void SetItem(GameObject item)
    {
        this.item = item;
    }

    public void Catch()
    {
        if (item) Instantiate(item);
        Destroy(gameObject);
    }
}
