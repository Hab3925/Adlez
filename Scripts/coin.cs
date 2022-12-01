using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        var Coin = GameObject.FindGameObjectWithTag("Coin").GetComponent<Money>();
        if (other.gameObject.tag.Equals("Player"))
        {
            Coin.M();
            Destroy(gameObject);
        }

    }
}
