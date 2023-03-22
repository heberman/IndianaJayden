using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public bool keyCollect;
    public GameObject character;

    public BoxCollider2D keyC;


    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character");
        keyC = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (keyCollect == true)
        {
            keyC.enabled = false;
            transform.localScale = new Vector3(.3f, .3f, 0);
            transform.position = new Vector3(character.transform.position.x+.7f, character.transform.position.y+.7f, character.transform.position.z);
            gameObject.tag = "Key";
            gameObject.SetActive(true);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            keyCollect = true;
            
        }
    }
}
