using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float PowerUpSpeed = 3.0f;
    //ID FOR POWERUPS
    //0 = Triple Shot
    //1 = Speed Boost
    //2 = Shileds 
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
                
                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;

                    case 1:
                        player.SpeedBoostActive();
                        break;

                    case 2:
                        Debug.Log("Default Value");
                        break;

                    default:
                        Debug.Log("Default Value");
                        break;

                }
                
            }

            Destroy(this.gameObject);
        }


    }

}
