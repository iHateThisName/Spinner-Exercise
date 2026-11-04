using UnityEngine;

public class Forward : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float speed = 10f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 0.1)
        {
            
            transform.Translate(speed * Time.deltaTime * Vector3.forward);
        }
    }
}
