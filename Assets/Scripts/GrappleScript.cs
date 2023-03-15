using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleScript : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRender;
    [SerializeField] private DistanceJoint2D distanceJoint;
    [SerializeField] private GameObject anchor;
    public bool grappling;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        distanceJoint.enabled = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && anchor != null)
        {
            lineRender.SetPosition(0, new Vector3(transform.position.x, transform.position.y + 0.43f, transform.position.z));
            lineRender.SetPosition(1, anchor.transform.position);
            distanceJoint.connectedAnchor = anchor.transform.position;
            distanceJoint.enabled = true;
            lineRender.enabled = true;
            grappling = true;
            animator.SetBool("isGrappling", true);
            animator.SetBool("isJumping", false);
        }
        else if (Input.GetKeyUp(KeyCode.G))
        {
            distanceJoint.enabled = false;
            lineRender.enabled = false;
            grappling = false;
            animator.SetBool("isGrappling", false);
        }
        if (distanceJoint.enabled)
        {
            lineRender.SetPosition(0, new Vector3(transform.position.x, transform.position.y + 0.43f, transform.position.z));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Anchor")
        {
            anchor = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Anchor")
        {
            anchor = null;
        }
    }
}
