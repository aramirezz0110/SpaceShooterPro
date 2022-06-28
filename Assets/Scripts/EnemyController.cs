using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Parameters")]
    [SerializeField] private float speed=4.0f;
    [SerializeField] private float bottomLimit = 0.0f;

    private PlayerController playerController;
    void Start()
    {
        
    }

    
    void Update()
    {
        Movement();
    }
    private void Movement()
    {
        float randomX = 0;
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < bottomLimit)
        {
            randomX = Random.Range(-9f, 9f);
            transform.position = new Vector3(randomX, 4, 0);
        }
    }
    #region Unity Callbacks    
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if other is player
        if (other.gameObject.tag == "Player")
        {
            playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                print("Damage to player!");
                playerController.Damage();
            }
            Destroy(gameObject);
        }
        //if other is laser 
        if (other.gameObject.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
    #endregion
}
