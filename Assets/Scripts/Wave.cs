using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {



    float currentRadius;
    float maxRadius;
    float spreadSpeed;

    bool stillSpreading;

    private void Awake()
    {

        currentRadius = 1.0f;
        maxRadius = 100.0f;
        spreadSpeed =0.8f;

        stillSpreading = true;

    }
    

    // Use this for initialization
    void Start () {

        StartCoroutine(SpreadWave());


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator SpreadWave()
    {
        while (true)
        {

            if(stillSpreading)
            {
                currentRadius += spreadSpeed;
                transform.localScale += new Vector3(currentRadius, currentRadius, 0);

                if (transform.localScale.x > maxRadius)
                {
                    stillSpreading = false;
                    Destroy(this.gameObject);
                }

                yield return new WaitForSeconds(spreadSpeed*0.01f);
            }
                
               yield return null;

        }
    }
}
