 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pot : MonoBehaviour
{
    private Animator anim;
    public AudioClip sound;
    void Start(){
        anim = GetComponent<Animator>();
    }


    public void Smash(){
        anim.SetBool("Smash", true);
        StartCoroutine(breakCo());
        AudioSource.PlayClipAtPoint (sound, transform.position);
    }

    IEnumerator breakCo(){
        yield return new WaitForSeconds(.3f);
        this.gameObject.SetActive(false);
    }
}

