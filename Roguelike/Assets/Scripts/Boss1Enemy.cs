using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss1Enemy : MonoBehaviour
{
    public float currentHP;
    public float maxHP;

    //путь
    private List<Vector2> PathToPlayer = new List<Vector2>();
    private PathFinder PathFinder;
    private bool isMooving;

    public GameObject Player;

    public float speed;
    public Slider slider;
    public float offset;
    public GameObject womenBeam;
    public GameObject womenBeamSplash;

    public float startTimeToSplashAttack;
    float timeToSplashAttack;

    //атака
    private float damage;
    private PlayerHP playerHP;
    public float attackRange;
    public float startTimeBtwAttac;
    private float timeBtwAttac = 0;
    public LayerMask whatIsEnemies;
    public GameObject HealthPotion, Scroll, Soull, GAmulet, BAmulet, YAmulet;
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
        maxHP = currentHP = 40 * LevelGenerator.LVL;
        speed = Random.Range(1f, 3f);
        DisplayHP();
    }


    void Update()
    {
        

        if (Player == null) return;

        if (timeBtwAttac <= 0)
        {
            //SplashShoot(transform);
            Shoot(Player.transform);
            timeBtwAttac = startTimeBtwAttac;
        }
        else
        {
            timeBtwAttac -= Time.deltaTime;
        }
        //подходит на 4 и стоит
        if (Vector2.Distance(transform.position, Player.transform.position) > 4f) 
        {
            PathToPlayer = PathFinder.GetPath(Player.transform.position);

            isMooving = true;
        }
        else
        {
            isMooving = false;
        }
        //если ты под ангелком стоишь больше старт секунд и расстояние меньше 1.5 то на голову сплеш
        if (Vector2.Distance(transform.position, Player.transform.position) < 1.5f)
        {
            timeToSplashAttack += Time.deltaTime;
            if (timeToSplashAttack >= startTimeToSplashAttack)
            {
                SplashShoot(transform);
                timeToSplashAttack = 0;
            }
        }
        else
        {
            timeToSplashAttack = 0;
        }


        if (isMooving)
        {
            if (Vector2.Distance(transform.position, PathToPlayer[PathToPlayer.Count - 1]) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(PathToPlayer[PathToPlayer.Count - 1].x, PathToPlayer[PathToPlayer.Count - 1].y, -91), speed * Time.deltaTime);

            }
            else
            {
                isMooving = false;
            }
        }
        else
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            PathToPlayer = PathFinder.GetPath(Player.transform.position);
            isMooving = true;
        }


    }
    void Shoot(Transform attackPos)
    {
        Instantiate(womenBeam,new Vector3( attackPos.transform.position.x, attackPos.transform.position.y + offset, -95), Quaternion.identity);
    }

    void SplashShoot(Transform attackPos)
    {
        Instantiate(womenBeamSplash, new Vector3(attackPos.transform.position.x, attackPos.transform.position.y, -95), Quaternion.identity);
    }
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(attackPos.position, attackRange);
    //}
    private void DisplayHP()
    {
        float HPSlider = currentHP / maxHP;
        slider.value = HPSlider;
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        print("Fay-Fay");
        DisplayHP();
        if (currentHP <= 0)
        {
            AmuletBuff.countDeadMobs += 5;

            for (int i = 0; i < 5; i++)
            {
                Vector3 itemDropPos;
                float r = (int)Random.Range(0f, 4f);

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
            }
            Vector3 itemDropPos1;
            float r1 = Random.Range(0f, 1f);
            if (r1 <= DropAmuletChance(3, AmuletBuff.GdropCount, AmuletBuff.countDeadMobs))
            {
                itemDropPos1 = new Vector3(transform.position.x + Random.Range(-0.25f, 0.25f), transform.position.y + Random.Range(-0.25f, 0.25f), -87);
                Instantiate(GAmulet, itemDropPos1, Quaternion.identity);
                AmuletBuff.GdropCount++;
            }
            if (r1 <= DropAmuletChance(2, AmuletBuff.BdropCount, AmuletBuff.countDeadMobs))
            {
                itemDropPos1 = new Vector3(transform.position.x + Random.Range(-0.25f, 0.25f), transform.position.y + Random.Range(-0.25f, 0.25f), -87);
                Instantiate(BAmulet, itemDropPos1, Quaternion.identity);
                AmuletBuff.BdropCount++;
            }
            if (r1 <= DropAmuletChance(2, AmuletBuff.YdropCount, AmuletBuff.countDeadMobs))
            {
                itemDropPos1 = new Vector3(transform.position.x + Random.Range(-0.25f, 0.25f), transform.position.y + Random.Range(-0.25f, 0.25f), -87);
                Instantiate(YAmulet, itemDropPos1, Quaternion.identity);
                AmuletBuff.YdropCount++;
            }

            GameObject.FindGameObjectWithTag("levelGenerator").GetComponent<LevelGenerator>().DecreaseMobCountOnLvl();
            Destroy(gameObject);
        }
    }
    float DropAmuletChance(float k, float dropCount, float countDeadMobs)
    {
        return ((k - dropCount) / (100 - countDeadMobs)) * 1.4f * (k - dropCount);
    }
}
