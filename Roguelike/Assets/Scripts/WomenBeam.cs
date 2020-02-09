using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomenBeam : MonoBehaviour
{
    public float damage;
    public float timeBtwAttac;
    private PlayerHP playerHP;

    // Start is called before the first frame update
    void Start()
    {
        playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHP>();        
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D other)
    {
        if (timeBtwAttac <= 0)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                playerHP.TakingDamage(damage);
                Destroy(gameObject);
            }
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (timeBtwAttac <= 0)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                playerHP.TakingDamage(damage);
                Destroy(gameObject);
            }
            Destroy(gameObject);
        }
    }
    void Update()
    {
        timeBtwAttac -= Time.deltaTime;
        if (timeBtwAttac <= -0.05)
        {
            Destroy(gameObject);
        }
    }
    
}
