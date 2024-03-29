using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGround : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] BoxCollider boxcollider;

    Vector3 startPoint;

    bool isFalling = false;

    private void Start()
    {
        startPoint = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        isFalling = false;
    }

    private void Drop()
    {
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        isFalling = true;
        Invoke(nameof(OnReset), 3f);
    }

    private void OnReset()
    {
        transform.position = startPoint;
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        isFalling = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Rigidbody>().velocity.y <= 0.05f)
        {
            Invoke(nameof(Drop), 1f);
        }
    }
}
