using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complex = System.Numerics.Complex;
using MathNet.Numerics.LinearAlgebra;

public class PlayerHealth : MonoBehaviour
{
    public Vector<Complex> Health;
    public GameObject bar0Fill;
    public GameObject bar1Fill;
    // Start is called before the first frame update
    void Start()
    {
        Health = Quantum.QubitState(new Complex(1,0), new Complex(0,0));
        UpdateBars();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        

    }

    public void ChangeState(Matrix<Complex> op){
        Health = Quantum.MatrixVectorMult(op, Health);
        UpdateBars();
    }

    public void UpdateBars(){
        (double prob0, double prob1) = Quantum.getProbs(Health);
        bar0Fill.transform.localScale=new Vector3((float) prob0*3.2f, bar0Fill.transform.localScale.y,bar0Fill.transform.localScale.z);
        bar1Fill.transform.localScale=new Vector3((float) prob1*3.2f, bar1Fill.transform.localScale.y,bar1Fill.transform.localScale.z);
    }
}
