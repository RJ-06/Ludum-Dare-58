using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] GameObject objToDrop;
    [SerializeField] int damageToDeal;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            GameObject a = Instantiate(objToDrop, transform.position, Quaternion.identity);
            a.transform.rotation = transform.rotation;
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("player")) 
        {
            Health h = collision.gameObject.GetComponent<Health>();
            if (h != null) 
            {
                h.TakeDamage(damageToDeal);
            }
        }
    }

}
