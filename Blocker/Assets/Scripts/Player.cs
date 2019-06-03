using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float jumpThreshold = 0.2f;

    private float height;
    float movement = 0f;
    private float jumpDuration=0f;
    private float rayDistance = 0.05f;
    private bool isJumping;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        height = GetComponent<Collider2D>().bounds.extents.y+0.01f;
    }

    private void Update()
    {
        Flip();
        Jump();
        Movement();
    }

    private void Flip()
    {
        movement = Input.GetAxis("Horizontal");

        if(movement<0)
        {
            sr.flipX = true;
        }
        else if(movement>0)
        {
            sr.flipX = false;
        }
    }

    private void Movement()
    {
        var xVelocity = 0f;

        if (movement == 0)
        {
            xVelocity = 0f;
            animator.SetBool("IsMove", false);
        }
        else
        {
            //xVelocity = rb.velocity.x;
            xVelocity = movement;
            animator.SetBool("IsMove", true);
        }

        //rb.AddForce(new Vector2(((movement * speed) /*- rb.velocity.x*/), 0));
        rb.velocity = new Vector2(xVelocity*speed, rb.velocity.y);
    }

    private bool IsGroundOrBox()
    {
       RaycastHit2D hit= Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y-height), Vector2.down, rayDistance);
       Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - height), Vector2.down*rayDistance, Color.green);
        if ( hit.collider != null && !hit.collider.CompareTag("Player"))
        {
            return true;
        }
        else
            return false;
    }

    private void Jump()
    {
        float jumping = Input.GetAxis("Jump");
        if (IsGroundOrBox() && isJumping == false)
        {
            if (jumping > 0f)
            {
                isJumping = true;
            }
        }

        if (jumping >= 1f && isJumping)
        {
            jumpDuration += Time.deltaTime;
            if (jumpDuration > jumpThreshold)
            {
                jumping = 0f;
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                AudioManager.PlayJumpSound();
            }
        }
        else
        {
            isJumping = false;
            jumpDuration = 0f;
        }

    }

    public void PlayFootstepsAudio()
    {
        if(IsGroundOrBox())
        AudioManager.PlayFootstepsSound();
    }

}
