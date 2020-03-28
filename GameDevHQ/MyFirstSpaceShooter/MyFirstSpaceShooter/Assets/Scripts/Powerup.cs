using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float PowerUpSpeed = 3.0f;
    [SerializeField]
    private int powerupID;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //move down at the speed of 3 (adjust in the inspector) - CHECK
        //when we leave screen destroy object - CHECK

        if (transform.position.y < -7f) 
        {
            Destroy(this.gameObject);
        }

        transform.Translate(Vector3.down * PowerUpSpeed * Time.deltaTime);

    }

    //OnTriggerCollision
    //only be collectable by the player (HINT: Use Tags)
    //on collect, destory 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") 
        {

            Player player = other.transform.GetComponent<Player>();

            if (player !=null)
            {
                //if powerupID is 0
                player.TripleShotActive();
                //else if powerupID is 1
                //Play speed power up
                //else if powerupID is 2
                //Play shields power up
            }

            Destroy(this.gameObject);
        }


    }

}
