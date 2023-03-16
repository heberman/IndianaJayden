using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    public Sprite pastSprite;
    public Sprite presentSprite;
    public Sprite futureSprite;

    public AudioClip pastMusic;
    public AudioClip presentMusic;
    public AudioClip futureMusic;
    // Start is called before the first frame update

    private Transform cameraTransform;
    private Vector3 previousCameraPosition;
    public float moveFactor = 0.3f;

    void Start()
    {
        GetComponent<AudioSource>().clip = presentMusic;
        GetComponent<AudioSource>().Play();

        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraDelta = cameraTransform.position - previousCameraPosition;
        transform.position += cameraDelta * moveFactor;
        previousCameraPosition = cameraTransform.position;

        if (Input.GetKeyDown("q"))
        {
            GetComponent<SpriteRenderer>().sprite = pastSprite;
            float time = GetComponent<AudioSource>().time;
            GetComponent<AudioSource>().clip = pastMusic;
            GetComponent<AudioSource>().time = time;
            GetComponent<AudioSource>().Play();
        }
        if (Input.GetKeyDown("w"))
        {
            GetComponent<SpriteRenderer>().sprite = presentSprite;
            float time = GetComponent<AudioSource>().time;
            GetComponent<AudioSource>().clip = presentMusic;
            GetComponent<AudioSource>().time = time;
            GetComponent<AudioSource>().Play();
        }
        if (Input.GetKeyDown("e"))
        {
            GetComponent<SpriteRenderer>().sprite = futureSprite;
            float time = GetComponent<AudioSource>().time;
            GetComponent<AudioSource>().clip = futureMusic;
            GetComponent<AudioSource>().time = time;
            GetComponent<AudioSource>().Play();
        }
    }
}
