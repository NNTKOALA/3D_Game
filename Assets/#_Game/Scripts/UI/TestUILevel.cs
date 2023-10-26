using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUILevel : MonoBehaviour
{
    public void SelectLevel(int id)
    {
        LevelManager.Ins.SpawnLevel(id);
    }
}
