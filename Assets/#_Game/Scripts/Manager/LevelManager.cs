using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Ins;

    public GameObject[] levelPrefabs;
    public Transform startPoint;
    public HeroController player;

    //public int currentLevel;

    private void Awake()
    {
        Ins = this;
    }

    private void Start()
    {
        StartPoin();
    }

    public void SpawnLevel(int id)
    {
        Instantiate(levelPrefabs[id].gameObject);
        
    }

    public void StartPoin()
    {
        startPoint = levelPrefabs[1].GetComponent<LevelGame>().startPoint;
        player.gameObject.SetActive(true);
    }
}
