using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpType powerUpType;
    [SerializeField] private float speed = 3.0f;    
    private void Start()
    {
        
    }
    private void Update()
    {
        MoveDown();
    }
    #region Private Methods
    private void MoveDown()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        CheckPositionAndDestroy();
    }
    private void CheckPositionAndDestroy()
    {
        if (transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }
    #endregion
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == GameTags.Player)
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {                
                switch (powerUpType)
                {
                    case PowerUpType.TripleShot:
                        {
                            playerController.TripleShotActive();
                            break;
                        }
                    case PowerUpType.Speed:
                        {
                            playerController.SpeedActive();
                            break;
                        }
                    case PowerUpType.Shield:
                        {
                            playerController.ShieldActive();
                            break;
                        }
                    default: break;
                }
            }
            AudioManager.Instance.PlayPowerUpSound();
            Destroy(this.gameObject);
        }
    }
}
enum PowerUpType 
{
    TripleShot,
    Speed,
    Shield
}


