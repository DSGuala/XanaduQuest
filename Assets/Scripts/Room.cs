using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complex = System.Numerics.Complex;

public class Room
{
    public byte RoomType;
    public RoomCoord roomCoord;
    public int n_doors;
    public int n_switches; //make sure no references before this is initialized
    public Vector3 CenterPosition;

    public Door roomDoor; //initialized by the world manager
    public RoomSwitch[] roomSwitches;

    public Room(){
        n_switches= Random.Range(0,5);
        roomSwitches = new RoomSwitch[n_switches];
        for (int i = 0; i<n_switches; i++)
        // for each switch, create a switch 
        {
            roomSwitches[i] = new RoomSwitch(Random.Range(0,DungeonData.n_types_gates), true);
        } 
    }
}

public class Door
{
    public string doorType;
    public float[] balancedvals = new float[4];
    

    public Door(string _doorType){
        doorType = _doorType;

        if (doorType != "Monster100"){
            balancedvals[0] = 0;
            balancedvals[1] = 0;
            balancedvals[2] = 1;
            balancedvals[3] = 0;
        }
        else{
            balancedvals[0] = Random.value;
            balancedvals[1] = Random.Range(0,Mathf.Sqrt(1-Mathf.Pow(balancedvals[0],2)));
            balancedvals[2] = Random.Range(0, Mathf.Sqrt(1-Mathf.Pow(balancedvals[0],2)-Mathf.Pow(balancedvals[1],2)));
            balancedvals[3] = Mathf.Sqrt(1-Mathf.Pow(balancedvals[0],2)-Mathf.Pow(balancedvals[1],2)-Mathf.Pow(balancedvals[2],2));
            var rng = new System.Random();
            rng.Shuffle(balancedvals);
        }
    }
}

public class RoomSwitch
{
    public int switchGate;
    public bool reversible;

    public RoomSwitch(int _switchGate, bool _reversible){
        switchGate=_switchGate;
        reversible=_reversible;

    }

}