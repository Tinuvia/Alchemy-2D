using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// from Board to Bits better jump tutorial
// fix so the input is from Update and the physics applied in FixedUpdate
// Incorporate into main Player Movement script
// apply the inbuilt Rigidbody2D gravity scale parameter

public class BetterJump : MonoBehaviour
{
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.0f;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += (Vector2.up * Physics2D.gravity.y * (fallMultiplier -1) * Time.deltaTime); // 1 multiple of gravity is applied by default
        } else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))  {
            rb.velocity += (Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
        }
    }

}
