using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb2;

    private void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb2.velocity = new Vector2(rb2.velocity.x, -speed);
    }
}
