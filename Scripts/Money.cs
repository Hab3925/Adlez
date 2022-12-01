using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public int Monney;
    public string Moneytext;
    public Text Monneytext;

   
    public void M(){
        Monney +=1;
        Debug.Log(Monney);
       Moneytext = ""+Monney;
       Monneytext.text = Moneytext;
    }
}
