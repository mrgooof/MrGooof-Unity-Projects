using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;
    private Animator _anim;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();

        // null check
        if (_player == null) 
        {
            Debug.LogError("The Player is NULL.");
        }

        _anim = GetComponent<Animator>();

        if (_anim == null) 
        {
            Debug.LogError("The Animator is NULL");
        }


    }

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

            _anim.SetTrigger("OnEnemyDeath");
            _speed = 5f;
            _audioSource.Play();
            Destroy(this.gameObject, 2.3f);

        }

        //if other is laser
        //destory laser
        //destroy us ( enemy )

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddScore(10);
            }

            _anim.SetTrigger("OnEnemyDeath");
            _speed = .5f;
            _audioSource.Play();
            Destroy(this.gameObject, 2.3f);
        }


    } //Void OnTriggerEnter End



}// MonoBehavior End
