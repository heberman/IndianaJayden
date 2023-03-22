using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterScript : MonoBehaviour
{
    public Rigidbody2D rigidBody;

    public GrappleScript grappleScript;
    public bool grapple;

    public GameObject anchorPres;
    public GameObject anchorPast;

    public GameObject key;

    public float runSpeed = 60f;
    public float horizontalMove = 0f;
    private bool facingRight = false;
    public float verticalMove = 0f;
    public float grav = 0f;
    public bool jumpReset;

    //Q = Time travel
    public GameObject present;
    public GameObject past;

    private Animator animator;
    
    public Image healthBar;
    public float health = 100f;

    public AnalogGlitch analogGlitch;
    public float glitchDuration = 0.2f;

    public List<GameObject> fBlocks;


    



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
	    rigidBody = GetComponent<Rigidbody2D>();
        jumpReset = true;
        past.SetActive(false);
        anchorPast = GameObject.FindGameObjectWithTag("AnchorPast");
        anchorPast.SetActive(false);
        key = GameObject.Find("Key");
        if (key.CompareTag("KeyPast"))
        {
            key.SetActive(false);
        }

        fBlocks = new List<GameObject>(GameObject.FindGameObjectsWithTag("BlockPresent"));
        fBlocks.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("BlockPast")));

        foreach (GameObject block in fBlocks)
        {
            if (block.CompareTag("BlockPast"))
            {
                block.SetActive(false);
            }
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isRunning", (Mathf.Abs(rigidBody.velocity.x) > 0.01f && !animator.GetBool("isGrappling") && !animator.GetBool("isJumping")));

        if(rigidBody.position.y < -10){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        grapple = grappleScript.grappling;
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && jumpReset)
        {
            animator.SetBool("isJumping", true);
            rigidBody.velocity = Vector2.up * grav;
            jumpReset = false;
        }


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            analogGlitch.scanLineJitter = 0.0f;
            analogGlitch.colorDrift = 0.0f;

            if (present.activeSelf && key.CompareTag("KeyPresent"))//turn to past
            {
                StartCoroutine(Glitch());
                present.SetActive(false);
                past.SetActive(true);
                anchorPast.SetActive(true) ;
                key.SetActive(false);
            }
            else if (past.activeSelf && key.CompareTag("KeyPresent"))//turn to present
            {
                StartCoroutine(Glitch());
                past.SetActive(false);
                present.SetActive(true);
                anchorPast.SetActive(false);
                key.SetActive(true);
            }
            if (present.activeSelf && key.CompareTag("KeyPast"))//turn to past
            {
                StartCoroutine(Glitch());
                present.SetActive(false);
                past.SetActive(true);
                anchorPast.SetActive(true);
                key.SetActive(true);
            }

            else if (past.activeSelf && key.CompareTag("KeyPast"))//turn to present
            {
                StartCoroutine(Glitch());
                past.SetActive(false);
                present.SetActive(true);
                anchorPast.SetActive(false);
                key.SetActive(false);

            }
            if (present.activeSelf && key.CompareTag("Key"))//turn to past
            {
                StartCoroutine(Glitch());
                present.SetActive(false);
                past.SetActive(true);
                anchorPast.SetActive(true);
            }
            else if (past.activeSelf && key.CompareTag("Key"))//turn to present
            {
                StartCoroutine(Glitch());
                past.SetActive(false);
                present.SetActive(true);
                anchorPast.SetActive(false);

            }

        }

        foreach (GameObject block in fBlocks)
        {
            if (block.CompareTag("BlockPresent") && present.activeSelf)
            {
                block.SetActive(true);
            }
            else if (block.CompareTag("BlockPresent") && !present.activeSelf)
            {
                block.SetActive(false);
            }
            else if (block.CompareTag("BlockPast") && past.activeSelf)
            {
                block.SetActive(true);
            }
            else if (block.CompareTag("BlockPast") && !past.activeSelf)
            {
                block.SetActive(false);
            }

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

    IEnumerator Glitch()
    {
        float startTime = Time.realtimeSinceStartup;
        float initialJitter = analogGlitch.scanLineJitter;
        float initialColorDrift = analogGlitch.colorDrift;

        while (Time.realtimeSinceStartup < startTime + glitchDuration)
        {
            analogGlitch.scanLineJitter = 0.5f;
            analogGlitch.colorDrift = 0.3f;
            yield return new WaitForSeconds(glitchDuration);
        }

        analogGlitch.scanLineJitter = initialJitter;
        analogGlitch.colorDrift = initialColorDrift;
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
            takeDamage(10);
        }

        if(collision.gameObject.tag == "Dart"){
            takeDamage(10);
        }
    }



    private void takeDamage(float damage){
        health -= damage;
        healthBar.fillAmount = health/100;
    }


}
