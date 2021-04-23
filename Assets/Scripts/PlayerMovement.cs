using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public float jumpForce;
    public float movementSpeed;
    public Transform feetBox;
    public LayerMask groundLayers;
    
    float _mx;
    bool _bJumpRequest;
    bool _bSpawnDust;
    float _timeBtwTrail;
    float _startTimeBtwTrail = 2f;
    GameObject _dustTrail;

    ObjectPooler _objectPooler;
    
    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        _timeBtwTrail = _startTimeBtwTrail;
    }

    void Update()
    {
        _mx = Input.GetAxisRaw("Horizontal");
        bool bGrounded = Physics2D.OverlapCircle(feetBox.position, 0.5f, groundLayers);

        if (bGrounded)
        {
            if (_bSpawnDust)
            {
                _objectPooler.SpawnFromPool("JumpCloud", feetBox.position, Quaternion.identity);
                _bSpawnDust = false;
            }

            if (Input.GetButtonDown("Jump"))
            {
                anim.SetTrigger("TakeOff");
                _bJumpRequest = true;
                _bSpawnDust = true;
            }
        }


        if (Mathf.Abs(_mx) > 0.01f)
        {
            transform.localScale = new Vector3(Mathf.Sign(_mx), 1f, 1f); // flips the player if needed
            anim.SetBool("IsWalking", true);

            if(_timeBtwTrail <= 0)
            {
                _dustTrail = _objectPooler.SpawnFromPool("DustTrail", feetBox.position, Quaternion.identity);
                _dustTrail.transform.SetParent(feetBox);
                //Instantiate(dustTrail, feetBox.position, Quaternion.identity);
                _timeBtwTrail = _startTimeBtwTrail;
            } else
            {
                _timeBtwTrail -= Time.deltaTime;
            }
         
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }

        anim.SetBool("IsGrounded", bGrounded);
    }

    private void FixedUpdate()
    {
        // move (Mass = 1, JumpForce = 17, movementSpeed = 10) 
        Vector2 movement = new Vector2(_mx * movementSpeed, rb.velocity.y);
        rb.velocity = movement;

        /* For an ice level?  Mass = 1, JumpForce = 15, movementSpeed = 35 
        Vector2 movement = new Vector2(_mx * movementFSpeed, 0);
        rb.AddForce(movement);
        */

        // jump
        if (_bJumpRequest)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _bJumpRequest = false;
        }

    }
}
