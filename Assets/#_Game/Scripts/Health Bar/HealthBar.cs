using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider healthBar;

    private float health;
    private float maxHealth;

    void Update()
    {
        //transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        healthBar.value = Mathf.Lerp(healthBar.value, health / maxHealth, Time.deltaTime * 5f);
    }

    public void OnInit(float maxHealth)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;

        healthBar.value = 1f;
    }

    public void SetNewHealth(float health)
    {
        this.health = health;
    }
}
