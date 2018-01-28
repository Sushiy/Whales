using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceBreachTrigger : MonoBehaviour
{
    public GameObject prefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            ClipPlayer.s_clipPlayer.PlaySurface();
            GameObject g = Instantiate(prefab, other.transform.position + new Vector3(0, 2.0f, 0), Quaternion.Euler(-90,0,0));
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            ClipPlayer.s_clipPlayer.PlaySurface();
            GameObject g = Instantiate(prefab, other.transform.position + new Vector3(0, 1.0f,0), Quaternion.Euler(-90, 0, 0));
        }
    }
}
