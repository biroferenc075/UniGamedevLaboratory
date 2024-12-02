using Assets.Scripts.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] bool topView;
    [SerializeField] PlayerMovementController playerController;
    [SerializeField] float multiplierDuration = 15f;
    [SerializeField] int killsRequired = 3;
    [SerializeField] GameObject endScreen;
    [SerializeField] GameObject gameOverScreen;
    public static GameStateManager Instance { get; private set; }

    private int score = 0;
    private int multiplier = 1;

    private int killCount = 0;
    private float multiplierTimer = 0f;

    private GameObject player;

    private int killedEnemies = 0;
    private int totalEnemies = 0;

    private int rawScore = 0;

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
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        MusicPlayer.Instance.PlayMusic();
    }

    void Update()
    {
        if (IsGameOver())
            GameOver();

        multiplierTimer -= Time.deltaTime;
        if(multiplierTimer < 0f )
        {
            multiplier = 1;
        }
    }

    public bool isTopView()
    {
        return topView;
    }

    public void EnemySpawned(int num)
    {
        totalEnemies+=num;
    }

    public void ItemPickedUp(int score)
    {
        multiplierTimer = multiplierDuration;
        multiplier = Math.Min(multiplier + 1, 5);

        this.score += score * multiplier;
    }

    public void EnemyKilled(int score)
    {
        multiplierTimer = multiplierDuration;
        if (killCount++ >= killsRequired)
        {
            multiplier = Math.Min(multiplier + 1, 5);
            killCount = 0;
        }

        if(score > 0)
        {
            killedEnemies++;
            this.score += score * multiplier;
            rawScore += score;
        }
    }

    public int GetScore() => score;
    public int GetMultiplier() => multiplier;

    public int GetKilledEnemies() => killedEnemies;
    public int GetTotalEnemies() => totalEnemies;

    public int GetRawScore() => rawScore;

    public void PlayerHit()
    {
        multiplier = 1;
        score = Math.Max(0, score-250);
    }

    public bool IsGameOver()
    {
        return player.GetComponent<Player>().GetHealth() <= 0;
    }

    internal void EndLevel()
    {
        player.GetComponent<Collider>().enabled = false;
        StartCoroutine(handleEndLevel());
    }

    internal void GameOver()
    {
        MusicPlayer.Instance.StopMusic();
        StartCoroutine(handleGameOver());
    }

    IEnumerator handleEndLevel()
    {
        yield return new WaitForSeconds(3);
        yield return endScreen.GetComponent<LevelEnd>().Show();
    }

    IEnumerator handleGameOver()
    {
        yield return new WaitForSeconds(1);
        gameOverScreen.GetComponent<GameOver>().Show();
    }
}
