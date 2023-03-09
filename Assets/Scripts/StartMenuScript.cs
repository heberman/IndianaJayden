using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            SceneManager.LoadScene("Tutorial");
        }
        else if (Input.GetKeyDown(KeyCode.B)) {
            SceneManager.LoadScene("Level 1");
        }
    }

}
