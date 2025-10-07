using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillBoss : MonoBehaviour
{
    Health health;
    bool canTakeDamage = true;

    private void Start()
    {
        health = GetComponent<Health>();
    }

    private void Update()
    {
        if (health.GetHealth() <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("spear") && canTakeDamage)
        {
            health.TakeDamage(10);
            Health spearHealth = collision.gameObject.GetComponent<Health>();
            spearHealth.TakeDamage(5);
            if (spearHealth.GetHealth() <= 0)
            {
                Destroy(collision.gameObject);
            }
            StartCoroutine(waitForTakeDamageAgain());
        }
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(5);
        }
    }

    IEnumerator waitForTakeDamageAgain() 
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(.2f);
        canTakeDamage = true;
    }
}
