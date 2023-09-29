using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGround : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] BoxCollider collider;

    Vector3 startPoint;

    bool isFalling = false;

    private void Start()
    {
        startPoint = transform.position;
        rb = GetComponent<Rigidbody>();
        isFalling = false;
    }

    private void Drop()
    {
        collider.isTrigger = true;
        isFalling = true;
        Invoke(nameof(OnReset), 3f);
    }

    private void OnReset()
    {
        transform.position = startPoint;
        collider.isTrigger = false;
        isFalling = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Rigidbody>().velocity.y <= 0.05f)
        {
            Invoke(nameof(Drop), 0.5f);
        }
    }
}
