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

    public void Start()
    {

        SwitchToMainMenuUI();
    }

    public void SetPlayerName(string playerName)
    {
        GameManager.Instance.SetPlayerName(playerName);
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
    }

    public void SwitchTo(GameObject ui)
    {
        DeactiveAll();
        ui.gameObject.SetActive(true);
    }

    public void SwitchToMainMenuUI()
    {
        SwitchTo(menuPanel);

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

}