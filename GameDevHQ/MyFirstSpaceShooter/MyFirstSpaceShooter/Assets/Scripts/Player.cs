﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public or private reference
    //data type ( int = Number, float = Decimal Value, bool = True or false, string = Text )
    //Every variable has a name
    //Optional value assigned
    // Start is called before the first frame update

    [SerializeField]
    private float _speed = 3.5f;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private float _fireRate = 0.5f;

    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnManager;

    [SerializeField]
    private bool _isTripleShotActive = false; 

    //Variable for is Triple_Shot Active?

    void Start()
    {
        //take current position = new postion (0, 0 , 0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (_spawnManager == null) 
        {
            Debug.LogError("The Spawn Manager Is NULL.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();


        //if i hit the space key
        //spawn gameObject

        if (Input.GetKeyDown(KeyCode.Space)&& Time.time > _canFire) 
        {
            FireLaser(); 
        }


    }

    void CalculateMovement() 
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        //Method for clamping between 2 values instead of multiple transform positions
        //similar to the x teleport or if else statement below
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0),0);


        if (transform.position.x >= 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }

        else if (transform.position.x < -11)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }

    }
    void FireLaser() 
    {
        _canFire = Time.time + _fireRate;

        if (_isTripleShotActive == true) 
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else 
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
        


        //If space key pressed
        //If triple shot Active is true
        //Fire 3 lasers

        //Else fire 1 laser 

        //Instantiate 3 lasers ( triple shot prefab)

    }


    public void Damage()
    {
        
        _lives--;


        if (_lives < 1) 
        {

            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }


    }


}
