using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Ins;

    public GameObject[] levelPrefabs;
    public Transform startPoint;
    public Player player;

    //public int currentLevel;

    private void Awake()
    {
        Ins = this;
    }

    private void Start()
    {
        //StartPoint();
    }

    public void SpawnLevel(int id)
    {
        GameManager.Instance.SpawnLevelById(id);      
    }

    public void StartPoint()
    {
        startPoint = levelPrefabs[1].GetComponent<LevelGame>().startPoint;
        player.gameObject.SetActive(true);
    }
}
