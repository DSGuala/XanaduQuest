using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complex = System.Numerics.Complex;
using MathNet.Numerics.LinearAlgebra;

public class DoorBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public WorldManager WorldManager;
    public RoomCoord connectedroomcoord;
    public RoomCoord roomcoord;
    public Vector<Complex> doorState;
    public GameObject bar0Sprite;
    public GameObject bar0Fill;
    public GameObject bar1Sprite;
    public GameObject bar1Fill;

    public string firstBarOutcome;

    public List<Sprite> spriteList;
    public Door door;

    void Start()
    {
        int x = WorldManager.roomcoord2index_x(connectedroomcoord.x);
        int y = WorldManager.roomcoord2index_y(connectedroomcoord.y);
        
        var thisRoom= WorldManager.rooms[x, y];
        var balancedvals = door.balancedvals;
        
        doorState = Quantum.QubitState(new Complex(balancedvals[0],balancedvals[1]), new Complex(balancedvals[2], balancedvals[3]));
        
        // Reflect door state in the bars
        UpdateBars();
        // Set the sprites
        SetSprites();
        // if both are monsters, make both bars red
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            //make room measurement
            (double prob0, double prob1) = Quantum.getProbs(doorState);
            WorldManager.MeasureRoom(connectedroomcoord,roomcoord);

            //create the indicated room
            WorldManager.CreateRoom(connectedroomcoord);
            
            //move to room
            WorldManager.MoveToRoom(connectedroomcoord);
            //destroy previous room
            Destroy(transform.parent.parent.gameObject);

        }
    }

    public void ChangeState(Matrix<Complex> op){
        doorState = Quantum.MatrixVectorMult(op, doorState);
        door.balancedvals = RoomSwitch.state2balancedvals(doorState);
        UpdateBars();
    }

    public void UpdateBars(){
        (double prob0, double prob1) = Quantum.getProbs(doorState);
        bar0Fill.transform.localScale=new Vector3((float) prob0*3.2f, bar0Fill.transform.localScale.y,bar0Fill.transform.localScale.z);
        bar1Fill.transform.localScale=new Vector3((float) prob1*3.2f, bar1Fill.transform.localScale.y,bar1Fill.transform.localScale.z);
    }

    public void SetSprites(){
        if (firstBarOutcome == "Treasure"){
            bar0Sprite.GetComponent<SpriteRenderer>().sprite = spriteList[0];
        }
        else{
            bar0Sprite.GetComponent<SpriteRenderer>().sprite = spriteList[1];
            bar0Fill.GetComponent<SpriteRenderer>().color = Color.red;
            bar1Sprite.GetComponent<SpriteRenderer>().sprite = spriteList[2];
        }
    }

}

static class RandomExtensions
{
    public static void Shuffle<T> (this System.Random rng, T[] array)
    {
        int n = array.Length;
        while (n > 1) 
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}
