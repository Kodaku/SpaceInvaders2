using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : Enemy
{
    private float direction;

    protected override void Awake()
    {
        base.Awake();
        int choice = Random.Range(0, 2);
        direction = choice == 0 ? 1.0f : -1.0f;
    }
    protected override void Fire()
    {
        base.Fire();
    }

    protected override void Move()
    {
        base.Move();
        rb2.velocity = new Vector2(speed * direction, rb2.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("WorldBound"))
        {
            direction *= -1.0f;
        }
    }

    protected override void OnParticleCollision(GameObject other)
    {
        base.OnParticleCollision(other);
    }
}
