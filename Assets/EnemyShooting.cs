using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject[] myFlames;
    public Transform flamePos;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 2)
        {
            timer = 0;
            shoot();
        }
    }

    void shoot()
    {
        int randomIndex = Random.Range(0, myFlames.Length);
        Instantiate(myFlames[randomIndex], flamePos.position, Quaternion.identity);
    }

}