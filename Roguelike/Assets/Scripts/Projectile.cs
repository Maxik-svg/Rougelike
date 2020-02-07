﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Vector3 Player;

    public float damage;
    public float speed;
    private PlayerHP playerHP;



    void Start()
    {
        Player = GameObject.FindWithTag("Player").transform.position;
        damage = 1 * LevelGenerator.LVL;
        playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHP>();
    }


    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Player, speed * Time.deltaTime);
        if (transform.position == Player)
        {
            Destroy(gameObject, 2);
        }
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHP.TakingDamage(damage);
            Destroy(gameObject);
        }

        else if (other.gameObject.CompareTag("wall"))
            Destroy(gameObject);
    }
}
