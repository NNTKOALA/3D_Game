using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public List<GameObject> levelPrefab;
    public int currentLevel;
    private GameObject currentLevelInstance;

    public int point;


    [SerializeField] int initialAmountEnemy = 5;
    [SerializeField] float spawnCooldown = 5f;
    [SerializeField] int maxAmountPerSpawn = 8;
    [SerializeField] int minAmountPerSpawn = 3;
    [SerializeField] Player player;
    public Player MainPlayer => player;

    private float spawnTimer;
    private bool isPlaying;

    private string playerName;
    public string PlayerName => playerName;

    public void SetPlayerName(string newName)
    {
        playerName = newName;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        currentLevel = 0;
        currentLevelInstance = Instantiate(levelPrefab[currentLevel]);
        PauseGame();
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = -1;
    }

    private void Update()
    {

    }

    public void StartNewGame()
    {
        spawnTimer = spawnCooldown;
        isPlaying = true;
        ResumeGame();
        player.gameObject.SetActive(true);
        player.OnNewGame();
    }


    private void WinGame()
    {
        UIManager.Instance.SwitchToWinPanel();
        PauseGame();
    }

    public void PauseGame()
    {
        isPlaying = false;
    }

    public void ResumeGame()
    {
        isPlaying = true;
    }


    public void NextLevel()
    {
        Debug.Log("Next Level");
        currentLevel = ++currentLevel % levelPrefab.Count;
        Destroy(currentLevelInstance);
        currentLevelInstance = Instantiate(levelPrefab[currentLevel]);
        StartNewGame();
        UIManager.Instance.SwitchToIngameUI();
    }

}