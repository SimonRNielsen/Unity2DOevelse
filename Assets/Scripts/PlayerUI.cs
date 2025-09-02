using UnityEngine;

public class PlayerUI : MonoBehaviour
{

    private Animator animator;
    private SpriteRenderer rend;
    private Rigidbody2D rb;
    private LaserPool laserPool;
    private GameObject shieldObject;
    private const int maxHealth = 3;
    private int health;
    private int damage = 1;
    private int lives = 3;
    private const float shieldOffset = 0.2f;
    private const float invulnerabilityTimer = 2f;
    private const float fireRate = 0.8f;
    private float lastShot = 0;
    private float invulnerable;
    private float respawned;
    private bool shield = false;
    private bool canFire = true;
    private bool recievesDamage = true;
    private Vector3 spawnPosition;
    //public GameObject laserPrefab;

    public int Health
    {
        get => health;
        set => AdjustHealth(value);
    }

    public bool Shield
    {
        get => shield;
        set => Shielding(value);
    }

    public bool RecievesDamage { get => recievesDamage; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        animator = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        laserPool = GameObject.FindWithTag("PlayerLaser").GetComponent<LaserPool>();
        health = maxHealth;
        invulnerable = invulnerabilityTimer;
        respawned = invulnerabilityTimer;
        spawnPosition = GameObject.FindWithTag("Respawn").transform.position;
        shieldObject = GameObject.FindWithTag("Shield");

        Spawn();

    }

    // Update is called once per frame
    void Update()
    {

        if (animator != null)
            animator.SetBool("LowHealth", health <= maxHealth / 2);

        if (shieldObject != null && shield)
            shieldObject.transform.position = gameObject.transform.position + new Vector3(0, shieldOffset);

        invulnerable += Time.deltaTime;

        if (rend != null)
        {

            if (invulnerable >= invulnerabilityTimer)
            {
                recievesDamage = true;
                rend.color = Color.white;
            }

            if (respawned <= invulnerabilityTimer)
            {
                respawned += Time.deltaTime;
                rend.enabled = Mathf.PingPong(Time.time * 5f, 1f) > 0.5f;
            }
            else
                rend.enabled = true;
        }

        lastShot += Time.deltaTime;
        if (lastShot >= fireRate)
            canFire = true;

        if (Input.GetKey(KeyCode.Space) && canFire)
        {
            if (laserPool != null)
            {
                GameObject laser = laserPool.GetObject();
                laser.SetActive(true);
                laser.transform.position = transform.position + new Vector3(0, rend.sprite.bounds.extents.y);
            }
            //Instantiate(laserPrefab, transform.position + new Vector3(0, rend.sprite.bounds.extents.y), Quaternion.identity);
            canFire = false;
            lastShot = 0;
        }

    }


    void AdjustHealth(int value)
    {

        if (health < value)
        {
            health = Mathf.Min(value, maxHealth);
        }
        else if (health > value && recievesDamage)
        {
            if (shield)
                Shield = false;
            else
            {
                rend.color = Color.red;
                health = value;
                invulnerable = 0;
                recievesDamage = false;
            }
        }

        if (health <= 0)
        {
            lives--;
            Spawn();
            respawned = 0;
        }

        if (lives <= 0)
            GameOver();

    }


    void Spawn()
    {

        if (spawnPosition != null)
        {
            health = maxHealth;
            transform.position = spawnPosition;
            rb.position = transform.position;
        }

    }


    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy") && invulnerable >= invulnerabilityTimer)
        {
            EnemyStats enemyStats = collision.gameObject.GetComponent<EnemyStats>();
            Health -= enemyStats.Damage;
            enemyStats.Health -= damage;
        }

    }


    private void Shielding(bool value)
    {

        if (shieldObject != null)
        {
            shield = value;
            shieldObject.SetActive(value);
        }

    }

    void GameOver()
    {



    }

}
