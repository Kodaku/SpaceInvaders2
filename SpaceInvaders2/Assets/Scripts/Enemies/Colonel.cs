using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colonel : Enemy
{
    private float directionX;
    private float directionY;
    private float directionTimer = 2.0f;
    private float currentDirectionTimer = 0.0f;

    protected override void Awake()
    {
        base.Awake();
        ChangeDirection();
    }

    private void ChangeDirection()
    {
        int choiceX = Random.Range(0, 3);
        int choiceY = Random.Range(0, 3);

        if (choiceX == 0) directionX = 0.0f;
        else if (choiceX == 1) directionX = 1.0f;
        else if (choiceX == 2) directionX = -1.0f;

        if (choiceY == 0) directionY = 0.0f;
        else if (choiceY == 1) directionY = 1.0f;
        else if (choiceY == 2) directionY = -1.0f;
    }

    private void ForceChangeDirection()
    {
        int choiceX = Random.Range(1, 3);
        int choiceY = Random.Range(1, 3);

        if (choiceX == 1) directionX = 1.0f;
        else if (choiceX == 2) directionX = -1.0f;

        if (choiceY == 1) directionY = 1.0f;
        else if (choiceY == 2) directionY = -1.0f;
    }

    private void ChangeUpDirection()
    {
        int choiceX = Random.Range(0, 3);

        if (choiceX == 0) directionX = 0.0f;
        else if (choiceX == 1) directionX = 1.0f;
        else if (choiceX == 2) directionX = -1.0f;

        directionY = 1.0f;
    }

    protected override void Fire()
    {
        base.Fire();
    }

    protected override void Move()
    {
        base.Move();
        rb2.velocity = new Vector2(speed * directionX, speed * directionY);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("WorldBound"))
        {
            ForceChangeDirection();
            currentDirectionTimer = 0.0f;
        }
    }

    private void CheckLowerBounds()
    {
        if(transform.position.y <= 6.0f)
        {
            ChangeUpDirection();
            currentDirectionTimer = 0.0f;
        }
    }

    protected override void OnParticleCollision(GameObject other)
    {
        base.OnParticleCollision(other);
    }

    protected override void Update()
    {
        base.Update();
        currentDirectionTimer += Time.deltaTime;
        if(currentDirectionTimer >= directionTimer)
        {
            currentDirectionTimer = 0.0f;
            ChangeDirection();
        }
        CheckLowerBounds();
    }


}
