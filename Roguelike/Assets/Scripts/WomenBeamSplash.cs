using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomenBeamSplash : MonoBehaviour
{
    private PlayerHP playerHP;
    public float damage;
    float time;
    void Start()
    {
        damage = 0.05f;
        playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHP>();
    }
    //переделывать под анимацию
    void Update()
    {
        var scale = transform.localScale;
        scale.x += 0.005f;
        scale.y += 0.005f;
        transform.localScale = scale;
        time += damage;
        if (scale.x >= 2f)
        {
            print(time);
            Destroy(gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        playerHP.TakingDamage(damage);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        playerHP.TakingDamage(damage);
    }

}
