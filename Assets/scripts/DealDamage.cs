using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] int damageToDeal = 15;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health h = collision.gameObject.GetComponent<Health>();
            if (h != null)
            {
                h.TakeDamage(damageToDeal);
            }
        }
    }

}