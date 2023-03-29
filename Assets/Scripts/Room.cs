using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complex = System.Numerics.Complex;

public class Room
{
    public byte RoomType;
    public RoomCoord roomCoord;
    public int n_doors;
    public Vector3 CenterPosition;
}

public class Door
{
    public Complex a;
    public Complex b;


}