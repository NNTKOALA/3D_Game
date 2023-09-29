using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningReward : MonoBehaviour
{
    public float speed = 50f;

    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            //collision.GetComponent<Player>().AddStrawBerry();
            Destroy(gameObject);
        }
    }
}
