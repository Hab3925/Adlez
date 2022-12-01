using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
   
    public Animator animator;
    public PlayerController direction;
    public AudioClip Attacksound;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
        if(Input.GetMouseButtonDown(0)){

            if(direction.facing == "up"){
                animator.SetTrigger("Attack-up");
                AudioSource.PlayClipAtPoint (Attacksound, transform.position);
            }
            if(direction.facing=="down"){
                AudioSource.PlayClipAtPoint (Attacksound, transform.position);
                animator.SetTrigger("Attack-down");
            }
            else
            {
                animator.SetTrigger("Atack");
                AudioSource.PlayClipAtPoint (Attacksound, transform.position);
            }
        }
    }
}
