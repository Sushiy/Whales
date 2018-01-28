using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FadeIn : MonoBehaviour {

    [SerializeField]
    private float fadeInDuration = 2f;

    private AudioSource _audio;
    private float maxVolume = 1f;
    private float currentVelocity = 0f;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
        maxVolume = _audio.volume;
    }
	
    void Start()
    {
        _audio.volume = 0f;
    }

    void Update()
    {
        _audio.volume = Mathf.SmoothDamp(_audio.volume, maxVolume, ref currentVelocity, fadeInDuration);
        if (_audio.volume >= maxVolume - 0.001f) Destroy(this);
    }
}
