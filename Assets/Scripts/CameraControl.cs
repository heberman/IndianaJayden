using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x < 0) 
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        }
        
    }
}
