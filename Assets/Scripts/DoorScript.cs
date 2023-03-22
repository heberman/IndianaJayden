using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    public GameObject key;
    public bool canProceed;

    void OnTriggerEnter2D(Collider2D other){
        if (other.name == "Character" && canProceed == true){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        key = GameObject.Find("Key");
        
    }

    // Update is called once per frame
    void Update()
    {
        canProceed = key.GetComponent<KeyScript>().keyCollect;
    }
}
