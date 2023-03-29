using System.Collections;
using System.Collections.Generic;
using Complex = System.Numerics.Complex;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    /// <summary>
    /// Variable <c>rooms</c> is an array with indices that go from 0,0 to x,y.
    /// </summary>
    Room [,] rooms = new Room[DungeonData.DungeonWidth+1,DungeonData.DungeonHeight+1];
    public RoomCoord ActiveRoomCoord;
    public GameObject Camera;
    public GameObject Player;
    public GameObject RoomPrefab;

    void Start()
    {
         ActiveRoomCoord = new RoomCoord(0,0);
         print("initializing dungeon");
         InitializeDungeon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeDungeon()
    {
        print("creating rooms");
        for (int x = -DungeonData.DungeonWidth/2; x<DungeonData.DungeonWidth/2; x++){
            for (int y = -DungeonData.DungeonHeight/2; y<DungeonData.DungeonHeight/2; y++){
                CreateRoom(new RoomCoord(x,y));
            }
        }
        print("finished creating rooms");
    }

    /// <summary>
    /// Method <c>roomcoord2index</c> turns a roomcoord into an index of the rooms array
    /// </summary>
    int roomcoord2index(int x)
    {
        return x + DungeonData.DungeonWidth/2;
    }

    int tosquarecoord(int x)
    {
        return x - DungeonData.DungeonWidth/2;
    }

    void CreateRoom(RoomCoord coord)
    {
        // coord.x is in range [-x,x]
        // it needs to be in range [0, 2x]
        int x = roomcoord2index(coord.x);
        int y = roomcoord2index(coord.y);

        rooms[x,y] = new Room();
        Vector3 roompos = new Vector3(coord.x*DungeonData.RoomWidth,
            coord.y*DungeonData.RoomHeight,0f);
        rooms[x,y].CenterPosition= roompos;
        GameObject instantiatedRoom = Instantiate(RoomPrefab,roompos,Quaternion.identity);
        instantiatedRoom.GetComponentInChildren<DoorBehavior>().connectedroomcoord = 
        new RoomCoord(coord.x, coord.y+1);
        instantiatedRoom.GetComponentInChildren<DoorBehavior>().WorldManager= 
        gameObject.GetComponent<WorldManager>();
        
    }

    public void MoveToRoom(RoomCoord coord){
        print("moving to new room at roomcoords" + coord.x.ToString() + "," + coord.y.ToString());

        int xindex = roomcoord2index(coord.x);
        int yindex = roomcoord2index(coord.y);

        float destinationx = rooms[xindex,yindex].CenterPosition.x;
        float destinationy = rooms[xindex,yindex].CenterPosition.y;

        Camera.transform.position = new Vector3(destinationx,destinationy,Camera.transform.position.z);
        Player.transform.position = new Vector3(destinationx,destinationy,Player.transform.position.z);
        ActiveRoomCoord = new RoomCoord(coord.x,coord.y);
        
    }
}

/// <summary>
/// Class <c>RoomCoord</c> has coordinates of a room, in a square, from -x,-y to x,y.
/// </summary>
public class RoomCoord
{
    public int x;
    public int y;

    public RoomCoord(int _x, int _y)
    {
        x = _x;
        y = _y;
    }


}
