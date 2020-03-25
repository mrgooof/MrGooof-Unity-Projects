﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }//void start end

    // Update is called once per frame
    void Update()
    {
        //move down at 4 meters per second

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //if bottom of screen 
        //respawn at top with a new random x position

        if (transform.position.y < -5f) 
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }

    }// void update end



    //On trigger like mario coin. This collects or interacts when the 2 triggers make contact. 
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if other is player
        //damage player
        //destroy us ( enemy ) 

        if (other.tag == "Player") 
        {
            //Damage the player
            Player player = other.transform.GetComponent<Player>();

            if (player != null) 
            {
                player.Damage();
            }

            Destroy(this.gameObject);

        }

        //if other is laser
        //destory laser
        //destroy us ( enemy )

        if (other.tag == "Laser") 
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }


    } //Void OnTriggerEnter End



}// MonoBehavior End
