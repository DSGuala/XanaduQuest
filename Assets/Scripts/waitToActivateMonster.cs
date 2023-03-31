using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waitToActivateMonster : MonoBehaviour
{

    public GameObject monster;
    private float timer = 0;
    public float timetowait = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer+=Time.deltaTime;
        if (timer > timetowait) 
        {
            monster.SetActive(true);
            Destroy(gameObject);
        
        }   
    }
}
