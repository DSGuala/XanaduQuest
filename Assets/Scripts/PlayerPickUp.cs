using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    public GameObject targetMonster;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        switch (other.gameObject.tag)
        {
            case "Xattack":
                targetMonster.GetComponent<MonsterHealth>().ChangeState(Quantum.PauliX);
                Destroy(other.gameObject);
                break;

            case "Yattack":
                targetMonster.GetComponent<MonsterHealth>().ChangeState(Quantum.PauliY);
                Destroy(other.gameObject);
                break;

            case "Zattack":
                targetMonster.GetComponent<MonsterHealth>().ChangeState(Quantum.PauliZ);
                Destroy(other.gameObject);
                break;

            case "Hattack":
                targetMonster.GetComponent<MonsterHealth>().ChangeState(Quantum.Hadamard);
                Destroy(other.gameObject);
                break;
        }
    }
}
