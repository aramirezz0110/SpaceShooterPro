using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float selfDestructionTime = 2f;
    
    private void Start()
    {
        AudioManager.Instance.PlayExplosionSound();
        Destroy(gameObject, selfDestructionTime);
    }
}
