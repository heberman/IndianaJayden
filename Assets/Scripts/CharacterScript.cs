using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterScript : MonoBehaviour
{
    public Rigidbody2D rigidBody;

    public GrappleScript grappleScript;
    public bool grapple;

    public float runSpeed = 60f;
    public float horizontalMove = 0f;
    private bool facingRight = false;
    public float verticalMove = 0f;
    public float grav = 0f;
    public bool jumpReset;

    //Q = Past, W = Present, E = Future
    public GameObject present;
    public GameObject future;
    public GameObject past;
    public bool isPresent;

    private Animator animator;

	
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
	    rigidBody = GetComponent<Rigidbody2D>();
        jumpReset = true;
        future.SetActive(false);
        past.SetActive(false);
        isPresent = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isRunning", (Mathf.Abs(rigidBody.velocity.x) > 0.01f && !animator.GetBool("isGrappling") && !animator.GetBool("isJumping")));

        if(rigidBody.position.y < -10   ){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        grapple = grappleScript.grappling;
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpReset)
        {
            animator.SetBool("isJumping", true);
            rigidBody.velocity = Vector2.up * grav;
            jumpReset = false;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            present.SetActive(false);
            past.SetActive(false);
            future.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            future.SetActive(false);
            past.SetActive(false);
            present.SetActive(true);

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            present.SetActive(false);
            future.SetActive(false);
            past.SetActive(true);

        }

        if (horizontalMove>0 && facingRight)
        {
            Flip();
        }
        if (horizontalMove<0 && !facingRight)
        {
            Flip();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Start Menu");
        } 
    }

    public void FixedUpdate()
    {
        if (!grapple) {
            rigidBody.velocity = new Vector2(horizontalMove * Time.fixedDeltaTime, rigidBody.velocity.y);
        }
        else
        {
            float speedDif = horizontalMove - rigidBody.velocity.x;
            float acc = (Mathf.Abs(horizontalMove) > 0.01f) ? 0.01f : -0.01f;
            //float movement = Mathf.Pow(Mathf.Abs(speedDif) * acc, 1f) * Mathf.Sign(speedDif) * 0.001f;
            float movement = speedDif * acc;
            rigidBody.AddForce(movement * Vector2.right);
        }
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
            animator.SetBool("isJumping", false);
        }
        if (collision.gameObject.name == "Trap")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //collision.gameObject.GetComponent<TrapScript>().isTouching = true; //ignore for now
        }
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.name == "Trap")
    //    {
    //        //collision.gameObject.GetComponent<TrapScript>().isTouching = false; 
    //    }
    //} ignore for now
}
