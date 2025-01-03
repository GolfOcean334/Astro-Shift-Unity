
using UnityEngine;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    private const string BestScoreFileName = "bestScore.json";

    public static ScoreManager Instance { get; private set; }

    public float CurrentScore { get; private set; }
    public int BestScore { get; private set; }

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
        LoadBestScore();
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

    private void CheckAndSaveBestScore()
    {
        if (Mathf.FloorToInt(CurrentScore) > BestScore)
        {
            BestScore = Mathf.FloorToInt(CurrentScore);
            SaveBestScore(BestScore);
        }
    }

    private void SaveBestScore(int bestScore)
    {
        BestScoreData data = new BestScoreData { BestScore = bestScore };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(GetFilePath(), json);
    }

    private void LoadBestScore()
    {
        string filePath = GetFilePath();
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            BestScoreData data = JsonUtility.FromJson<BestScoreData>(json);
            BestScore = data.BestScore;
        }
        else
        {
            BestScore = 0;
        }
    }

    private string GetFilePath()
    {
        return Path.Combine(Application.persistentDataPath, BestScoreFileName);
    }

    [System.Serializable]
    private class BestScoreData
    {
        public int BestScore;
    }
}