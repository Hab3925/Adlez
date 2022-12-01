using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int height;
    public int width;
    public GameObject[] sprites;
    public int[,] Grid;

    // Start is called before the first frame update
    void Start()
    {
        Grid = new int[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Grid[i, j] = Random.Range(0, sprites.Length);
                SpawnTile(i, j, Grid[i, j]);
            }
        }
    }

    private void SpawnTile(int x, int y, int value)
    {
        GameObject g = new GameObject("X: " + x + "Y: " + y);
        g.transform.position = new Vector3(y, x);
        var sr = g.AddComponent<SpriteRenderer>();
        sr.sprite = sprites[value].GetComponent<SpriteRenderer>().sprite;

        if (sprites[value].GetComponent<BoxCollider2D>())
        {
            var coll = g.AddComponent<BoxCollider2D>();
            var tempColl = sprites[value].GetComponent<BoxCollider2D>();
            coll.size = tempColl.size;
            coll.offset = tempColl.offset;
        }
    }
}
