using UnityEngine;
using System.IO;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject gameOverPanel;
    public TMP_Text finalScoreText;
    public ScoreManager scoreManager;

    private string filePath;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Define a relative path (inside the project folder)
        string folderName = "SaveFiles"; // Folder inside the project directory
        string fileName = "player_score.json"; // File name

        // Combine the folder and file name with the project directory
        filePath = Path.Combine(Application.dataPath, folderName, fileName);

        Debug.Log("File Path: " + filePath);
    }

    void Start()
    {
        gameOverPanel.SetActive(false);
    }
    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        if (finalScoreText != null && scoreManager != null)
        {
            finalScoreText.text = "Final Score: " + scoreManager.score.ToString();
            SaveScore(scoreManager.score);
        }
        Time.timeScale = 0f;
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void SaveScore(int currentScore)
    {
        PlayerScoreData data = LoadScoreData();

        if (currentScore > data.highScore)
        {
            data.highScore = currentScore;
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(filePath, json);
        }
    }

    private PlayerScoreData LoadScoreData()
    {
        PlayerScoreData data = new PlayerScoreData();

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<PlayerScoreData>(json);
        }
        return data;
    }

    public int GetHighScore()
    {
        PlayerScoreData data = LoadScoreData();
        return data.highScore;
    }
}