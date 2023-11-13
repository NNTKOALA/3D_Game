using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DeathState : IState
{
    float timer;
    float duration;

    public void OnEnter(Enemy enemy)
    {
        enemy.isDead = true;
        enemy.OnDead();
        duration = Random.Range(1f, 3f);
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;

        if (timer > duration)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Enemy enemy)
    {

    }
}
