using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            //collider.gameObject.GetComponent<Player>().SetSavePoint(transform.position);

            if(GameManager.Instance.point >= 200)
            {
                UIManager.Instance.SwitchToWinPanel();
            }
            else
            {
                UIManager.Instance.LoadingSceneCoroutine(3f);
            }
        }
    }
}
