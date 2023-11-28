using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] public Animator anim;
    //[SerializeField] private HealthBar healthBar;
    //[SerializeField] private GameObject combatTextPrefab;

    protected float health;
    [SerializeField] protected float maxHealth = 100f;

    public bool IsDeath => health <= 0f;

    protected string currentAnim = "";

    public bool isDead { get; set; } = false;

    private void Start()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        health = 100f;
        //healthBar.OnInit(health);
    }

    public virtual void OnDespawn()
    {

    }

    protected virtual void OnDeath()
    {
        ChangeAnim("die");
        Invoke(nameof(OnDespawn), 1f);
        isDead = true;
    }

    protected void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            if (!string.IsNullOrEmpty(animName))
            {
                Debug.Log("play anim " + animName);

                anim.ResetTrigger(currentAnim);
                currentAnim = animName;
                anim.SetTrigger(currentAnim);
            }
            else
            {
                currentAnim = "";
            }
        }
    }

    public virtual void OnHit(float damage)
    {
        if (!IsDeath)
        {
            health -= damage;

            if (IsDeath)
            {
                health = 0;
                OnDeath();
            }
        }
    }

    public virtual void OnNewGame()
    {

    }

}
