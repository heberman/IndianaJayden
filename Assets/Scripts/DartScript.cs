using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartScript : MonoBehaviour
{
    [SerializeField]
    private float dartSpeed = 5;
    [SerializeField]

    private Rigidbody2D rigidBody;


    // Update is called once per frame
    void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(-1*(dartSpeed * Time.fixedDeltaTime), rigidBody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other){
        Destroy(gameObject);
    }
}
