using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;

    //путь
    private List<Vector2> PathToPlayer = new List<Vector2>();
    private PathFinder PathFinder;
    private bool isMooving;
    private GameObject Player;
    public float speed;


    //атака
    private float damage;
    private PlayerHP playerHP;
    public Transform attackPos;
    public float attackRange;
    public float startTimeBtwAttac;
    private float timeBtwAttac = 0;
    public LayerMask whatIsEnemies;
    public GameObject HealthPotion, Scroll, Soull;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player != null)
        {
            PathFinder = GetComponent<PathFinder>();
            PathToPlayer = PathFinder.GetPath(Player.transform.position);
            isMooving = true;
        }
        damage = 3 * LevelGenerator.LVL;
        playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHP>();
        health = 4.5f * LevelGenerator.LVL;
        speed = Random.Range(1f, 3f);
    }


    void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        if (Player == null) return;

        if (PathToPlayer.Count == 0 && Vector2.Distance(transform.position, Player.transform.position) > 0.6f)
        {
            PathToPlayer = PathFinder.GetPath(Player.transform.position);
            
            isMooving = true;
        }
        if (PathToPlayer.Count == 0)
        {

            Collider2D PlayerToDamage = Physics2D.OverlapCircle(attackPos.position, attackRange, whatIsEnemies);
            if (PlayerToDamage != null)
            {

                if (timeBtwAttac <= 0)
                {
                    playerHP.TakingDamage(damage);
                    timeBtwAttac = startTimeBtwAttac;
                }
                else
                {
                    timeBtwAttac -= Time.deltaTime;
                }
            }
            return;
        }


        if (isMooving)
        {
            if (Vector2.Distance(transform.position, PathToPlayer[PathToPlayer.Count - 1]) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position,new Vector3( PathToPlayer[PathToPlayer.Count - 1].x, PathToPlayer[PathToPlayer.Count - 1].y, -91), speed * Time.deltaTime);
                
            }
            else
            {
                isMooving = false;
            }
        }
        else
        {
            PathToPlayer = PathFinder.GetPath(Player.transform.position);
            isMooving = true;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        print("Fay-Fay");
        if (health <= 0)
        {
            Vector3 itemDropPos;
            int r = (int)Random.Range(0f, 4f);

            if (r == 0)
            {
                itemDropPos = new Vector3(transform.position.x + Random.Range(-0.25f, 0.25f), transform.position.y + Random.Range(-0.25f, 0.25f), -87);
                Instantiate(HealthPotion, itemDropPos, Quaternion.identity);
            }

            else if (r == 1)
            {
                itemDropPos = new Vector3(transform.position.x + Random.Range(-0.25f, 0.25f), transform.position.y + Random.Range(-0.25f, 0.25f), -87);
                Instantiate(Scroll, itemDropPos, Quaternion.identity);
            }

            else if (r == 2)
            {
                itemDropPos = new Vector3(transform.position.x + Random.Range(-0.25f, 0.25f), transform.position.y + Random.Range(-0.25f, 0.25f), -87);
                Instantiate(Soull, itemDropPos, Quaternion.identity);
            }

            else if (r == 3)
            {
                itemDropPos = new Vector3(transform.position.x + Random.Range(-0.25f, 0.25f), transform.position.y + Random.Range(-0.25f, 0.25f), -87);
                Instantiate(HealthPotion, itemDropPos, Quaternion.identity);
            }
            GameObject.FindGameObjectWithTag("levelGenerator").GetComponent<LevelGenerator>().DecreaseMobCountOnLvl();
            Destroy(gameObject);
        }
    }
}
