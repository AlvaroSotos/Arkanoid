using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Scripts.Arkanoid
{
    [RequireComponent(typeof(Collider2D))]
    public class BrickController : MonoBehaviour
    {
        [SerializeField, Min(50)] int score = 50;
        [SerializeField, Range(1, 10)] internal int resistance = 1;
        [SerializeField] PowerUpType powerUpType;
        DamageController damageController;
        public event Action<BrickController> OnBrickDestroyedEvent;


        public int GetScore() { return score; }

        private void Awake()
        {
            damageController = GetComponent<DamageController>();
        }
        internal bool HasPowerUp()
        {
            return !powerUpType.Equals(PowerUpType.NoPowerUp);
        }
        internal PowerUpType GetPowerupType()
        {
            return powerUpType;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == ("ball"))
            {
                resistance = damageController.MakeDamageBall(resistance);
                if (resistance <= 0)
                {
                    OnBrickDestroyedEvent?.Invoke(this);
                }
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                resistance = damageController.MakeDamageBullet(resistance);
                if (resistance <= 0)
                {
                    OnBrickDestroyedEvent?.Invoke(this);
                }
            }
        }

        internal void SetPowerUp(PowerUpType powerUpType)
        {
            this.powerUpType = powerUpType;
        }
    }
}

