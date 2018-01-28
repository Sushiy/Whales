using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceBreachTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 8)
            ClipPlayer.s_clipPlayer.PlaySurface();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
            ClipPlayer.s_clipPlayer.PlaySurface();
    }
}
