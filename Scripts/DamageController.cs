using System;
using UnityEngine;

namespace Scripts.Arkanoid
{
    [RequireComponent(typeof(AudioSource))]
    public class DamageController : MonoBehaviour
    {
        [SerializeField, Min(50)] int score = 50;
        [SerializeField, Range(1, 10)] int resistance = 1;
        [SerializeField] GameObject destructionPrefab;
        public event Action<DamageController> OnDestroyedEvent;
        public event Action<DamageController> OnDamageReceivedEvent;
        AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }
        internal int GetScore()
        {
            return score;
        }

        internal int MakeDamageBullet(int resistance)
        {
            resistance--;
            if (resistance <= 0)
            {
                Instantiate(destructionPrefab, transform.position, transform.rotation);
                OnDestroyedEvent?.Invoke(this);
            }
            else
            {
                OnDamageReceivedEvent?.Invoke(this);
                audioSource.PlayOneShot(audioSource.clip);
            }
            return resistance;
        }
        internal int MakeDamageBall(int resistance)
        {
            resistance -= 2;
            if (resistance <= 0)
            {

                Instantiate(destructionPrefab, transform.position, transform.rotation);
                OnDestroyedEvent?.Invoke(this);
            }
            else
            {
                OnDamageReceivedEvent?.Invoke(this);
                audioSource.PlayOneShot(audioSource.clip);
            }
            return resistance;
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("ball") && !gameObject.CompareTag("Brick"))
            {
                resistance = MakeDamageBall(resistance);
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Bullet") && !gameObject.CompareTag("Brick"))
            {
                resistance = MakeDamageBullet(resistance);
            }
        }
    }
}