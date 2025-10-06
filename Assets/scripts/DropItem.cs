using System.Collections;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] GameObject objToDrop;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            StartCoroutine(dropObj());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ground")) 
        {
            StartCoroutine(dropObj());
        }
    }

    IEnumerator dropObj() 
    {
        yield return new WaitForSeconds(.05f);
        GameObject a = Instantiate(objToDrop,transform.position, Quaternion.identity);
        a.transform.rotation = transform.rotation;
        Destroy(gameObject);
    }

}
