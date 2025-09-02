using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer rend;
    private EnemyPool pool;
    private LaserPool laserPool;
    public Sprite[] sprites = new Sprite[3];
    private int health;
    private int maxHealth = 1;
    private int damage = 1;
    private float moveSpeed = 1;
    private float rotationSpeed = 0.15f;
    private EnemyType type;
    private bool canShoot = false;
    private const float refireRate = 1.8f;
    private float refire = 0;
    //public GameObject shotPrefab;
    public GameObject explosion;

    public int Health
    {
        get => health;
        set
        {
            health = value;

            if (health <= 0)
            {
                Death();
                if (explosion != null)
                    Instantiate(explosion, transform.position, Quaternion.identity);
            }
        }
    }

    public int Damage { get => damage; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        pool = GameObject.FindWithTag("EnemyPool").GetComponent<EnemyPool>();
        laserPool = GameObject.FindWithTag("EnemyLaser").GetComponent<LaserPool>();
        Load();

    }

    // Update is called once per frame
    void Update()
    {

        switch (type)
        {
            case EnemyType.Regular:
                refire += Time.deltaTime;
                Shoot();
                break;
            case EnemyType.UFO:
                rb.rotation += rotationSpeed;
                break;
            default:
                break;
        }

        if (transform.position.y < -Camera.main.orthographicSize - 1f)
            Death();

    }


    public void Load()
    {

        rb.rotation = 0;
        type = (EnemyType)Random.Range(0, System.Enum.GetValues(typeof(EnemyType)).Length);
        if ((int)type < sprites.Length)
            rend.sprite = sprites[(int)type];
        health = maxHealth;
        rb.linearVelocityY = -moveSpeed;
        transform.position = pool.transform.position + new Vector3(Random.Range((int)GameObject.FindGameObjectWithTag("LeftSide").gameObject.transform.position.x + 1, (int)GameObject.FindGameObjectWithTag("RightSide").gameObject.transform.position.x - 1), 0);
        rb.position = transform.position;

        switch (type)
        {
            case EnemyType.Regular:
                canShoot = true;
                break;
            default:
                canShoot = false;
                break;
        }

    }

    private void Shoot()
    {

        if (canShoot && refire >= refireRate)
        {
            refire = 0;
            if (laserPool != null)
            {
                GameObject laser = laserPool.GetObject();
                laser.SetActive(true);
                laser.transform.position = transform.position + new Vector3(0, -sprites[(int)type].bounds.extents.y);
            }
            //Instantiate(shotPrefab, transform.position + new Vector3(0, -sprites[(int)type].bounds.extents.y), Quaternion.identity);
        }

    }

    //private void OnBecameInvisible() => pool.ReturnObject(gameObject);

    private void Death() => pool.ReturnObject(gameObject);


}

public enum EnemyType
{
    Regular,
    UFO,
    Unarmed
}