using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public float startDamage; //не только стартовый урон. Пояснение: урон без бафа или дебафа
    public float startTimeBtwAttac;
    public float timeForScroll;


    private float currentDamage;
    private Inventory inventory;
    private float timeBtwAttac = 0;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        startDamage = 4;
        currentDamage = startDamage;
    }

    void Update()
    {
        if (Input.GetKeyUp("3"))
        {
            if (timeForScroll <= 0 && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHP>().timeForSoul <= 0)
                UseScroll();
        }

        if (timeBtwAttac <= 0)
        {
            if (Input.GetKeyUp("space"))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {

                    if (enemiesToDamage[i].GetComponent<Enemy>() != null)
                        enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(currentDamage);
                    if (enemiesToDamage[i].GetComponent<Boss2Enemy>() != null)
                        enemiesToDamage[i].GetComponent<Boss2Enemy>().TakeDamage(currentDamage);
   
                }
                timeBtwAttac = startTimeBtwAttac;
                
            }
            
        }
        else
        {
            timeBtwAttac -= Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        if (timeForScroll > 0f)
        {
            timeForScroll -= Time.deltaTime;
        }
        else
        {
            currentDamage = startDamage;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    public void UseScroll()
    {
        for (int i = 0; i < inventory.slots.Length; i++) //inventory.slots.Length
        {
            if (inventory.slots[i].transform.childCount > 0)
            {
                if (inventory.slots[i].transform.GetChild(0).CompareTag("Scroll"))
                {
                    currentDamage = startDamage * 1.5f;
                    timeForScroll = 15f;
                    inventory.isFull[i] = false;
                    foreach (Transform t in inventory.slots[i].transform)
                    {
                        Destroy(t.gameObject);
                    }
                    break;
                }
            }
            else
            {
                continue;
            }

        }
    }
}
