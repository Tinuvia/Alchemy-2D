using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public Rigidbody2D rb;
    public Animator anim;
    public float jumpForce;
    public Transform feetBox;
    public LayerMask groundLayers;

    float _mx;

    void Start()
    {
        
    }


    void Update() {
        _mx = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded()) {
            Jump();
            anim.SetTrigger("TakeOff");
        }

        if(Mathf.Abs(_mx) > 0.05f) {
            anim.SetBool("IsWalking", true);
        } else {
            anim.SetBool("IsWalking", false);
        }

        if (_mx > 0f) {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (_mx < 0f) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        anim.SetBool("IsGrounded", IsGrounded());
    }

    private void FixedUpdate() {
        Vector2 movement = new Vector2(_mx * movementSpeed, rb.velocity.y);
        rb.velocity = movement;
    }

    void Jump() {
        Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
        rb.velocity = movement;
    }

    public bool IsGrounded() {
        Collider2D groundCheck = Physics2D.OverlapCircle(feetBox.position, 0.5f, groundLayers);

        if (groundCheck != null)
        {
            return true;
        }
        return false;
    }
}
