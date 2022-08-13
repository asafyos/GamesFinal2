using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanerScript : MonoBehaviour
{
    public int maxSpawns = 25;
    public float spawnCooldown = 5f;
    public bool stopSpawn = false;

    public PersonScript[] spwanables;
    public GameObject waypointGroups;

    int currentSpanws = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObject", 0f, spawnCooldown);
    }

    public void SpawnObject()
    {
        if (!stopSpawn)
        {
            // Gets a new spawnable type
            int spawnIdx = Random.Range(0, 100) % spwanables.Length;

            // Creates a copy of the type
            PersonScript spawnee = Instantiate(spwanables[spawnIdx], transform.position, transform.rotation);

            // Gets a wapoint system for the spawnee
            GameObject waypoints = waypointGroups.transform.GetChild(Random.Range(0, waypointGroups.transform.childCount - 1)).gameObject;
            spawnee.waypointsHolder = waypoints;

            // Random waypoints order
            spawnee.randomPoints = Random.Range(0, 100) % 2 == 0;

            // Random speed
            spawnee.speed = Random.Range(3.5f, 7f);

            // Random damage
            spawnee.damage = Random.Range(12, 18);

            if (++currentSpanws >= maxSpawns)
            {
                CancelInvoke("SpawnObject");
            }
        }
    }
}
