using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public const string PREF_MAX_LEVEL = "max_level";

    public static GameManager Instance { get; private set; }
    public List<LevelGame> mainLevelPrefab;
    public List<LevelGame> questLevelPrefab;
    public int currentLevel;
    private LevelGame currentLevelInstance;

    public int point;

    [SerializeField] float spawnCooldown = 5f;
    [SerializeField] Player player;
    public Player MainPlayer => player;

    private float spawnTimer;
    private bool isPlaying;

    private string playerName;
    public string PlayerName => playerName;

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
        player.Rb.isKinematic = true;
        //currentLevelInstance = Instantiate(levelPrefab[currentLevel]);
        //player.transform.position = currentLevelInstance.startPoint.position;
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
        player.Rb.isKinematic = false;
    }

    public void ResetSavePoint()
    {
        player.SetSavePoint(currentLevelInstance.startPoint.position);
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
        currentLevel = ++currentLevel % mainLevelPrefab.Count;

        int maxLevel = PlayerPrefs.GetInt(PREF_MAX_LEVEL, 0);
        if(currentLevel > maxLevel)
        {
            PlayerPrefs.SetInt(PREF_MAX_LEVEL, currentLevel);
        }

        Destroy(currentLevelInstance.gameObject);
        currentLevelInstance = Instantiate(mainLevelPrefab[currentLevel]);
        ResetSavePoint();
        player.transform.position = currentLevelInstance.startPoint.position;
        StartNewGame();
        UIManager.Instance.SwitchToIngameUI();
    }

    public void SpawnLevelById(int id)
    {
        currentLevel = id;
        if(currentLevelInstance != null)
        {
            Destroy(currentLevelInstance.gameObject);
        }
        currentLevelInstance = Instantiate(mainLevelPrefab[currentLevel]);
        player.transform.position = currentLevelInstance.startPoint.position;
        ResetSavePoint();
        StartNewGame();
        UIManager.Instance.SwitchToIngameUI();
    }

    public void SpawnQuestLevel()
    {
        int randomIndex = UnityEngine.Random.Range(0, questLevelPrefab.Count);
        if (currentLevelInstance != null)
        {
            Destroy(currentLevelInstance.gameObject);
        }
        currentLevelInstance = Instantiate(questLevelPrefab[randomIndex]);
        player.transform.position = currentLevelInstance.startPoint.position;
        ResetSavePoint();
        StartNewGame();
        UIManager.Instance.SwitchToIngameUI();
    }
}