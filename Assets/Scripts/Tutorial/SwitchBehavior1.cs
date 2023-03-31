using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complex = System.Numerics.Complex;
using MathNet.Numerics.LinearAlgebra;

public class SwitchBehavior1 : MonoBehaviour
{

    public DoorBehavior1 doorBehavior;
    bool playerNear = false;
    public GameObject floatingSpaceBar;
    public Matrix<Complex> switchOperation;
    public int operationIndex;
    public List<Sprite> opSprites;
    public GameObject Label;
    public List<Sprite> switchSprites;
    int spriteIndex = 0;

    public bool reversible = false;

    // Start is called before the first frame update
    void Start()
    {
        // choose operation
        switchOperation = Quantum.PauliX;
        operationIndex = 0;
        print(switchOperation);
        // set sprite
        Label.GetComponent<SpriteRenderer>().sprite = opSprites[operationIndex];
        

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
        doorBehavior.ChangeState(switchOperation);
        spriteIndex= (spriteIndex+1)%2;
        gameObject.GetComponent<SpriteRenderer>().sprite= switchSprites[spriteIndex];
    }
}
