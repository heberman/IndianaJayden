using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartTrapScript : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public GameObject character;

    public GameObject preFab;

    public bool isCollide;
    public GameObject dart;
    public Transform dartPosition;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        isCollide = false;
        rigidBody = GetComponent<Rigidbody2D>();
        character = GameObject.FindGameObjectWithTag("Character");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (isCollide)
        {
            if (timer > 2)
            {
                timer = 0;
                activateTrap();
                isCollide = false;
            }
        }


        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            isCollide = true;
        }
    }


    private void activateTrap()
    {
        preFab = Instantiate(dart, dartPosition.position, Quaternion.identity);
        preFab.tag = "Dart";
    }

}
