using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paragan : Enemy
{
    public GameObject firePoint2;
    public GameObject laserPoint;
    public GameObject paraganLaser;
    private float direction;
    private float laserTimer = 5.0f;
    private float currentLaserTimer = 0.0f;
    private bool isShootingLaser = false;

    protected override void Awake()
    {
        base.Awake();
        int choice = Random.Range(0, 2);
        direction = choice == 0 ? 1.0f : -1.0f;
    }
    protected override void Fire()
    {
        base.Fire();
        Instantiate(enemyProjectile, firePoint2.transform.position, Quaternion.identity);
    }

    protected override void Move()
    {
        if (!isShootingLaser)
        {
            base.Move();
            rb2.velocity = new Vector2(speed * direction, rb2.velocity.y);
        }
        else
        {
            rb2.velocity = Vector2.zero;
        }
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

    protected override void Update()
    {
        base.Update();
        currentLaserTimer += Time.deltaTime;

        if(currentLaserTimer >= laserTimer)
        {
            currentLaserTimer = 0.0f;
            isShootingLaser = true;
            GameObject laser = Instantiate(paraganLaser, laserPoint.transform.position, Quaternion.Euler(90.0f, 0.0f, 0.0f));
            ParticleSystem particles = laser.GetComponent<ParticleSystem>();
            particles.Play();
            StartCoroutine(ResetLaserTimer());
        }
    }

    private IEnumerator ResetLaserTimer()
    {
        yield return new WaitForSeconds(3.0f);
        isShootingLaser = false;
        currentLaserTimer = 0.0f;
    }
}
