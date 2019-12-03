using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallColliderMsg : MonoBehaviour
{
    public GameObject pannelWallMsg;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player") 
        {
            print("Wall");
            pannelWallMsg.SetActive(true);
            StartCoroutine("WaitForSec");
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(3);
        pannelWallMsg.SetActive(false);
    }
}
