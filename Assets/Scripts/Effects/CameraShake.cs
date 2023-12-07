using com.cyborgAssets.inspectorButtonPro;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Niose")]
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _perlinNoiseTimeScale;
    [SerializeField] private AnimationCurve _perlinNoiseAmplitudeCurve;

    [Header("Recoil")]
    [SerializeField] private float _tenson;
    [SerializeField] private float _damping;
    [SerializeField] private float _impulse;

    private Vector3 _shakeAngles = new Vector3();

    private Vector3 _recoilAngles = new Vector3();
    private Vector3 _recoilVelocity = new Vector3();

    private float _amplitude = 5f;
    private float _duration = 1f;
    private float _shakeTimer = -1f;
    
    private void Update()
    {
        UpdateShake();
        UpdateRecoil();

        _cameraTransform.localEulerAngles = _shakeAngles + _recoilAngles;
    }

    private void UpdateRecoil()
    {
        _recoilAngles += _recoilVelocity * Time.deltaTime;
        _recoilVelocity += -_recoilAngles * Time.deltaTime * _tenson;
        _recoilVelocity = Vector3.Lerp(_recoilVelocity, Vector3.zero, Time.deltaTime * _damping);
        print("UpddateRecoil");
    }

    private void UpdateShake()
    {
        if (_shakeTimer > 0)
        {
            _shakeTimer -= Time.deltaTime / _duration;
        }

        float time = Time.time * _perlinNoiseTimeScale;
        _shakeAngles.x = Mathf.PerlinNoise(time, 0);
        _shakeAngles.y = Mathf.PerlinNoise(0, time);
        _shakeAngles.z = Mathf.PerlinNoise(time, time);

        _shakeAngles *= _amplitude;
        _shakeAngles *= _perlinNoiseAmplitudeCurve.Evaluate(Mathf.Clamp01(1 - _shakeTimer));
    }

    [ProPlayButton]
    public void MakeShake()
    {
        MakeShake(15f, 3f);
    }

    public void MakeShake(float amplitude, float duration)
    {
        _amplitude = amplitude;
        _duration = Mathf.Max(duration, 0.05f);
        _shakeTimer = 1f;
        print("Shake");
    }

    //public void MakeRecoil()
    //{
    //    MakeRecoil(-Vector3.right * UnityEngine.Random.Range(_impulse * 0.5f, _impulse));// + Vector3.up * UnityEngine.Random.Range(-_impulse, _impulse) / 4f);
    //}
     
    [ProPlayButton]
    public void MakeRecoil()//Vector3 impulse)
    {
        //_recoilVelocity += impulse;
        _recoilVelocity += -Vector3.right * Random.Range(_impulse * 0.5f, _impulse);
        print($"Recoil = {_recoilVelocity}");
    }
}
