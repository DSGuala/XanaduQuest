using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTimeOut : MonoBehaviour
{   
    private float timer;
    public float lifeTime = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > lifeTime) 
        {
            Destroy(gameObject);
        
        }
    }
}
