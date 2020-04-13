using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space Key Pressed");
        }

        if (Input.GetKey(KeyCode.E)) 
        {
            Debug.Log("E KEY HELD/PRESSED");
        }

        if (Input.GetKeyUp(KeyCode.F)) 
        {
            Debug.Log("F KEY UP");
        }
    }
}
