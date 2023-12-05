using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private GameObject _lightSourse;
    [SerializeField] private AudioSource _shootSound;

    public void Perfom()
    {
        StartCoroutine(EffectRoutine());
    }

    private IEnumerator EffectRoutine()
    {
        _shootSound.Play();
        _lightSourse.SetActive(true);
        _particleSystem.Clear();
        _particleSystem.Play();

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        _lightSourse.SetActive(false);
    }
}
