using System.Collections;
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
    [SerializeField]
    private AudioClip harpoon;
    [SerializeField]
    private AudioClip bubbles;
    [SerializeField]
    private AudioClip[] hit;
    [SerializeField]
    private AudioClip[] lampfish;
    [SerializeField]
    private AudioClip underwater_explosion;
    [SerializeField]
    private AudioClip spear_splash;
    [SerializeField]
    private AudioClip[] huyas;
    [SerializeField]
    private AudioClip[] whale_alarm;
    [SerializeField]
    private AudioClip[] whale_comehere;
    [SerializeField]
    private AudioClip[] bigWhale;

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

    public void PlayHarpoon()
    {
        audio_.PlayOneShot(harpoon);
    }

    public void PlayHit()
    {
        int randomIndex = Random.Range(0, hit.Length);
        audio_.PlayOneShot(hit[randomIndex]);
    }

    public void PlayBubbles()
    {
        audio_.PlayOneShot(bubbles);
    }

    public void PlayLampfish()
    {
        int randomIndex = Random.Range(0, lampfish.Length);
        audio_.PlayOneShot(lampfish[randomIndex]);
    }

    public void PlayUnderwaterExplosion()
    {
        audio_.PlayOneShot(underwater_explosion);
    }

    public void PlaySpearSplash()
    {
        audio_.PlayOneShot(spear_splash, 0.5f);
    }

    public void PlayHuyas()
    {
        int randomIndex = Random.Range(0, huyas.Length);
        audio_.PlayOneShot(huyas[randomIndex]);
    }

    public void PlayWhale_alarm()
    {
        int randomIndex = Random.Range(0, whale_alarm.Length);
        audio_.PlayOneShot(whale_alarm[randomIndex]);
    }

    public void PlayWhale_comehere()
    {
        int randomIndex = Random.Range(0, whale_comehere.Length);
        audio_.PlayOneShot(whale_comehere[randomIndex]);
    }

    public void PlayBigWhale()
    {
        int randomIndex = Random.Range(0, bigWhale.Length);
        audio_.PlayOneShot(bigWhale[randomIndex]);
    }
}
