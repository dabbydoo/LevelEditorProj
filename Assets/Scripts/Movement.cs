using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    [SerializeField]private float speed;
    [SerializeField]private float jump;

    public Collider2D cd;

    private void Awake()
    {
        //Get reference
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cd = GetComponent<Collider2D>();
    }
    
    // Update is called once per frame
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        //Flip player transform
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        if (Input.GetKey(KeyCode.Space) && isGrounded)
            Jump();

        if (Input.GetKeyDown(KeyCode.C) && !cd.isTrigger)
            cd.isTrigger = true;
        else if(Input.GetKeyDown(KeyCode.C) && cd.isTrigger)
            cd.isTrigger = false;

        if (Input.GetKeyDown(KeyCode.G) && rb.gravityScale != 0)
            rb.gravityScale = 0;
        else if(Input.GetKeyDown(KeyCode.G) && rb.gravityScale == 0)
            rb.gravityScale = 1;

        //Set aninmator parameters
        anim.SetBool("isRun", horizontalInput != 0);
        anim.SetBool("isGrounded", isGrounded);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jump);
        anim.SetTrigger("isJump");
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = true;
    }


}
