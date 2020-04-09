using System.Collections;
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

    private float _speedMultiplier = 2;

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

    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;

    private bool _isShieldsActive = false;

   [SerializeField]
    public GameObject _shieldVisualizer;

    [SerializeField]
    private GameObject _rightDMG, _leftDMG;

    [SerializeField]
    private int _score;

    private UIManager _uiManager;

    void Start()
    {
        //take current position = new postion (0, 0 , 0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_spawnManager == null) 
        {
            Debug.LogError("The Spawn Manager Is NULL.");
        }

        if (_uiManager == null) 
        {
            Debug.LogError("The UI Manager is NULL");
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

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);


            transform.Translate(direction * _speed * Time.deltaTime);
        
    
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
        if (_isShieldsActive == true) 
        {
            _isShieldsActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

        _lives--;

        if (_lives == 2) 
        {
            _rightDMG.SetActive(true);
        }
        else if (_lives ==1) 
        {
            _leftDMG.SetActive(true);
        }
        _uiManager.UpdateLives(_lives);

        if (_lives < 1) 
        {

            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }


    }

    public void TripleShotActive() 
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine() 
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;

    
    }

    public void SpeedBoostActive() 
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine() 
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }

    public void ShieldsActive() 
    {
        _isShieldsActive = true;
        _shieldVisualizer.SetActive(true);
    }

    public void AddScore(int points) 
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

}
