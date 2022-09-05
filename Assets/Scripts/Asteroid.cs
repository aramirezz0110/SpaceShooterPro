using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 3.0f;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float selfDestructionTime = .6f;

    
    private void Update()
    {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == GameTags.Laser)
        {
            SpawnManager.Instance.StartSpawninig();
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(collision.gameObject);
            gameObject.SetActive(false);
            Destroy(gameObject, selfDestructionTime);
        }
    }
}
