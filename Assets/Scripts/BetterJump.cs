using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// from Board to Bits better jump tutorial
// Incorporate into main Player Movement script
// apply the inbuilt Rigidbody2D gravity scale parameter

public class BetterJump : MonoBehaviour
{
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.0f;

    float _originalGravityScale;
    bool _higherJump;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _originalGravityScale = rb.gravityScale;
    }

    private void Update()
    {
        if(rb.velocity.y > 0 && !Input.GetButton("Jump")) {
            _higherJump = true;
        }        
    }

    private void FixedUpdate()
    {
        if (rb.velocity.y < 0)  {
            rb.gravityScale = fallMultiplier;  
        } else if (_higherJump)  {
            rb.gravityScale = lowJumpMultiplier;
            _higherJump = false;
        } else {
            rb.gravityScale = _originalGravityScale;
        }
    }

}
