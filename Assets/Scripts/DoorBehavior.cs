using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public WorldManager WorldManager;
    public RoomCoord connectedroomcoord;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            print("collision with door detected");
            WorldManager.MoveToRoom(connectedroomcoord);

        }
    }
}
