using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DeathState : IState
{

    public void OnEnter(Enemy enemy)
    {
        enemy.isDead = true;
        enemy.OnDespawn();

    }

    public void OnExecute(Enemy enemy)
    {

    }

    public void OnExit(Enemy enemy)
    {

    }
}
