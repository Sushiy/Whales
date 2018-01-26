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

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            PlayWhale1();
        if (Input.GetKeyDown(KeyCode.Alpha2))
            PlaySurface();
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
}
