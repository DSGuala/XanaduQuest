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
        print("detected collision with" + other.gameObject.tag);
        switch (other.gameObject.tag)
        {
            case "Xgate":
                targetMonster.GetComponent<MonsterHealth>().ChangeState(Quantum.PauliX);
                Destroy(other.gameObject);
                break;

            case "Ygate":
                targetMonster.GetComponent<MonsterHealth>().ChangeState(Quantum.PauliY);
                Destroy(other.gameObject);
                break;

            case "Zgate":
                targetMonster.GetComponent<MonsterHealth>().ChangeState(Quantum.PauliZ);
                Destroy(other.gameObject);
                break;

            case "Hgate":
                targetMonster.GetComponent<MonsterHealth>().ChangeState(Quantum.Hadamard);
                Destroy(other.gameObject);
                break;
        }
    }
}
