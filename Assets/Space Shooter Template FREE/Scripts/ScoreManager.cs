using UnityEngine;
using TMPro; // Required for working with TextMeshPro

public class ScoreManager : MonoBehaviour
{
    // Public static instance to access the ScoreManager from anywhere
    public static ScoreManager Instance;

    // The current score
    public int score = 0;

    // Reference to the TextMeshPro UI element to display the score
    public TMP_Text scoreText; // Drag your TextMeshPro element here in the Inspector

    void Awake()
    {
        // Ensure there is only one instance of the ScoreManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate instances
        }
    }

    void Start()
    {
        // Initialize the score text
        UpdateScoreText();
    }

    // Method to add points to the score
    public void AddPoints(int points)
    {
        score += points;
        UpdateScoreText();
    }

    // Method to reset the score
    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    // Method to update the score text on the UI
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
        else
        {
            Debug.LogWarning("TextMeshPro Score Text UI element is not assigned!");
        }
    }
}