using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBlockScript : MonoBehaviour
{
    [SerializeField]
    public float fallSpeed = 50;
    [SerializeField]

    public Rigidbody2D rigidBody;

    private float timer;
    public bool hasCollided;

    public CharacterScript character;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        character = GameObject.FindGameObjectWithTag("Character").GetComponent<CharacterScript>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (hasCollided && timer < 5)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, -1 * (fallSpeed * Time.fixedDeltaTime));
        }
        else if (hasCollided && timer >= 5)
        {
            character.fBlocks.Remove(gameObject);
            Destroy(gameObject);
            hasCollided = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            hasCollided = true;
            timer = 0;
            character.jumpReset = true;
        }
    }
}
