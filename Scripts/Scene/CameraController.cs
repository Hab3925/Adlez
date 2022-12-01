using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform Knil;
    public float cameraDistance;
    public Vector2 maxPosition;
    public Vector2 minPosition;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position != Knil.position)
        {
            Vector3 KnilPosition = new Vector3(Knil.position.x, Knil.position.y, -cameraDistance);
            KnilPosition.x = Mathf.Clamp(KnilPosition.x, minPosition.x, maxPosition.x);
            KnilPosition.y = Mathf.Clamp(KnilPosition.y, minPosition.y, maxPosition.y); 
            transform.position = Vector3.Lerp(transform.position, KnilPosition, cameraDistance);
        }
       
        

    }
}
