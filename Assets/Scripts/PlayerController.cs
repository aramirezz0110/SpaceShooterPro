using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("Player configurations")]
    [SerializeField] private float speed=3f;
    [SerializeField] private float speedMultiplier = 2f;
    [SerializeField] private float fireRate = 0.5f;

    [SerializeField] private float verticalLimit = 3.8f;
    [SerializeField] private float horizontalLimit = 9.5f;
    
    private float _canFire = -1f;
    [SerializeField] private int lives = 3;

    [Header("Laser references")]
    public GameObject laserPrefab;
    public GameObject tripleShootPrefab;
    public Transform bulletOrigin;

    [Header("Scripts References")]    
    public UIManager uiManager;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 direction;


    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    private bool isTripleShotActive=false;
    private bool isSpeedBoostActive = false;
    private bool isShieldActive = false;

    [SerializeField] private GameObject shieldVisualizer;

    [SerializeField] private int score=0;
    void Start()
    {
        shieldVisualizer.SetActive(false);
        transform.position = new Vector3(0, 0, 0);       

        uiManager.UpdateLives(lives);


    }    
    void Update()
    {
        CalculateMovement();
        if (Input.GetKey(KeyCode.Space))
        {
            FireLaser();
        }        
    }
    #region Private Methods
    private void CalculateMovement()
    {
        horizontalInput = Input.GetAxis(Horizontal);
        verticalInput = Input.GetAxis(Vertical);

        direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -verticalLimit, 0));

        if (transform.position.x >= horizontalLimit)
        {
            transform.position = new Vector3(horizontalLimit, transform.position.y, 0);
        }
        else if (transform.position.x <= -horizontalLimit)
        {
            transform.position = new Vector3(-horizontalLimit, transform.position.y, 0);
        }
    }
    private void FireLaser()
    {        
        if (Time.time > _canFire)
        {
            _canFire = Time.time + fireRate;

            if (isTripleShotActive)
            {
                Instantiate(tripleShootPrefab, bulletOrigin.position, Quaternion.identity);
            }
            else
            {
                Instantiate(laserPrefab, bulletOrigin.position, Quaternion.identity);
            }            
        }
    }
    private IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isTripleShotActive = false;
    }
    private IEnumerator SpeedBoosterDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoostActive = false;
        speed /= speedMultiplier;
    }
    private IEnumerator ShieldDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isShieldActive = false;
    }
    #endregion
    #region Public Methods
    public void Damage()
    {
        if (isShieldActive)
        {
            isShieldActive = false;
            shieldVisualizer.SetActive(false);
            return;
        }    

        lives--;
        uiManager.UpdateLives(lives);
        if (lives == 0)
        {            
            SpawnManager.Instance.OnPlayerDeath();

            GameManager.Instance.GameOver();
            print("Game Over!");
            Destroy(gameObject);            
        }
    }
    public void TripleShotActive()
    {
        isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    public void SpeedActive()
    {
        isSpeedBoostActive = true;
        speed *= speedMultiplier;
        StartCoroutine(SpeedBoosterDownRoutine());
    }
    public void ShieldActive()
    {
        isShieldActive = true;
        if (shieldVisualizer != null)
        {
            shieldVisualizer.SetActive(true);
        }
    }
    public void AddScore(int points) 
    {
        score += points;
        if (uiManager != null)
        {
            uiManager.UpdateScore(score);
        }
    }
    #endregion
}
