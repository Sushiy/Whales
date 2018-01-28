using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Networking;

public class MP_Health : NetworkBehaviour
{
    [SyncVar(hook = "OnChangeHealth")]
    public int health;

    int maxHealth = 5;

    private void Awake()
    {
        health = maxHealth;
    }

    [Command]
    public void CmdTakeDamage()
    {
        health--;
        if(health <= 0)
        {
            Debug.Log("You lose");

            Application.Quit();
        }

    }

    public void ServerTakeDamage()
    {
        health--;
        if (health <= 0)
        {
            Debug.Log("You lose");

            Application.Quit();
        }

    }

    public void OnChangeHealth(int currentHealth)
    {
        if(currentHealth < maxHealth)
            StartCoroutine(HealthFlash());
    }

    IEnumerator HealthFlash()
    {
        SpriteRenderer rend = GetComponent<SpriteRenderer>();
        rend.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        rend.color = Color.white;
        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isClient)
        {
            if (collision.gameObject.layer == 9)
            {
                CmdTakeDamage();
            }
        }

    }
}
