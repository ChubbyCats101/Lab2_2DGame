using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;

    protected Rigidbody2D rb;
    protected Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        //animator => Enemy hurt
        if (health < 0)
        {
            //animator => Enemy dead
            Destroy(gameObject);
        }
    }
}
