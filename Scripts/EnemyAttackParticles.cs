using Scripts.Arkanoid;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackParticles : MonoBehaviour
{

    public event Action OnVausGethitEvent;

    private void OnParticleCollision(GameObject other)
    {
        
        if (other.CompareTag("Vaus"))
        {
            OnVausGethitEvent?.Invoke();
        }
    }
}
