using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawn : MonoBehaviour
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
            Vector2 randomSpawnPosition = new Vector2(Random.Range(-8, 9), Random.Range(-4, 5));
            Instantiate(myObjects[randomIndex], randomSpawnPosition, Quaternion.identity);
            timer = 0;
        }
    }
}
