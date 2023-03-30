using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSpawn : MonoBehaviour
{
    public GameObject[] myObjects;
    private float timer;
    public float period;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > period)
        {
            int randomIndex = Random.Range(0, myObjects.Length);
            Vector2 randomSpawnPosition = transform.position + new Vector3(Random.Range(-DungeonData.RoomWidth/2.5f, DungeonData.RoomWidth/2.5f),
                Random.Range(-DungeonData.RoomHeight/3.5f, DungeonData.RoomHeight/3.5f), 0);
            Instantiate(myObjects[randomIndex], randomSpawnPosition, Quaternion.identity);
            timer = 0;
        }
    }
}
