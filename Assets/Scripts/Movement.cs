using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int horizontal = 0;      
        int vertical = 0;        

    #if UNITY_STANDALONE
        horizontal = (int)(Input.GetAxisRaw("Horizontal"));

        
        vertical = (int)(Input.GetAxisRaw("Vertical"));

        if (horizontal == 0 && vertical == 0)
        {
            return;
        }

        if (horizontal != 0)
        {
            vertical = 0;
        }

    #endif

        Debug.Log("HORIZONTAL: " + horizontal + " VERTICAL: " + vertical);

    }
}
