using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabCoin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Coin"))
        {
            PlayerManagement.numberOfCoins++;
            Destroy(collider2D.gameObject);
        }
    }
}
