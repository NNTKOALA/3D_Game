using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningReward : MonoBehaviour
{
    public float speed = 50f;
    public int point;

    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {           
            Destroy(gameObject);
            GameManager.Instance.point += point;
            UIManager.Instance.UpdatePoint();
        }
    }
}
