using UnityEngine;

public class DestroyAfterTimer : MonoBehaviour
{
    [SerializeField] float timing = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, timing);
    }
}
