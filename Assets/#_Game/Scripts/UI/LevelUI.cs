using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public List<Button> levelButtonList;

    private void Start()
    {
        int maxLevel = PlayerPrefs.GetInt(GameManager.PREF_MAX_LEVEL, 0);

        for(int i = 0; i < levelButtonList.Count; i++)
        {
            if(i <= maxLevel)
            {
                levelButtonList[i].interactable = true;
            }
            else
            {
                levelButtonList[i].interactable = false;
            }
        }

    }

    public void SelectLevel(int id)
    {
        LevelManager.Ins.SpawnLevel(id);
        GameManager.Instance.ResetSavePoint();
        GameManager.Instance.StartNewGame();

        gameObject.SetActive(false);
    }
}
