using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(Animator))]
public class EnemyController : MonoBehaviour
{
    [Header("Enemy Parameters")]
    [SerializeField] private float speed=4.0f;
    [SerializeField] private float bottomLimit = 0.0f;
    [SerializeField] private Animator animator;
    private PlayerController playerController;
    private void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
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
        if (other.gameObject.tag == GameTags.Player)
        {            
            if (playerController != null)
            {                
                playerController.Damage();                 
            }
            ShowExplosionAnim();            
            Destroy(gameObject,2.8f);
        }
        if (other.gameObject.tag == GameTags.Laser)
        {
            playerController.AddScore(10);
            Destroy(other.gameObject);            
            ShowExplosionAnim();
            Destroy(gameObject,2.8f);            
        }
    }
    private void ShowExplosionAnim()
    {
        speed = 0;
        animator.SetTrigger("OnEnemyDeath");
    }
    #endregion
}
