using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public int damage = 2;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("breakable"))
        {
            other.GetComponent<pot>().Smash();
        }
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Shroom>().hit(damage);
        }
    }
}
