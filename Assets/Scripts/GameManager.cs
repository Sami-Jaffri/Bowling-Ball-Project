using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float score = 0;
    [SerializeField] private BallController ball;
    [SerializeField] private GameObject pinCollection;
    [SerializeField] private Transform pinAnchor;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private TextMeshProUGUI scoreText;

    private FallTrigger[] fallTriggers;
    private GameObject pinObjects;

    private void Start()
    {
        // Adding the HandleReset function as a listener to our OnResetPressed event
        inputManager.OnResetPressed.AddListener(HandleReset);
        SetPins();
    }

    public void HandleReset()
    {
        ball.ResetBall();
        SetPins();
    }

    private void SetPins()
    {
        // First, ensure that all the previous pins are destroyed so we don't have overlapping pins.
        if (pinObjects != null)
        {
            foreach (Transform child in pinObjects.transform)
            {
                Destroy(child.gameObject);
            }

            Destroy(pinObjects);
        }

        // Instantiate a new set of pins at the pinAnchor's position.
        pinObjects = Instantiate(pinCollection, pinAnchor.transform.position, Quaternion.identity, transform);

        // Add the IncrementScore function as a listener to the OnPinFall event for each new pin.
        fallTriggers = FindObjectsOfType<FallTrigger>(true);
        foreach (FallTrigger pin in fallTriggers)
        {
            pin.OnPinFall.AddListener(IncrementScore);
        }
    }

    private void IncrementScore()
    {
        score++;
        scoreText.text = $"Score: {score}";
    }
}
