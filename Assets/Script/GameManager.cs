using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Speed Settings")]
    public float baseSpeed = 5f;
    public float maxSpeed = 10f;
    public float speedIncreasePerPoint = 0.1f;

    [Header("UI References")]
    public SpriteScore scoreDisplay, finalScoreDisplay, bestScoreDisplay;
    public GameObject gameOverPanel, newBadge;
    public GameObject bronze, silver, gold;

    private int score = 0;
    private int bestScore = 0;
    private bool isAlive = true;

    void Awake()
    {
        instance = this;
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }

    public float GetCurrentSpeed()
    {
        return Mathf.Min(baseSpeed + score * speedIncreasePerPoint, maxSpeed);
    }
    public void AddScore()
    {
        if (!isAlive) return; 
        score++;
        scoreDisplay.SetScore(score);
        AudioManager.instance.PlayScore();

        foreach (var pipe in FindObjectsOfType<PipeMove>(true))
            pipe.speed = GetCurrentSpeed();
        // Cập nhật tốc độ spawn
        UpdateSpawnInterval();
         
    }

    void UpdateSpawnInterval()
    {
        PipeSpawner spawner = FindObjectOfType<PipeSpawner>();
        if (spawner == null) return;

        // Spawn nhanh dần theo điểm, tối thiểu 1.2 giây
        spawner.spawnInterval = Mathf.Max(1.8f - score * 0.03f, 1.2f);
    }

    public void GameOver()
    {
        if(!isAlive) return;
        isAlive = false;
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
            newBadge.SetActive(true);
        }
        else
        {
            newBadge.SetActive(false);
        }
        if (score >= 10) bronze.SetActive(true);
        if (score >= 30) silver.SetActive(true);
        if (score >= 50) gold.SetActive(true);
        Time.timeScale = 0f;
        StartCoroutine(ShowGameOver());
    }
    IEnumerator ShowGameOver()
    {
        // Chờ 1 frame để score cập nhật xong
        yield return null;
        finalScoreDisplay.SetScore(score);
        bestScoreDisplay.SetScore(bestScore);
        gameOverPanel.SetActive(true);
    }
    public void ReStart()
    {
        AudioManager.instance.PlaySwoosh();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        if (!isAlive && Input.GetKeyDown(KeyCode.Space))
        {
            ReStart();
        }
    }
    
}