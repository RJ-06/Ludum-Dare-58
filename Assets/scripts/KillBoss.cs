using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillBoss : MonoBehaviour
{
    Health health;

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
}
