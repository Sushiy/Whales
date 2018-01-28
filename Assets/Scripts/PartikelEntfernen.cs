using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartikelEntfernen : MonoBehaviour
{
    ParticleSystem partikel;

	// Use this for initialization
	void Awake ()
    {
        partikel = GetComponent<ParticleSystem>();	
	}
	

	// Zerstöre dich selbst, sobald dein Partikelsystem zuende ist
	void Update ()
    {
        if (!partikel.IsAlive())
            Destroy(transform.root.gameObject);
	}
}
