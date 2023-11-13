using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    public void SelectLevel(int id)
    {
        LevelManager.Ins.SpawnLevel(id);
        GameManager.Instance.StartNewGame();

        gameObject.SetActive(false);
    }
}
