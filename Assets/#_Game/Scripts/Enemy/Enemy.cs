using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Character
{
    //[SerializeField] float attackRange;
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody rb;
    //[SerializeField] GameObject attackArea;

    private IState currentState;
    private bool isRight;
    private Character target;

    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;

    public Character Target
    {
        get => target;
        set
        {
            target = value;

            /*            if (IsTargetInRange())
                        {
                            ChangeState(new AttackState());
                        }
                        else if (target != null)
                        {
                            ChangeState(new PatrolState());
                        }
                        else
                        {
                            ChangeState(new IdleState());
                        }*/
        }
    }

    private void Update()
    {
        if (currentState != null && !IsDeath)
        {
            currentState.OnExecute(this);
        }
    }

    public override void OnInit()
    {
        base.OnInit();

        currentHealth = maxHealth;

        ChangeState(new IdleState());
        isRight = true;
    }

    public override void OnDespawn()
    {
        base.OnDespawn();

        Destroy(this.gameObject);
    }

    protected override void OnDeath()
    {
        ChangeState(null);
        base.OnDeath();
    }

    public void Moving()
    {
        ChangeAnim("run");

        rb.velocity = transform.right * moveSpeed;
    }

    public void StopMoving()
    {
        ChangeAnim("idle");

        rb.velocity = Vector3.zero;
    }

    public void Attack()
    {
        ChangeAnim("attack");
        ActiveAttack();

        Invoke(nameof(DeactiveAttack), 0.5f);
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Debug.Log("EnemyDie");

            Destroy(gameObject);
                //Die State
            }
        }
    }

    /*    public bool IsTargetInRange()
        {
            if (target != null && Vector3.Distance(transform.position, target.transform.position) <= attackRange)
                return true;

            return false;
        }*/

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("EnemyWall"))
        {
            ChangeDirection(!isRight);
        }
    }

    public void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;

        transform.rotation = isRight ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
    }

    private void ActiveAttack()
    {
        //attackArea.SetActive(true);
    }

    private void DeactiveAttack()
    {
        //attackArea.SetActive(false);
    }
}
