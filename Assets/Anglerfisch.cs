using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anglerfisch : MonoBehaviour {

    [SerializeField]
    private float turnOnDuration = 1f;
    private bool lit = false;
    private SpriteRenderer lightRenderer;
    private float velocity;
    private float alpha = 0f;
    private Color color;

    private void Awake()
    {
        lightRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        color = lightRenderer.color;
    }

    void Update()
    {
        if (lit)
        {
            alpha = Mathf.SmoothDamp(alpha, 1f, ref velocity, turnOnDuration);
            lightRenderer.color = new Color(color.r, color.g, color.b, alpha);
        }
    }

	public void LightOn()
    {
        ClipPlayer.s_clipPlayer.PlayLampfish();
        lit = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (lit)
            return;
        if (other.gameObject.layer == 8)
        {
            LightOn();
        }
    }
}
