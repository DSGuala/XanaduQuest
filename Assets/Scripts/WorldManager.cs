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

    public GameObject treasurePrefab;
    public List<GameObject> monsterPrefabs;
    public GameObject BossPrefab;
    public GameObject winUI;

    void Start()
    {
         ActiveRoomCoord = new RoomCoord(0,0);         
         print("initializing dungeon");
         InitializeDungeon();
         CreateRoom(ActiveRoomCoord);
         

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeDungeon()
    {
        print("initializing rooms");
        // for (int x = -DungeonData.DungeonWidth/2; x<DungeonData.DungeonWidth/2; x++)
        {
            int x = 0;
            for (int y = 0; y<DungeonData.DungeonHeight; y++){
                InitializeRoom(new RoomCoord(x,y));
            }
        }
        print("finished initializing rooms");
    }

    /// <summary>
    /// Method <c>roomcoord2index</c> turns a roomcoord into an index of the rooms array
    /// </summary>
    public static int roomcoord2index_x(int x)
    {
        return x;
    }

    public static int roomcoord2index_y(int x)
    {
        return x;
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
        if (y == DungeonData.DungeonHeight-2 || Random.Range(0,y) > 4){
            if (y == DungeonData.DungeonHeight-2){
                rooms[x,y].roomDoor = new Door("Monster100");
            }
            else{
                rooms[x,y].roomDoor = new Door("Monster");
            }
        }
        else{
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
        roomDoorBehavior.roomcoord = coord;
        roomDoorBehavior.WorldManager = gameObject.GetComponent<WorldManager>();
        roomDoorBehavior.door = thisRoom.roomDoor;
        roomDoorBehavior.firstBarOutcome = thisRoom.roomDoor.doorType;
        GameObject tempDoor = roomDoorBehavior.gameObject;
        tempDoor.SetActive(false);
        // create and set switches
        CreateSwitches(coord, instantiatedRoom, roomDoorBehavior);
        // delete switches if the next room is a mandatory boss room
        if (coord.y==DungeonData.DungeonHeight-2)
        {
            foreach (GameObject roomSwitch in instantiatedRoom.GetComponent<RoomBehavior>().Switches)
            {
                Destroy(roomSwitch);
            }
        }
        
        
        // create room contents: treasure, monster, or boss

        switch (thisRoom.RoomType)
        {
            case "Treasure":
                Instantiate(treasurePrefab, thisRoom.CenterPosition, Quaternion.identity);
                break;

            case "Monster":
                //choose a random monster prefab
                int i = Random.Range(0,monsterPrefabs.Count);
                GameObject monsterUI = Instantiate(monsterPrefabs[i], thisRoom.CenterPosition, Quaternion.identity);
                
                break;

            case "Boss":
                GameObject monsterUI2 =  Instantiate(BossPrefab, thisRoom.CenterPosition, Quaternion.identity);
                monsterUI2.GetComponent<MonsterHealth>().WinUI=winUI;
                break;
        }

        if (coord.x == 0 && coord.y == 0)
        {
            print("setting door active");
            tempDoor.SetActive(true);
            foreach (GameObject roomSwitch in instantiatedRoom.GetComponent<RoomBehavior>().Switches)
            {
                roomSwitch.SetActive(true);
            }
        }

        
    }

    private void CreateSwitches(RoomCoord coord, GameObject instantiatedRoom, DoorBehavior roomDoorBehavior){
        int x = roomcoord2index_x(coord.x);
        int y = roomcoord2index_y(coord.y);

        // grab the room at this coordinate
        Room thisRoom = rooms[x,y];

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

            thisSwitch.SetActive(false);
            instantiatedRoom.GetComponent<RoomBehavior>().Switches.Add(thisSwitch);


        }

    }

    public void MoveToRoom(RoomCoord coord){
        print("moving to new room at roomcoords" + coord.x.ToString() + "," + coord.y.ToString());
        
        int xindex = roomcoord2index_x(coord.x);
        int yindex = roomcoord2index_y(coord.y);

        float destinationx = rooms[xindex,yindex].CenterPosition.x;
        float destinationy = rooms[xindex,yindex].CenterPosition.y;

        Camera.transform.position = new Vector3(destinationx,destinationy,Camera.transform.position.z);
        Player.transform.position = new Vector3(destinationx,destinationy-DungeonData.RoomHeight/2.5f,Player.transform.position.z);
        ActiveRoomCoord = new RoomCoord(coord.x,coord.y);
        
    }

    public void MeasureRoom(RoomCoord coord, RoomCoord prevcoord){
        // this should set the room type: treasure, monster, or boss
        // it will be set based on the door probabilities
        // 
        int xindex = roomcoord2index_x(coord.x);
        int yindex = roomcoord2index_y(coord.y);
        Room theRoom = rooms[xindex,yindex];

        int previousxindex = roomcoord2index_x(prevcoord.x);
        int previousyindex = roomcoord2index_y(prevcoord.y);
        Room previousRoom = rooms[previousxindex,previousyindex];

        // get the probability of room contents:
        (double p0, double p1)= Quantum.getProbs(RoomSwitch.balancedvals2state(previousRoom.roomDoor.balancedvals));
        // theRoom.RoomType = "monster", "boss" or "treasure";
        switch (previousRoom.roomDoor.doorType)
        {
            case "Treasure":
                if (Random.value < p0) // we get the first thing, treasure
                {
                    theRoom.RoomType="Treasure";
                }
                else //we get the second thing, monster
                {
                    theRoom.RoomType="Monster";
                }
                break;

            case "Monster":
                if (Random.value < p0) // we get the first thing, monster
                {
                    theRoom.RoomType="Monster";
                }
                else //we get the second thing, Boss
                {
                    theRoom.RoomType="Boss";
                }
                break;
            
            case "Monster100":
                theRoom.RoomType="Boss"; // we get the boss
                break;

        }
    }

    public void MeasureEntities()
    {
        // find an measure monsters
        GameObject[] monsters;
        GameObject player;
        monsters = GameObject.FindGameObjectsWithTag("Monster");
        player = GameObject.FindGameObjectWithTag("Player");

        foreach (GameObject monster in monsters)
        {
            monster.GetComponent<MonsterHealth>().Measure();
        }
        
        player.GetComponent<PlayerHealth>().Measure();


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
