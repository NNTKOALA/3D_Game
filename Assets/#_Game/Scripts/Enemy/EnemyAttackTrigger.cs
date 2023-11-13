using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTrigger : MonoBehaviour
{
    [SerializeField] Enemy thisEnemy;

    private void Awake()
    {
        if(thisEnemy == null)
        {
            thisEnemy = GetComponentInParent<Enemy>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Player>(out var player))
        {
            Debug.Log("attack");
            thisEnemy.Target = player;
            thisEnemy.ChangeState(new AttackState());
            player.OnHit(thisEnemy.Damage);
        }
    }
}
