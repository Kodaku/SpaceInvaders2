using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zawolod : Enemy
{
    public GameObject firePoint2;
    public GameObject laserPoint;
    public GameObject zawolodLaser;
    private float directionX;
    private float directionY;
    private float laserTimer = 5.0f;
    private float currentLaserTimer = 0.0f;
    private bool isShootingLaser = false;
    private float directionTimer = 2.0f;
    private float currentDirectionTimer = 0.0f;
    private float machineGunWaitTimer = 6.0f;
    private float currentMachineGunWaitTimer = 0.0f;
    private bool isUsingMachineGun = false;
    private float machineGunUsingTimer = 6.0f;
    private float currentMachineGunUsingTimer = 0.0f;
    private float machineGunFireRate = 0.3f;
    private float oldFireRate;

    protected override void Awake()
    {
        base.Awake();
        oldFireRate = fireRate;
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
        Instantiate(enemyProjectile, firePoint2.transform.position, Quaternion.identity);
    }

    protected override void Move()
    {
        if (!isShootingLaser)
        {
            base.Move();
            rb2.velocity = new Vector2(speed * directionX, speed * directionY);
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
            ForceChangeDirection();
            currentDirectionTimer = 0.0f;
        }
    }

    private void CheckLowerBounds()
    {
        if (transform.position.y <= 6.0f)
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
        CheckShootLaser();
        CheckChangeDirection();
        CheckShootMachineGun();
    }

    private void CheckShootMachineGun()
    {
        if (!isUsingMachineGun)
        {
            currentMachineGunWaitTimer += Time.deltaTime;
            if (currentMachineGunWaitTimer >= machineGunWaitTimer)
            {
                currentMachineGunWaitTimer = 0.0f;
                isUsingMachineGun = true;
                fireRate = machineGunFireRate;
            }
        }
        else
        {
            currentMachineGunUsingTimer += Time.deltaTime;
            if (currentMachineGunUsingTimer >= machineGunUsingTimer)
            {
                currentMachineGunUsingTimer = 0.0f;
                isUsingMachineGun = false;
                fireRate = oldFireRate;
            }
        }
    }

    private void CheckChangeDirection()
    {
        currentDirectionTimer += Time.deltaTime;
        if (currentDirectionTimer >= directionTimer)
        {
            currentDirectionTimer = 0.0f;
            ChangeDirection();
        }
        CheckLowerBounds();
    }

    private void CheckShootLaser()
    {
        currentLaserTimer += Time.deltaTime;
        if (currentLaserTimer >= laserTimer)
        {
            currentLaserTimer = 0.0f;
            isShootingLaser = true;
            GameObject laser = Instantiate(zawolodLaser, laserPoint.transform.position, Quaternion.Euler(90.0f, 0.0f, 0.0f));
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
