using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionController : MonoBehaviour
{
    AudioSource audioSource;
    private void Awake()
    {
        audioSource= GetComponent<AudioSource>();
        Destroy(gameObject, audioSource.clip.length);

    }

}
