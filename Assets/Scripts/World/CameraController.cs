using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform PlayerTransform;

    // Update is called once per frame
    void Update()
    {
        if (PlayerTransform == null)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Player");
            if (go!= null)
            {
                PlayerTransform = go.transform;
            }
        } 
        else
        {
            this.transform.position = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, -30);
        }
    }
}
