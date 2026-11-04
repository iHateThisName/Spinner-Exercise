using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting2 : MonoBehaviour
{
    public InputAction pressButton;
    public GameObject bullet;

    //creates an area in which the bullets should be randomised
    [Header("Aiming Randomness")]
    public float spreadAngle = 5f;

    void Start()
    {
        pressButton.Enable();
    }

    void Update()
    {
        if (pressButton.triggered)
        {
            // generates a random range in which the bullets from the spreadAngle should be spawned
            float randomX = Random.Range(-spreadAngle, spreadAngle);
            float randomY = Random.Range(-spreadAngle, spreadAngle);

            Vector3 randomPosition = transform.position + new Vector3(randomX, randomY, 0f);

            // spawn the bullet with the given randomness
            Instantiate(bullet, randomPosition);
        }
    }
}