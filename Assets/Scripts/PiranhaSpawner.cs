using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiranhaSpawner : MonoBehaviour
{
    private const float checkTime = 3;
    private const float spawnDist = 15;

    public GameObject piranhaPrefab;
    private GameObject piranhaSpawned;

    private Boat boat;
    private Player player;

    private void Start()
    {
        boat = FindObjectOfType<Boat>();
        player = FindObjectOfType<Player>();
        StartCoroutine(CheckSpawn());
    }

    private IEnumerator CheckSpawn()
    {
        while (true)
        {
            if (piranhaSpawned == null && Vector3.Distance(boat.transform.position, transform.position) <= spawnDist && !boat.smashed && !player.paused)
            {
                piranhaSpawned = Instantiate(piranhaPrefab, transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(checkTime);
        }
    }
}
