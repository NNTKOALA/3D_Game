using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    float timer;
    float duration;

    public void OnEnter(Enemy enemy)
    {
        if (enemy.Target != null)
        {
            enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);
            enemy.StopMoving();
            enemy.Attack();
        }

        timer = 0f;
        duration = Random.Range(0.5f, 2f);
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
