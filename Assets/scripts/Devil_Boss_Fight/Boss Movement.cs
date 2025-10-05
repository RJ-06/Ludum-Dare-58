using System.Collections;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [Header("BASIC MOVEMENT")]
    [SerializeField] float moveSpeed;
    [SerializeField] Transform[] possiblePositions;

    [Header("PROJECTILE FIRING ATTACKS")]
    [SerializeField] GameObject fireProj;
    [SerializeField] float projForce;
    [SerializeField] float timeBetweenShotsForSpin;

    [Header("FLAME PYRES")]
    [SerializeField] GameObject pyreObj;
    [SerializeField] Transform[] pyrePositions;

    [Header("SPEAR THROW")]
    [SerializeField] GameObject trident;
    [SerializeField] float throwForce;
    
    int currentPos = 0;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MoveToNextPosition() 
    {
        int a = Random.Range(0, possiblePositions.Length);
        while (a == currentPos) { a = Random.Range(0, possiblePositions.Length);} //make sure pos is different
        currentPos = a;
        rb.linearVelocity = (possiblePositions[currentPos].position - transform.position).normalized * moveSpeed;

        yield return new WaitUntil(() => Vector2.Distance(possiblePositions[currentPos].position, transform.position) < .1f);
        rb.linearVelocity = Vector2.zero;
    }

    IEnumerator SpiralFlameAttack() 
    {
        for (int i = 0; i < 12; i++) 
        {
            GameObject p = Instantiate(fireProj);
            Rigidbody2D pRigidBody = p.GetComponent<Rigidbody2D>();
            pRigidBody.AddForce(new Vector2(Mathf.Cos(30 * i), Mathf.Sin(30 * i)) , ForceMode2D.Impulse);

            yield return new WaitForSeconds(timeBetweenShotsForSpin);
        }
    }

    void ThreeFlamesAttack() 
    {
        GameObject a = Instantiate(fireProj);
        GameObject b = Instantiate(fireProj);
        GameObject c = Instantiate(fireProj);

        a.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(-3 * Mathf.PI / 4), Mathf.Sin(-3 * Mathf.PI / 4)).normalized * projForce, ForceMode2D.Impulse);
        b.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,-1) * projForce, ForceMode2D.Impulse);
        a.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(-1 * Mathf.PI / 4), Mathf.Sin(-1 * Mathf.PI / 4)).normalized * projForce, ForceMode2D.Impulse);
    }

    void FlamePyre() 
    {
        int pos = Random.Range(0, pyrePositions.Length);
        Instantiate(pyreObj, pyrePositions[pos].position, Quaternion.identity);
    }

    void ThrowTrident() 
    {
        Vector2 randomDir = new Vector2(Random.Range(-1, 1), Random.Range(-1, 0)).normalized; //any downward angle
        GameObject t = Instantiate(trident);
        t.transform.LookAt(randomDir);
        t.GetComponent<Rigidbody2D>().AddForce(randomDir * throwForce, ForceMode2D.Impulse);
    }


}
