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

    private void Start()
    {
        boat = FindObjectOfType<Boat>();
        StartCoroutine(CheckSpawn());
    }

    private IEnumerator CheckSpawn()
    {
        while (true)
        {
            if (piranhaSpawned == null && Vector3.Distance(boat.transform.position, transform.position) <= spawnDist)
            {
                piranhaSpawned = Instantiate(piranhaPrefab, transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(checkTime);
        }
    }
}
