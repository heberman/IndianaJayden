using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    public Rigidbody2D rigidBody;

    public float runSpeed = 60f;
    public float horizontalMove = 0f;
    private bool facingRight = false;

	
    // Start is called before the first frame update
    void Start()
    {
	rigidBody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

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
}
