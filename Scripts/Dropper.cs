using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    public GameObject[] dropPoints;
    public int minDrops = 1;
    public int maxDrops = 3;
    public GameObject dropItem;

    private int[] usedPoints = new int[100];

    public void drop()
    {
        int numItems = Random.Range(minDrops, maxDrops);
        for (int i = 0; i < numItems; i++)
        {
            int rand = Random.Range(0, dropPoints.Length);

            if (!containsInt(rand, usedPoints))
            {
                Instantiate(dropItem, dropPoints[rand].transform.position, Quaternion.identity);
                usedPoints[i] = rand;
            }
            else
            {
                numItems += 1;
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
