using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] GameObject ingameUI;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject exitPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject levelmanagerPanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;
    [SerializeField] GameObject loadingPanel;
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject infoPanel;
    [SerializeField] TextMeshProUGUI pointCount;


    public void Awake()
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

    public void LoadingSceneCoroutine(float delay)
    {
        StartCoroutine(LoadingRoutine(delay));
    }

    private IEnumerator LoadingRoutine(float delay)
    {
        loadingPanel.SetActive(true);
        GameManager.Instance.SpawnQuestLevel();

        yield return new WaitForSeconds(delay);

        loadingPanel.SetActive(false);
    }

    public void Start()
    {

        SwitchToMainMenuUI();
    }

    public void ResetGame()
    {
        
    }

    public void NewGame()
    {
        SwitchToIngameUI();
        GameManager.Instance.StartNewGame();
    }

    public void DeactiveAll()
    {
        ingameUI.SetActive(false);
        menuPanel.SetActive(false);
        exitPanel.SetActive(false);
        pausePanel.SetActive(false);
        levelmanagerPanel.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        loadingPanel.SetActive(false);
        settingPanel.SetActive(false);
        infoPanel.SetActive(false);
    }

    public void SwitchTo(GameObject ui)
    {
        DeactiveAll();
        ui.gameObject.SetActive(true);
    }

    public void SwitchToMainMenuUI()
    {
        SwitchTo(menuPanel);
        SoundManager.Instance.PlayMenuSound();

    }
    public void UpdatePoint()
    {
        pointCount.text = GameManager.Instance.point.ToString();
    }

    public void SwitchToIngameUI()
    {
        SwitchTo(ingameUI);
    }

    public void SwitchToLevelManagerUI()
    {
        SwitchTo(levelmanagerPanel);
    }

    public void SwitchToExitPanel()
    {
        SwitchTo(exitPanel);
    }

    public void SwitchToPausePanel()
    {
        SwitchTo(pausePanel);
    }

    public void SwitchToWinPanel()
    {
        SwitchTo(winPanel);
    }

    public void SwitchToLosePanel()
    {
        SwitchTo(losePanel);
    }

    public void SwitchToLoadingPanel()
    {
        SwitchTo(loadingPanel);
    }
    public void SwitchToSettingPanel()
    {
        SwitchTo(settingPanel);
    }
    public void SwitchToInfoPanel()
    {
        SwitchTo(infoPanel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}