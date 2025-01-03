using UnityEngine;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    private const string ScoreFileName = "scoreData.json";

    public static ScoreManager Instance { get; private set; }

    public float CurrentScore { get; private set; }
    public int BestScore { get; private set; }
    public int LastScore { get; private set; }

    private float time;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScores();
    }

    void Update()
    {
        UpdateCurrentScore();
        CheckAndSaveBestScore();
    }

    private void UpdateCurrentScore()
    {
        time += Time.deltaTime;
        CurrentScore = time * 5;
    }

    public void EndGame()
    {
        LastScore = Mathf.FloorToInt(CurrentScore);
        SaveScores();
    }

    private void CheckAndSaveBestScore()
    {
        if (Mathf.FloorToInt(CurrentScore) > BestScore)
        {
            BestScore = Mathf.FloorToInt(CurrentScore);
        }
    }

    private void SaveScores()
    {
        ScoreData data = new ScoreData
        {
            BestScore = BestScore,
            LastScore = LastScore
        };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(GetFilePath(), json);
    }

    private void LoadScores()
    {
        string filePath = GetFilePath();
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            ScoreData data = JsonUtility.FromJson<ScoreData>(json);
            BestScore = data.BestScore;
            LastScore = data.LastScore;
        }
        else
        {
            BestScore = 0;
            LastScore = 0;
        }
    }

    private string GetFilePath()
    {
        return Path.Combine(Application.persistentDataPath, ScoreFileName);
    }

    [System.Serializable]
    private class ScoreData
    {
        public int BestScore;
        public int LastScore;
    }
}
