using UnityEngine;

public class PlayerAnimate : MonoBehaviour
{

    private Animator animator;
    private Rigidbody2D rb;
    private const float moveSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            direction += Vector2.up;
        if (Input.GetKey(KeyCode.S))
            direction += Vector2.down;
        if (Input.GetKey(KeyCode.A))
            direction += Vector2.left;
        if (Input.GetKey(KeyCode.D))
            direction += Vector2.right;

        if (animator != null)
        {
            animator.SetBool("Move", direction.y != 0);
            animator.SetBool("MoveLeft", direction.x < 0);
            animator.SetBool("MoveRight", direction.x > 0);
        }

        direction.Normalize();
        if (rb != null)
            rb.linearVelocity = direction * moveSpeed;

    }
}
