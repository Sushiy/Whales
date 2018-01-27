using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BigBlueGuides : MonoBehaviour
{
    public static List<BigBlueGuides> instances;

    public ReactiveProperty<Vector2> worldPosition;

	// Use this for initialization
	void Awake ()
    {
        if (instances == null)
            instances = new List<BigBlueGuides>();
        instances.Add(this);

        worldPosition = new ReactiveProperty<Vector2>();

	}
	
	// Update is called once per frame
	void Update ()
    {
        worldPosition.Value = new Vector2(transform.position.x, transform.position.y);
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
