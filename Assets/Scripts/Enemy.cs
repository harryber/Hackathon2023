using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage;
    public float attackSpeed;
    public playerMovement player;
    GameObject playerObject;
    public Timer t;
    private void Start()
    {
        playerObject = GameObject.Find("character (test)");
        player = playerObject.GetComponent<playerMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(t.timeRemaining);
        if (t.ready)
        {
            Attack(damage, attackSpeed);
        }
    }

    private void Attack(float damage, float attackSpeed)
    {
        t.start = true;
        t.SetTimeRemaining(attackSpeed);
        player.AdjustHealth(-damage);
        Debug.Log(player.health);
    }
}
