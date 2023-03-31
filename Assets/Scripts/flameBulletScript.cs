using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using Complex = System.Numerics.Complex;

public class flameBulletScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force;
    private float timer;
    public string gateName;
    Matrix<Complex> op;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        switch (gateName)
        {
            case "X":
                op = Quantum.PauliX;
                break;

            case "Y":
                op = Quantum.PauliY;
                break;

            case "Z":
                op = Quantum.PauliZ;
                break;

            case "H":
                op = Quantum.Hadamard;
                break;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 4) 
        {
            Destroy(gameObject);
        
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().ChangeState(op);
            Destroy(gameObject);
        }
    }
}
