using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Dialoge : MonoBehaviour
{

    public GameObject dialogeBox;
    public Text dialogeTitle;
    public Text dialogeText;
    public string title;
    public string dialog;
    public bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerInRange){
            if(dialogeBox.activeInHierarchy){
                dialogeBox.SetActive(false);
            }
           
            else{
                dialogeBox.SetActive(true);
                dialogeText.text=dialog;
                dialogeTitle.text = title; 
                
            }
        } 
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            playerInRange =true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){
            playerInRange =false;
            dialogeBox.SetActive(false);

        }
        
    }
}
