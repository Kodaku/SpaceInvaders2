using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float fireRate;
    public float speed;
    public int lives;
    public GameObject enemyProjectile;
    public GameObject firePoint;
    public HealthBar healthBar;
    protected Rigidbody2D rb2;
    private float currentFireTimer = 0.0f;

    protected virtual void Awake()
    {
        fireRate = Random.Range(fireRate - 0.7f, fireRate + 0.7f);
        rb2 = GetComponent<Rigidbody2D>();
        healthBar.SetMaxHealth(lives);
    }

    protected virtual void Move() { }

    protected virtual void Fire()
    {
        Instantiate(enemyProjectile, firePoint.transform.position, Quaternion.identity);
    }

    protected virtual void Update()
    {
        if(GameManager.IsMovementEnabled)
        {
            Move();
            currentFireTimer += Time.deltaTime;
            if (currentFireTimer >= fireRate)
            {
                currentFireTimer = 0.0f;
                Fire();
            }
        }
    }

    protected virtual void OnParticleCollision(GameObject other)
    {
        Destroy(other);
        lives--;
        healthBar.SetHealth(lives);
        if (lives <= 0)
        {
            ScoreManager.Instance.UpdateScore(EnemyScore.GetScore(tag));
            EventHandler.CallDestroyEnemyEvent(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
