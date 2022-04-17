using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb2;

    private void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontalDirection = Input.GetAxisRaw("Horizontal");
        rb2.velocity = new Vector2(horizontalDirection * speed, rb2.velocity.y);
        //print(rb2.velocity);
    }
}
