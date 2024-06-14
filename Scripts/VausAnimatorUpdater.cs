using Scripts.Arkanoid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Arkanoid
{
    [RequireComponent(typeof(Animator))]
    public class VausAnimatorUpdater : MonoBehaviour, IUpdateVaus
    {
        Animator animator;
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        public void UpdateVaus(VausState vausState)
        {
            switch (vausState)
            {
                case VausState.Normal:
                    animator.SetTrigger("Normal");
                    break;
                case VausState.Enlarged:
                    animator.SetTrigger("Enlarged");
                    break;
                case VausState.Laser:
                    animator.SetTrigger("Laser");
                    break;
                case VausState.Catch:
                    break;
                case VausState.Destruction:
                    animator.SetTrigger("Destruction");
                    break;
            }
        }


    }
}

