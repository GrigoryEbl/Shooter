using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellSound : MonoBehaviour
{
    [SerializeField] private AudioSource _sound;

    private bool _isPlaying = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (_isPlaying)
        {
            _isPlaying = false;
            _sound.Play();
        }
    }
}
