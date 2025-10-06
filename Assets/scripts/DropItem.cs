using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] GameObject objToDrop;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            GameObject a = Instantiate(objToDrop, transform.position, Quaternion.identity);
            a.transform.rotation = transform.rotation;
            Destroy(gameObject);
        }
    }

}
