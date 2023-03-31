using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complex = System.Numerics.Complex;
using MathNet.Numerics.LinearAlgebra;

public class TreasureBehavior : MonoBehaviour
{

    bool playerNear = false;
    public GameObject floatingSpaceBar;
    public List<Sprite> switchSprites;
    int spriteIndex = 0;

    public bool reversible = false;

    public GameObject CoinPrefab;
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        
        

    }

    // Update is called once per frame
    void Update()
    {
        if (playerNear){
            if (Input.GetKeyDown(KeyCode.Space)){
                flipswitch();
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")){
            playerNear=true;
            floatingSpaceBar.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")){
            playerNear=false;
            floatingSpaceBar.SetActive(false);
        }
    }

    private void flipswitch(){
        if (reversible==false && spriteIndex==1){
            return;
        }
        spriteIndex= (spriteIndex+1)%2;
        gameObject.GetComponent<SpriteRenderer>().sprite= switchSprites[spriteIndex];
        
        foreach (GameObject room in GameObject.FindGameObjectsWithTag("Room"))
        {
            room.GetComponent<RoomBehavior>().Door.SetActive(true);
            foreach (GameObject roomSwitch in room.GetComponent<RoomBehavior>().Switches)
            {
                roomSwitch.SetActive(true);
            }
        };

        //shoot out coins
        int n_coins = Random.Range(1,4);
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
    }
}
