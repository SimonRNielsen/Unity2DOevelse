using UnityEngine;

public class EnemyLaser : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer rend;
    private LaserPool pool;
    private const float speed = 2.5f;
    private const int damage = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void OnEnable()
    {

        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        pool = GameObject.FindWithTag("EnemyLaser").GetComponent<LaserPool>();

        if (rb != null && rend != null)
        {
            rb.linearVelocityY = -speed;
            transform.position -= new Vector3(0, rend.sprite.bounds.extents.y);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -Camera.main.orthographicSize - 1f)
            pool.ReturnObject(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player") && collision.GetComponent<PlayerUI>().RecievesDamage)
        {
            pool.ReturnObject(gameObject);
            PlayerUI player = collision.gameObject.GetComponent<PlayerUI>();
            player.Health -= damage;
        }
    }

    //private void OnBecameInvisible() => Destroy(gameObject);

}
