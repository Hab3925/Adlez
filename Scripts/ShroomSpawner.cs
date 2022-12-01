using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomSpawner : MonoBehaviour
{
    public GameObject[] SpawnPoints;
    public int units = 3;
    public int spawnRange = 6;
    public GameObject Shroom;

    private Transform player;
    private bool spawned = false;
    private int[] usedPoints = new int[100];

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (Vector2.Distance(player.position, transform.position) < spawnRange && !spawned)
        {
            spawned = true;
            for (int i = 0; i < units; i++)
            {
                int rand = Random.Range(0, SpawnPoints.Length);

                if (!containsInt(rand, usedPoints))
                {
                    Instantiate(Shroom, SpawnPoints[rand].transform.position, Quaternion.identity);
                    usedPoints[i] = rand;
                }
                else
                {
                    units += 1;
                }
            }
        }

    }
    public bool containsInt(int input, int[] array)
    {
        if (array == null || array.Length == 0) return false;
        foreach (int e in array)
        {
            if (e == input)
            {
                return true;
            }
        }
        return false;
    }
}
