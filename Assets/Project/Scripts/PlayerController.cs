using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playerBody;
    [Range(0,5)]
    public float walkSpeed = 1;
    [Range(0,50)]
    public float jumpHeight = 5;
    [Space]
    public Animator animator;
    public Rigidbody2D rb;
    [Space]
    public Transform playerFeet;
    [Range(0,1)]
    public float playerFeetRadius = 0.1f;

    Collider2D ground;
    public bool isGrounded { get => ground; }
    bool disablePlayerInputs;
    public bool isPaused { set => disablePlayerInputs = value; get => disablePlayerInputs; }

    private void Start()
    {
        disablePlayerInputs = false;
    }

    void Update()
    {
        ground = Physics2D.OverlapCircle(playerFeet.position, playerFeetRadius);

        // Attach to ground
        if (isGrounded)
        {
            if (transform.parent == null || transform.parent != ground.transform)
                transform.parent = ground.transform;
        }
        else
        {
            transform.parent = null;
        }

        float x = 0;

        // Player Inputs
        if (!disablePlayerInputs)
        {
            x = Input.GetAxis("Horizontal");
            transform.Translate(Vector2.right * x * walkSpeed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
                rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }

        // Animations
        animator.SetBool("Grounded", isGrounded);
        animator.SetFloat("WalkSpeed", Mathf.Abs(x));
        if (x != 0)
            playerBody.transform.localScale = new Vector3(Mathf.Sign(x), 1, 1);

        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Jump"))
            if (rb.velocity.y > 0)
                animator.Play("Jump");
            else if (rb.velocity.y < 0)
                animator.Play("Fall");
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (playerFeet)
            Gizmos.DrawSphere(playerFeet.transform.position, playerFeetRadius);
    }
#endif
}
