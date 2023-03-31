using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complex = System.Numerics.Complex;
using MathNet.Numerics.LinearAlgebra;

public class MonsterHealth : MonoBehaviour
{
    public Vector<Complex> Health;
    public GameObject bar0Fill;
    public GameObject bar1Fill;
    // Start is called before the first frame update
    public GameObject popParticles;
    public GameObject WinUI;
    public bool Boss = false;
    public GameObject CoinPrefab;
    public float force = 2;
    void Start()
    {
        Health = Quantum.QubitState(new Complex(1,0), new Complex(0,0));
        UpdateBars();
        GameObject.Find("Player").GetComponent<PlayerPickUp>().targetMonster=gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void Measure(){
        (double p_live, double p_die) = Quantum.getProbs(Health);
        if (Random.value < p_live)
        {
            Live();
        }
        else
        {
            Die();
        }

    }

    void Die(){
        Instantiate(popParticles, gameObject.transform.position + Vector3.back*3, Quaternion.identity);
        if (Boss){
            ActivateWinUI();
        }
        
        foreach (GameObject room in GameObject.FindGameObjectsWithTag("Room"))
        {
            room.GetComponent<RoomBehavior>().Door.SetActive(true);
            foreach (GameObject roomSwitch in room.GetComponent<RoomBehavior>().Switches)
            {
                roomSwitch.SetActive(true);
            }
        };

        // create coins
        int n_coins = Random.Range(1,4);
        if (Boss)
        {
            n_coins = Random.Range(3,6);
        }
        for (int i = 0; i<n_coins; i++)
        {
            GameObject instantiatedCoin = Instantiate(CoinPrefab, transform.position+Vector3.up*2, Quaternion.identity);

            instantiatedCoin.GetComponent<Rigidbody2D>().AddForce(Vector2.up*force*(1+Random.Range(0f,0.5f)), ForceMode2D.Impulse);
            if (Random.value>0.5)
            {
                instantiatedCoin.GetComponent<Rigidbody2D>().AddForce(Vector2.left*force, ForceMode2D.Impulse);
            }
            else{
                instantiatedCoin.GetComponent<Rigidbody2D>().AddForce(Vector2.right*force, ForceMode2D.Impulse);
            }
            

        }

        Destroy(gameObject);
    }

    void Live(){
        Health = Quantum.QubitState(new Complex(1,0), new Complex(0,0));
        gameObject.GetComponent<MonsterTimerManager>().ResetTimer();
        UpdateBars();

    }

    void ActivateWinUI(){
        WinUI.SetActive(true);
    }
}
