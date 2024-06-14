using System;
using UnityEngine;

namespace Scripts.Arkanoid
{
    [RequireComponent(typeof(Collider2D))]
    public class BreakController : MonoBehaviour
    {
        public event Action OnVausEnterBreakEvent;

        internal void Activate()
        {
            gameObject.SetActive(true);
        }

        internal void Reset()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Vaus"))
            {
                OnVausEnterBreakEvent?.Invoke();
            }
        }
    }
}