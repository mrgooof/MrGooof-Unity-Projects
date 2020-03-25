using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    
        //speed variable of 8
    [SerializeField]
    private float _LaserSpeed = 8.0f;

    // Start is called before the first frame update

   

    // Update is called once per frame
    void Update()
    {
        //translate laser up

        transform.Translate(Vector3.up * Time.deltaTime * _LaserSpeed);

        //If laser position is greater than 8 on the y
        //Destory the object

        if (transform.position.y > 8f) 
        {
            Destroy(this.gameObject);
        }
    }
}
