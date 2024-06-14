using Pong.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Arkanoid
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BallController : MonoBehaviour
    {
        Rigidbody2D ballRb;
        [SerializeField, Range(10, 40)] private float ballSpeed = 20f;
        [SerializeField, Range(0.05f, 0.5f)] private float increaseParameter = 0.1f;
        [SerializeField] float MaxSpeedFactor = 2f;
        public event Action<BallController> OnBallDestroyedEvent;
        [SerializeField] private VausController vausController;
        static bool isCatchy;
        AudioSource audioSource;
        [SerializeField, Range(20, 45)] float minAngleInDegrees = 30f;
        internal bool isFirstTime = true;
        private void OnEnable()
        {
            ballRb = GetComponent<Rigidbody2D>();
        }

        public void Release()
        {
            ballRb.bodyType = RigidbodyType2D.Dynamic;
            if (isFirstTime || isCatchy)
            {
                isFirstTime= false;
                ballRb.velocity = Vector2.up * ballSpeed;
            }
            else
            {
                SetDirection(Random.insideUnitCircle);

            }
        }

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }
        internal void resetBall(BallController ballcontroller)
        {
            Vector3 VausPos = vausController.GetVausPosition();
            Vector3 newBallPos = new Vector3(VausPos.x, VausPos.y + 1f, VausPos.z);
            Rigidbody2D rb = ballcontroller.GetComponent<Rigidbody2D>();

            ballcontroller.gameObject.SetActive(true);
            ResetPosition(ballcontroller, newBallPos);

            ResetRigidBody(rb);

        }
        void UpdateVelocity()
        {
            if (ballRb.velocity.magnitude * (1 + increaseParameter) < ballSpeed * MaxSpeedFactor)
            {
                ballRb.velocity *= 1 + increaseParameter;
            }
        }
        internal void FreezeBall()
        {
            ballRb.velocity = Vector3.zero;
        }
        private static void ResetRigidBody(Rigidbody2D rb)
        {
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        private static void ResetPosition(BallController ballcontroller, Vector3 newBallPos)
        {
            ballcontroller.transform.position = newBallPos;
        }

        internal void SetCatch(bool catchMode)
        {
            isCatchy = catchMode;

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("vacio"))
            {
                OnBallDestroyedEvent?.Invoke(this);
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Vaus"))
            {
                if (isCatchy)
                {
                    ResetRigidBody(ballRb);
                }
                else
                {
                    UpdateVelocity();
                    audioSource.Play();
                }
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            ClampAngles();
        }

        private void ClampAngles()
        {
            float x = ballRb.velocity.x;
            float y = ballRb.velocity.y;
            if (x != 0)
            {
                float angleInDegrees = MathF.Atan(y / x) * Mathf.Rad2Deg;
                float minValue = MathF.Sign(x * y) > 0 ? minAngleInDegrees : -90;
                float maxValue = MathF.Sign(x * y) > 0 ? 90 : -minAngleInDegrees;
                float newAngleInDegrees = Mathf.Clamp(angleInDegrees, minValue, maxValue);
                float newAngleInRadians = newAngleInDegrees * Mathf.Deg2Rad;
                float newX = MathF.Abs(MathF.Cos(newAngleInRadians)) * Mathf.Sign(x);
                float newY = MathF.Abs(MathF.Sin(newAngleInRadians)) * Mathf.Sign(y);
                ballRb.velocity = new Vector2(newX, newY) * ballRb.velocity.magnitude;
                Debug.Log($"{angleInDegrees} -> {newAngleInDegrees}");
            }
        }
        internal bool IsCatchy()
        {
            return isCatchy;
        }

        internal void Slow()
        {
            ballRb.velocity *= 0.5f;
        }

        internal void SetDirection(Vector2 newDirection)
        {
            ballRb.velocity = newDirection.normalized * ballSpeed;
        }
    }
}

