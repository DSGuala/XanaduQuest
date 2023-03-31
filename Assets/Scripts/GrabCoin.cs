using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabCoin : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Player"))
        {
            PlayerManagement.numberOfCoins++;
            Destroy(gameObject);
        }
    }
}
