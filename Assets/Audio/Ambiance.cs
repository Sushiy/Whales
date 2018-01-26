using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Ambiance : MonoBehaviour {

    [SerializeField]
    private AudioClip[] clips;
    [SerializeField]
    private Vector2 minMaxDuration = new Vector2(1f, 4f);

    private AudioSource _audio;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        StartCoroutine(PlayRando());
    }

    IEnumerator PlayRando()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minMaxDuration.x, minMaxDuration.y));
            _audio.PlayOneShot(clips[Random.Range(0, clips.Length)], 0.5f);
        }
    }
}
