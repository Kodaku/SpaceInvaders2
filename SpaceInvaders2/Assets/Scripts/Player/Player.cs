using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    private int lives;
    public GameObject playerProjecile;
    public GameObject firePoint1;
    public GameObject firePoint2;
    public GameObject firePoint3;
    private Text livesText;
    private bool isUsingMachineGun = false;
    private float machineGunTimer = 20.0f;
    private float currentMachineGunTimer = 0.0f;
    private float invulnerableTimer = 2.0f;
    private bool isInvulnerable = false;
    private Rigidbody2D rb2;
    private Animator animator;

    public int Lives { get => lives; set => lives = value; }

    private void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        livesText = GameObject.Find("LivesText").GetComponent<Text>();
        UpdateLivesText();
    }

    private void Update()
    {
        if(GameManager.IsMovementEnabled)
        {
            float horizontalDirection = Input.GetAxisRaw("Horizontal");
            rb2.velocity = new Vector2(horizontalDirection * speed, rb2.velocity.y);

            if (isUsingMachineGun)
            {
                currentMachineGunTimer += Time.deltaTime;
                if (currentMachineGunTimer >= machineGunTimer)
                {
                    currentMachineGunTimer = 0.0f;
                    isUsingMachineGun = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(playerProjecile, firePoint1.transform.position, Quaternion.identity);
                if (isUsingMachineGun)
                {
                    Instantiate(playerProjecile, firePoint2.transform.position, Quaternion.identity);
                    Instantiate(playerProjecile, firePoint3.transform.position, Quaternion.identity);
                }
            }
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Laser") || other.CompareTag("EnemyProjectile"))
        {
            if (!isInvulnerable)
            {
                isInvulnerable = true;
                lives--;
                CheckDeath();
                UpdateLivesText();
                if (other.CompareTag("EnemyProjectile"))
                {
                    Destroy(other);
                }
            }
        }
        else if (other.CompareTag("Life"))
        {
            lives++;
            UpdateLivesText();
            Destroy(other);
        }
        else if (other.CompareTag("MachineGun"))
        {
            isUsingMachineGun = true;
            Destroy(other);
        }
        else
        {
            Destroy(other);
        }
    }

    private void CheckDeath()
    {
        if(lives == 0)
        {
            GameManager.Instance.LoseGame();
            Destroy(this.gameObject);
        }
        else
        {
            animator.SetTrigger("Invulnerable");
            StartCoroutine(ResetVulnerability());
        }
    }

    private IEnumerator ResetVulnerability()
    {
        yield return new WaitForSeconds(invulnerableTimer);
        isInvulnerable = false;
        animator.SetTrigger("Vulnerable");
    }

    private void UpdateLivesText()
    {
        livesText.text = lives.ToString();
    }
}
