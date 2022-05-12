using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : Enemy
{
    private float direction;
    private float upDownTimer = 2.0f;
    private float currentUpDownTimer = 0.0f;
    private float upDownDirection = -1.0f;
    private float minimumY = 6.0f;
    private float maximumY = 17.0f;

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
            ChangeUpDownDirection();
        }
    }

    private void ChangeUpDownDirection()
    {
        if (transform.position.y <= minimumY)
        {
            upDownDirection = 1.0f;
        }
        else if (transform.position.y >= maximumY)
        {
            upDownDirection = -1.0f;
        }
    }

    protected override void OnParticleCollision(GameObject other)
    {
        base.OnParticleCollision(other);
    }

    protected override void Update()
    {
        base.Update();
        currentUpDownTimer += Time.deltaTime;
        if(currentUpDownTimer >= upDownTimer)
        {
            currentUpDownTimer = 0.0f;
            int choice = Random.Range(0, 2);
            ChangeUpDownDirection();
            Vector2 commanderPosition = transform.position;
            if(choice == 1)
            {
                commanderPosition.y += upDownDirection;
            }
            transform.position = commanderPosition;
        }
    }
}
