using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spinner : MonoBehaviour {
    private Rigidbody rigidbody;
    private bool isSpinning = false;
    [SerializeField] private TMP_Text spinnerText;
    [SerializeField] private SpinnerType spinnerType = SpinnerType.Random;
    private int currentNumber = 0;

    [Header("Deterministic Configuration")]
    [SerializeField, Range(1, 10)] private int determinatedNumber = 1;

    [Header("Probability Configuration")]

    [SerializeField] private ProbabilitySettings probabilitySettings;
    private bool isProbabilityNormalized = false;



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
            this.isProbabilityNormalized = false;
            return;
        }

        if (Keyboard.current.spaceKey.isPressed || !this.isSpinning) return;

        // Apply angular damping to slow down the spinning
        if (Keyboard.current.spaceKey.wasReleasedThisFrame) {
            if (this.rigidbody.angularDamping == 0f) {
                this.rigidbody.angularDamping = 1;
            }
        }

        switch (this.spinnerType) {

            case SpinnerType.Random:
                // Check if the object have stoped spinning
                CheckIfSpinnerStoped();

                break;

            case SpinnerType.Deterministic:
                // Deterministic behavior

                //if (this.currentNumber != this.determinatedNumber) {
                //    this.currentNumber = this.determinatedNumber;
                //    this.spinnerText.text = $"The object has stopped spinning. The number is: {this.currentNumber}";
                //}

                if (this.currentNumber != this.determinatedNumber && this.isSpinning) {
                    this.rigidbody.angularDamping = 0f;
                } else if (this.currentNumber == this.determinatedNumber && this.isSpinning) {
                    this.rigidbody.angularDamping = 1f;
                    CheckIfSpinnerStoped();
                }

                break;

            case SpinnerType.Probability:
                // Probability behavior

                if (!isProbabilityNormalized) {
                    this.probabilitySettings.NormalizeProbabilities();
                    isProbabilityNormalized = true;
                }

                this.determinatedNumber = this.probabilitySettings.SelectRandomNumberBasedOnProbabilitySettings();
                this.spinnerType = SpinnerType.Deterministic;


                break;

        }

    }

    private void CheckIfSpinnerStoped() {
        if (isSpinning && rigidbody.angularVelocity.y < 0.1f) {
            this.rigidbody.freezeRotation = true;
            isSpinning = false;

            this.spinnerText.text = "The object has stopped spinning.";
        }
    }
    [ContextMenu("Normalize Probabilities")]
    public void NormalizeProbabilities() => this.probabilitySettings.NormalizeProbabilities();
    
    [ContextMenu("Debug Probabilities")]
    public void DebugProbabilities() => this.probabilitySettings.DebugProbabilities();
    [System.Serializable]
    public class ProbabilitySettings {
        [SerializeField, Range(0f, 1f)] private float probabilityOne = 0.1f;
        [SerializeField, Range(0f, 1f)] private float probabilityTwo = 0.1f;
        [SerializeField, Range(0f, 1f)] private float probabilityThree = 0.1f;
        [SerializeField, Range(0f, 1f)] private float probabilityFour = 0.1f;
        [SerializeField, Range(0f, 1f)] private float probabilityFive = 0.1f;
        [SerializeField, Range(0f, 1f)] private float probabilitySix = 0.1f;
        [SerializeField, Range(0f, 1f)] private float probabilitySeven = 0.1f;
        [SerializeField, Range(0f, 1f)] private float probabilityEight = 0.1f;
        [SerializeField, Range(0f, 1f)] private float probabilityNine = 0.1f;
        [SerializeField, Range(0f, 1f)] private float probabilityTen = 0.1f;

        public float ProbabilityOne => CalculateMagnitude(probabilityOne);
        public float ProbabilityTwo => CalculateMagnitude(probabilityTwo);
        public float ProbabilityThree => CalculateMagnitude(probabilityThree);
        public float ProbabilityFour => CalculateMagnitude(probabilityFour);
        public float ProbabilityFive => CalculateMagnitude(probabilityFive);
        public float ProbabilitySix => CalculateMagnitude(probabilitySix);
        public float ProbabilitySeven => CalculateMagnitude(probabilitySeven);
        public float ProbabilityEight => CalculateMagnitude(probabilityEight);
        public float ProbabilityNine => CalculateMagnitude(probabilityNine);
        public float ProbabilityTen => CalculateMagnitude(probabilityTen);

        private float CalculateMagnitude(float value) {
            float totalValue = this.probabilityOne
                               + this.probabilityTwo
                               + this.probabilityThree
                               + this.probabilityFour
                               + this.probabilityFive
                               + this.probabilitySix
                               + this.probabilitySeven
                               + this.probabilityEight
                               + this.probabilityNine
                               + this.probabilityTen;

            return value / totalValue;
        }


        public void NormalizeProbabilities() {
            float totalValue = this.probabilityOne
                               + this.probabilityTwo
                               + this.probabilityThree
                               + this.probabilityFour
                               + this.probabilityFive
                               + this.probabilitySix
                               + this.probabilitySeven
                               + this.probabilityEight
                               + this.probabilityNine
                               + this.probabilityTen;
            probabilityOne /= totalValue;
            probabilityTwo /= totalValue;
            probabilityThree /= totalValue;
            probabilityFour /= totalValue;
            probabilityFive /= totalValue;
            probabilitySix /= totalValue;
            probabilitySeven /= totalValue;
            probabilityEight /= totalValue;
            probabilityNine /= totalValue;
            probabilityTen /= totalValue;
        }

        public void DebugProbabilities() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Probabilities:");
            sb.AppendLine($"Probability One: {probabilityOne}");
            sb.AppendLine($"Probability Two: {probabilityTwo}");
            sb.AppendLine($"Probability Three: {probabilityThree}");
            sb.AppendLine($"Probability Four: {probabilityFour}");
            sb.AppendLine($"Probability Five: {probabilityFive}");
            sb.AppendLine($"Probability Six: {probabilitySix}");
            sb.AppendLine($"Probability Seven: {probabilitySeven}");
            sb.AppendLine($"Probability Eight: {probabilityEight}");
            sb.AppendLine($"Probability Nine: {probabilityNine}");
            sb.AppendLine($"Probability Ten: {probabilityTen}");
            Debug.Log(sb.ToString());
        }

        public int SelectRandomNumberBasedOnProbabilitySettings() {
            float randomValue = Random.value;
            float cumulativeProbability = 0f;
            if (randomValue < (cumulativeProbability += ProbabilityOne)) {
                Debug.Log("Selected Number: 1");
                return 1;
            } else if (randomValue < (cumulativeProbability += ProbabilityTwo)) {
                Debug.Log("Selected Number: 2");
                return 2;
            } else if (randomValue < (cumulativeProbability += ProbabilityThree)) {
                Debug.Log("Selected Number: 3");
                return 3;
            } else if (randomValue < (cumulativeProbability += ProbabilityFour)) {
                Debug.Log("Selected Number: 4");
                return 4;
            } else if (randomValue < (cumulativeProbability += ProbabilityFive)) {
                Debug.Log("Selected Number: 5");
                return 5;
            } else if (randomValue < (cumulativeProbability += ProbabilitySix)) {
                Debug.Log("Selected Number: 6");
                return 6;
            } else if (randomValue < (cumulativeProbability += ProbabilitySeven)) {
                Debug.Log("Selected Number: 7");
                return 7;
            } else if (randomValue < (cumulativeProbability += ProbabilityEight)) {
                Debug.Log("Selected Number: 8");
                return 8;
            } else if (randomValue < (cumulativeProbability += ProbabilityNine)) {
                Debug.Log("Selected Number: 9");
                return 9;
            } else {
                Debug.Log("Selected Number: 10");
                return 10;
            }
        }

    }

    public enum SpinnerType { Random, Deterministic, Probability }
}
