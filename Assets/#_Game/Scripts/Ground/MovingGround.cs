using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGround : MonoBehaviour
{
    [SerializeField] Transform startPoint;
    [SerializeField] Transform finishPoint;

    [SerializeField] float moveSpeed = 1;
    Transform target;

    private void Start()
    {
        transform.position = startPoint.position;
        target = finishPoint;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, startPoint.position) < 0.1f)
        {
            target = finishPoint;
        }
        else if (Vector3.Distance(transform.position, finishPoint.position) < 0.1f)
        {
            target = startPoint;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.fixedDeltaTime * moveSpeed);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }

    private void OnDrawGizmos()
    {
        if (startPoint != null && finishPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(startPoint.position, 0.3f);
            Gizmos.DrawWireSphere(finishPoint.position, 0.3f);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(startPoint.position, finishPoint.position);
        }

    }
}

