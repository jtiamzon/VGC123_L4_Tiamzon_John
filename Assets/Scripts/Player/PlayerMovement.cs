using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]  // <<-- will always force the component to have a RigidBody2D
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer marioSprite;

    public float speed;
    public int jumpForce;
    public bool isGrounded;
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;
    public bool fire;
    public bool bloat;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // would be invalid if RigidBody does not exist
        anim = GetComponent<Animator>();
        marioSprite = GetComponent<SpriteRenderer>();

        if (speed <= 0) ;
        {
            speed = 5.0f;
        }

        if (jumpForce <=0)
        {
            jumpForce = 100;
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.01f;
        }

        if (!groundCheck)
        {
            Debug.Log("Groundcheck does not exist, please set a transform value for groundcheck");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
        
        if (Input.GetButtonDown("Jump") && isGrounded) // when player presses 'Jump' and is on the ground
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
        }

        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        anim.SetFloat("speed", Mathf.Abs(horizontalInput)); // Math.Abs makes input positive always
        anim.SetBool("isGrounded", isGrounded); // if isGrounded true, you can jump

        if(marioSprite.flipX && horizontalInput > 0 || !marioSprite.flipX && horizontalInput < 0) // if flipX is true, basically
        {
            marioSprite.flipX = !marioSprite.flipX; // equates to the inverse of mariosprite.flipX
        }

        fire = Input.GetKey("left ctrl");
        if (fire == true)
        {
            bool fire = Input.GetKey("left ctrl");
            anim.SetBool("fire", fire);
        }

        
        if (bloat == true)
        {
            if (Input.GetKey("q") && Input.GetKey("left shift"))
            { 
                // bool bloat = Input.GetKey("Combo1"); <<--- wasn't needed
                anim.SetBool("bloat", bloat);
            }
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            if (coll.gameObject.tag == "Player")
            {
                Destroy(coll.gameObject);
            }
        }


    }
}
