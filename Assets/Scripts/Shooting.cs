using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public InputAction pressButton;
    public GameObject bullet;

    void Start()
    {
        pressButton.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (pressButton.triggered)
        {
            Instantiate(bullet, this.transform.position, this.transform.rotation);
        }
    }
}
