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
    [SerializeField] float damage = 10f;
    [SerializeField] int currentHealth;

    public int point;
    public float Damage => damage;

    private IState currentState;
    private bool isRight;
    private Character target;


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

        currentHealth = Mathf.RoundToInt(maxHealth);
        point = currentHealth;
        ChangeState(new IdleState());
        isRight = true;
        Debug.Log("OnInit");
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
        //ChangeAnim("run");
        anim.SetBool("isMoving", true);

        rb.velocity = (isRight ? 1 : -1) * Vector3.right * moveSpeed;
    }

    public void StopMoving()
    {
        anim.SetBool("isMoving", false);
        //ChangeAnim("idle");

        rb.velocity = Vector3.zero;
    }

    public void Attack()
    {
        StopMoving();
        ChangeAnim("attack");
        ActiveAttack();

        Invoke(nameof(DeactiveAttack), 0.5f);
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            ChangeAnim("hit");

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Debug.Log("EnemyDie");
                //Die State
                //Goi anim die
                ChangeAnim("die");
                StartCoroutine(DestroyEnemy());
                GameManager.Instance.point += point;
                UIManager.Instance.UpdatePoint();

            }
        }
    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
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
            Debug.Log("Hit wall");
        }
    }

    public void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;

        transform.rotation = isRight ? Quaternion.Euler(0, 90, 0) : Quaternion.Euler(0, 270, 0);
    }

    private void ActiveAttack()
    {

    }

    private void DeactiveAttack()
    {
        ChangeAnim("");
        Target = null;
    }
}
