using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage;
    public float attackSpeed;
    public playerMovement player;
    public Timer t;

    // Update is called once per frame
    void Update()
    {
        if (t.ready)
        {
            Attack(damage, attackSpeed);
        }
    }

    private void Attack(float damage, float attackSpeed)
    {
        t.SetTimeRemaining(attackSpeed);
        player.health -= damage;
        Debug.Log(player.health);
    }
}
