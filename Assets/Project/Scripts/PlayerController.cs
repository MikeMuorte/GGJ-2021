using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(0,5)]
    public float walkSpeed = 1;
    [Range(0,50)]
    public float jumpHeight = 5;
    [Space]
    public Animator animator;
    public Rigidbody2D rb;
    public Transform playerFeet;

    Collider2D ground;
    public bool isGrounded { get => ground; }

    void Update()
    {ground = Physics2D.OverlapCircle(playerFeet.position, 0.1f);

        // Attach to ground
        if(isGrounded)
        {
            if (transform.parent == null)
                transform.parent = ground.transform;
        }
        else
        {
            transform.parent = null;
        }

        // Player Inputs
        float x = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * x * walkSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);

        // Animations
        if (rb.velocity.y == 0)
        {
            if (x == 0)
                animator.Play("Idle");
            else
                animator.Play("Walk");
        }
        else if (rb.velocity.y > 0)
        {
            animator.Play("Jump");
        }
        else
            animator.Play("Fall");

    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (playerFeet)
            Gizmos.DrawSphere(playerFeet.transform.position, 0.1f);
    }
#endif
}
