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
    private float lastDuration = 0f;
    int lastClip = 0;

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
            yield return new WaitForSeconds(Random.Range(minMaxDuration.x, minMaxDuration.y) + lastDuration);
            int index = Random.Range(0, clips.Length);
            if (index == lastClip) index++;
            index = index % clips.Length;
            float vol = 0.5f;
            if (index >= 2) vol = 0.3f;
            AudioClip clip = clips[index];
            _audio.PlayOneShot(clip, vol);
            lastDuration = clip.length;
        }
    }
}
