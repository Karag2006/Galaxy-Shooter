using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _score = 0;

    private UIManager _uiManager;

    private SpawnManager _spawnManager;

    [SerializeField]
    private bool _tripleshotActive = false;
    [SerializeField]
    private float _tripleshotPowerDownTimer = 5f;

    [SerializeField]
    private bool _speedBoostActive = false;
    [SerializeField]
    private float _speedBoostPowerDownTimer = 3f;
    [SerializeField]
    private float _speedBoostStrength = 3.8f;

    [SerializeField]
    private bool _shieldActive = false;




    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn_Manager was Null");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("UIManager was Null");
        }

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float currentSpeed = _speed;
        if (_speedBoostActive) {
          currentSpeed += _speedBoostStrength;
        }
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * currentSpeed * Time.deltaTime);

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if(transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }



        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y , 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        Vector3 offset = new Vector3(0, 1.14f , 0);
        Vector3 tripleshotOffset = new Vector3(-1.36f, 0.4f, 0);

        if (_tripleshotActive == true)
        {
            Instantiate(_tripleShotPrefab,transform.position + tripleshotOffset, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + offset, Quaternion.identity);
        }
    }

    public void Damage()
    {
        if (_shieldActive)
        {
            _shieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

        _lives--;

        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void ActivateTripleShot()
    {
        _tripleshotActive = true;

        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(_tripleshotPowerDownTimer);
        _tripleshotActive = false;
    }


    public void ActivateSpeedBoost()
    {
        _speedBoostActive = true;

        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(_speedBoostPowerDownTimer);
        _speedBoostActive = false;
    }

    public void ActivateShield()
    {
        _shieldActive = true;
        _shieldVisualizer.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
