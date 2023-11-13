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
        Invoke(nameof(OnDespawn), 5f);
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

            //healthBar.SetNewHealth(health);

            /*            GameObject text = Instantiate(combatTextPrefab, transform.position + Vector3.up, Quaternion.identity);
                        text.GetComponent<CombatText>().OnInit(damage);*/
        }
    }

    public virtual void OnNewGame()
    {

    }

    public virtual void OnDead()
    {
        isDead = true;
    }

}
