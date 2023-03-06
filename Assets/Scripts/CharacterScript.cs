using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public Rigidbody2D rigidBody;

    public float runSpeed = 60f;
    public float horizontalMove = 0f;
    private bool facingRight = false;
    public float verticalMove = 0f;
    public float grav = 0f;
    public bool jumpReset;

	
    // Start is called before the first frame update
    void Start()
    {
	    rigidBody = GetComponent<Rigidbody2D>();
        jumpReset = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpReset)
        {
            rigidBody.velocity = Vector2.up * grav;
            jumpReset = false;
        }
        

        if (horizontalMove>0 && facingRight)
        {
            Flip();
        }
        if (horizontalMove<0 && !facingRight)
        {
            Flip();
        }
    }

    public void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(horizontalMove * Time.fixedDeltaTime, rigidBody.velocity.y);
    }

    public void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumpReset = true;
        }
    }
}
