using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player configurations")]
    [SerializeField] private float speed=3f;
    [SerializeField] private float verticalLimit = 3.8f;
    [SerializeField] private float horizontalLimit = 9.5f;
    [SerializeField] private float fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField] private int lives = 3;

    [Header("Bullet references")]
    public GameObject laserPrefab;
    public Transform bulletOrigin;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 direction;

    private SpawnManager spawnManager;
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireLaser();
        }
        
    }
    #region Private Methods
    private void CalculateMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //transform.Translate(Vector3.right*Time.deltaTime*speed*horizontalInput);        
        //transform.Translate(Vector3.up*Time.deltaTime*speed*verticalInput);

        direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * speed * Time.deltaTime);

        //if (transform.position.y >= verticalLimit)
        //{
        //    transform.position = new Vector3(transform.position.x, verticalLimit, 0);
        //}
        //else if (transform.position.y <= -verticalLimit)
        //{
        //    transform.position = new Vector3(transform.position.x, -verticalLimit, 0);
        //}
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
            Instantiate(laserPrefab, bulletOrigin.position, Quaternion.identity);
        }
    }
    #endregion
    #region Public Methods
    public void Damage()
    {
        lives--;
        if (lives < 1)
        {
            if (spawnManager != null)
            {
                spawnManager.OnPlayerDeath();
            }   
            print("Game Over!");
            Destroy(gameObject);            
        }
    }
    #endregion
}
