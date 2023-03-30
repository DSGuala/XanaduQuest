using System.Collections;
using System.Collections.Generic;
using Complex = System.Numerics.Complex;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    /// <summary>
    /// Variable <c>rooms</c> is an array with indices that go from 0,0 to x,y.
    /// </summary>
    public Room [,] rooms = new Room[DungeonData.DungeonWidth+1,DungeonData.DungeonHeight+1];
    public RoomCoord ActiveRoomCoord;
    public GameObject Camera;
    public GameObject Player;
    public GameObject RoomPrefab;
    public GameObject switchPrefab;

    void Start()
    {
         ActiveRoomCoord = new RoomCoord(0,0);
         InitializeRoom(ActiveRoomCoord);
         CreateRoom(ActiveRoomCoord);
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
                InitializeRoom(new RoomCoord(x,y));
            }
        }
        print("finished creating rooms");
    }

    /// <summary>
    /// Method <c>roomcoord2index</c> turns a roomcoord into an index of the rooms array
    /// </summary>
    public static int roomcoord2index_x(int x)
    {
        return x + DungeonData.DungeonWidth/2;
    }

    public static int roomcoord2index_y(int x)
    {
        return x + DungeonData.DungeonHeight/2;
    }

    public static int tosquarecoord_x(int x)
    {
        return x - DungeonData.DungeonWidth/2;
    }
    public static int tosquarecoord_y(int x)
    {
        return x - DungeonData.DungeonHeight/2;
    }

    void InitializeRoom(RoomCoord coord)
    {
        // coord.x is in range [-x,x]
        // it needs to be in range [0, 2x]
        int x = roomcoord2index_x(coord.x);
        int y = roomcoord2index_y(coord.y);

        rooms[x,y] = new Room();
        Vector3 roompos = new Vector3(coord.x*DungeonData.RoomWidth,
            coord.y*DungeonData.RoomHeight,0f);
        rooms[x,y].CenterPosition= roompos;

        // decide on the door, farther from center means more likelihood of finding boss
        // until y = 5, we won't have a boss
        // after y = 5, we have increasing probability of finding a boss
        // if we are at the last room, we make it a boss
        if (Random.Range(0,y) > 4 || y == DungeonData.DungeonHeight-1){
            if (y == DungeonData.DungeonHeight-1){
                print("monster100");
                rooms[x,y].roomDoor = new Door("Monster100");
            }
            else{
                print("monster");
                rooms[x,y].roomDoor = new Door("Monster");
            }
        }
        else{
            print("treasure");
            rooms[x,y].roomDoor = new Door("Treasure");
        }
        
        

    }

    // the DoorBehavior has a RoomCoord that it is related to
    // the DoorBehavior will call CreateRoom to this RoomCoord
    // the CreateRoom function will create the initialized Room at that RoomCoord
    public void CreateRoom(RoomCoord coord){
        // coord.x is in range [-x,x]
        // it needs to be in range [0, 2x]
        int x = roomcoord2index_x(coord.x);
        int y = roomcoord2index_y(coord.y);

        // grab the room at this coordinate
        Room thisRoom = rooms[x,y];

        //create room gameobject and set its variables
        GameObject instantiatedRoom = Instantiate(RoomPrefab,thisRoom.CenterPosition,Quaternion.identity);
        DoorBehavior roomDoorBehavior = instantiatedRoom.GetComponentInChildren<DoorBehavior>();
        roomDoorBehavior.connectedroomcoord = new RoomCoord(coord.x, coord.y+1);
        roomDoorBehavior.WorldManager = gameObject.GetComponent<WorldManager>();
        roomDoorBehavior.door = thisRoom.roomDoor;
        roomDoorBehavior.firstBarOutcome = thisRoom.roomDoor.doorType;
        
        Vector3[] switchPositions = {new Vector3(-3f, 5.66f, 0f),
        new Vector3(3f, 5.66f, 0f),
        new Vector3(-4.65f, 5.66f, 0f),
        new Vector3(4.65f, 5.66f, 0f)};

        //create the switches
        var switches = thisRoom.roomSwitches;
        // there are n switches in roomSwitches. For each one, we have to create a switch gameobject in the room
        for (int i = 0; i < switches.Length; i++){
            Transform positioning = instantiatedRoom.transform.Find("Positioning").transform;
            Vector3 localposition = positioning.TransformPoint(switchPositions[i]);
            GameObject thisSwitch = Instantiate(switchPrefab, localposition, Quaternion.identity, positioning);
            
            var thisSwitchBehavior = thisSwitch.GetComponent<SwitchBehavior>();
            thisSwitchBehavior.doorBehavior = roomDoorBehavior;

            //set the switches
            SwitchBehavior switchBehavior = instantiatedRoom.GetComponentInChildren<SwitchBehavior>();
            switchBehavior.doorBehavior = roomDoorBehavior;
        }
        
        //choose a random operation from the Quantum library
        // switchBehavior.switchOperation = Quantum.
    }

    public void MoveToRoom(RoomCoord coord){
        print("moving to new room at roomcoords" + coord.x.ToString() + "," + coord.y.ToString());
        
        int xindex = roomcoord2index_x(coord.x);
        int yindex = roomcoord2index_y(coord.y);

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
