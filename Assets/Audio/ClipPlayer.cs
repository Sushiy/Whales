﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ClipPlayer : MonoBehaviour {

    public static ClipPlayer s_clipPlayer = null;

    [SerializeField]
    private AudioClip[] whale1;
    [SerializeField]
    private AudioClip[] surface;
    [SerializeField]
    private AudioClip[] thump;
    [SerializeField]
    private AudioClip[] crash;
    [SerializeField]
    private AudioClip[] ice_thru;

    private AudioSource audio_;

    void Awake()
    {
        // singleton code
        if (s_clipPlayer == null)
        {
            s_clipPlayer = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        audio_ = GetComponent<AudioSource>();
    }

    public void PlayWhale1()
    {
        int randomIndex = Random.Range(0, whale1.Length);
        audio_.PlayOneShot(whale1[randomIndex]);
    }

    public void PlaySurface()
    {
        int randomIndex = Random.Range(0, surface.Length);
        audio_.PlayOneShot(surface[randomIndex], 0.3f);
    }

    public void PlayThump(int index)
    {
        audio_.PlayOneShot(thump[index]);
    }

    public void PlayCrash(int index, float vol)
    {
        audio_.PlayOneShot(crash[index],vol);
    }

    public void PlaySurfaceThroughIce(int index)
    {
        PlayCrash(index, 0.3f);
        StartCoroutine(SurfaceDelay(1.5f));
    }

    public void PlaySurfaceThroughIce2()
    {
        PlayCrash(3, 1f);
        PlaySurface();
    }

    IEnumerator SurfaceDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PlaySurface();
    }

    public void PlayIceThrough()
    {
        int randomIndex = Random.Range(0, ice_thru.Length);
        audio_.PlayOneShot(ice_thru[randomIndex]);
    }
}
