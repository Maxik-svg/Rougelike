using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;             

    private Rigidbody2D rb2d;


    //Используем это для инициализации
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        speed = 140f;
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed * Time.deltaTime;
    }

    //void Start()
    //{
    //    rb2d = GetComponent<Rigidbody2D>();
    //    speed = 2f;
    //}

    //private void FixedUpdate()
    //{
    //    rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed * Time.deltaTime;
    //}
}
