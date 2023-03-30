using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabCoin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Xgate"))
        {
            PlayerManagement.numberOfX++;
            Destroy(collider2D.gameObject);
        }
        if (collider2D.gameObject.CompareTag("Ygate"))
        {
            PlayerManagement.numberOfY++;
            Destroy(collider2D.gameObject);
        }
        if (collider2D.gameObject.CompareTag("Zgate"))
        {
            PlayerManagement.numberOfZ++;
            Destroy(collider2D.gameObject);
        }
        if (collider2D.gameObject.CompareTag("Hgate"))
        {
            PlayerManagement.numberOfH++;
            Destroy(collider2D.gameObject);
        }
    }
}
