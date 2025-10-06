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
    [SerializeField] float heightForHighAttacks;

    [Header("FLAME PYRES")]
    [SerializeField] GameObject pyreObj;
    [SerializeField] Transform[] pyrePositions;

    [Header("SPEAR THROW")]
    [SerializeField] GameObject trident;
    [SerializeField] float throwForce;

    [SerializeField] GameObject playerObj;
    
    int currentPos = 0;
    Rigidbody2D rb;
    Animator animator;

    bool makeDecision = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (makeDecision) 
        {
            int choice = Random.Range(0, 5);
            switch (choice) 
            {
                case 0:
                    StartCoroutine(SpiralFlameAttack());
                    StartCoroutine(WaitForNext(6f));
                    break;
                case 1:
                    if (transform.position.y < heightForHighAttacks) break;
                    StartCoroutine(ThreeFlamesAttack(3, .5f));
                    StartCoroutine(WaitForNext(2f));
                    break;
                case 2:
                    FlamePyre();
                    StartCoroutine(WaitForNext(4f));
                    break;
                case 3: //continues to next - 3 and 4 do the same attack
                case 4:
                    ThrowTrident();
                    StartCoroutine(WaitForNext(3f));
                    break;
                default:
                    Debug.Log("This shouldn't be happening");
                    StartCoroutine(WaitForNext(1f));
                    break;
            }
        }
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
        for (int i = 0; i < 24; i++) 
        {
            GameObject p = Instantiate(fireProj, transform.position, Quaternion.identity);
            Rigidbody2D pRigidBody = p.GetComponent<Rigidbody2D>();
            pRigidBody.AddForce(new Vector2(Mathf.Cos(Mathf.PI/6 * i), Mathf.Sin(Mathf.PI/6 * i)).normalized * projForce , ForceMode2D.Impulse);

            yield return new WaitForSeconds(timeBetweenShotsForSpin);
        }
    }

    IEnumerator ThreeFlamesAttack(int numBursts, float timeBetweenShots) 
    {
        for (int i = 0; i < numBursts; i++) 
        {
            GameObject a = Instantiate(fireProj, transform.position, Quaternion.identity);
            GameObject b = Instantiate(fireProj, transform.position, Quaternion.identity);
            GameObject c = Instantiate(fireProj, transform.position, Quaternion.identity);

            a.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(-3 * Mathf.PI / 4), Mathf.Sin(-3 * Mathf.PI / 4)).normalized * projForce, ForceMode2D.Impulse);
            b.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1) * projForce, ForceMode2D.Impulse);
            c.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(-1 * Mathf.PI / 4), Mathf.Sin(-1 * Mathf.PI / 4)).normalized * projForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(timeBetweenShots);
        }

        
    }

    void FlamePyre() 
    {
        int pos = Random.Range(0, pyrePositions.Length);
        Instantiate(pyreObj, pyrePositions[pos].position, Quaternion.identity);
        pos = Random.Range(0, pyrePositions.Length);
        Instantiate(pyreObj, pyrePositions[pos].position, Quaternion.identity);
        pos = Random.Range(0, pyrePositions.Length);
        Instantiate(pyreObj, pyrePositions[pos].position, Quaternion.identity);

    }

    void ThrowTrident() 
    {
        //Vector2 randomDir = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)); 
        //randomDir = randomDir + (Vector2)playerObj.transform.position; //roughly towards player with a bit of randomness
        //GameObject t = Instantiate(trident, transform.position, Quaternion.identity);
        //t.transform.up = randomDir;
        //t.GetComponent<Rigidbody2D>().AddForce(randomDir * throwForce, ForceMode2D.Impulse);

        Vector2 target = playerObj.transform.position - transform.position;
        target += new Vector2(Random.Range(-3,3), Random.Range(-3,3));
        GameObject t = Instantiate(trident, transform.position, Quaternion.identity);
        t.transform.up = target;
        t.GetComponent<Rigidbody2D>().AddForce(target * throwForce, ForceMode2D.Impulse);
    }

    IEnumerator WaitForNext(float timeToWait) 
    {
        makeDecision = false;
        yield return new WaitForSeconds(timeToWait);
        StartCoroutine(MoveToNextPosition());
        yield return new WaitForSeconds(2f);
        makeDecision = true;
    }


}
