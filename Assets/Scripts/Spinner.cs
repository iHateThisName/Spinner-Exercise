using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spinner : MonoBehaviour {
    private Rigidbody rigidbody;
    private bool isSpinning = false;
    [SerializeField] private TMP_Text spinnerText;
    [SerializeField] private SpinnerType spinnerType = SpinnerType.Random;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        if (rigidbody == null) {
            rigidbody = GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update() {

        // Start Spinning
        if (Keyboard.current.spaceKey.wasPressedThisFrame) {
            this.rigidbody.freezeRotation = false;
            this.rigidbody.angularDamping = 0f;
            this.rigidbody.AddTorque(Vector3.up * 10f, ForceMode.Impulse);
            this.isSpinning = true;
            return;
        }

        if (Keyboard.current.spaceKey.isPressed) return;

        // Apply angular damping to slow down the spinning
        if (Keyboard.current.spaceKey.wasReleasedThisFrame) {
            if (this.rigidbody.angularDamping == 0f) {
                this.rigidbody.angularDamping = 1;
            }
        }

        if (this.spinnerType == SpinnerType.Random) {
            // Random behavior

            // Check if the object have stoped spinning
            if (isSpinning && rigidbody.angularVelocity.y < 0.1f) {
                this.rigidbody.freezeRotation = true;
                isSpinning = false;

                this.spinnerText.text = "The object has stopped spinning.";
            }
        }

    }
    public enum SpinnerType { Random, Deterministic, Probability }
}
