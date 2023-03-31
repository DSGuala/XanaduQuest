using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterTimerManager : MonoBehaviour
{
    TimerScript timerScript;
    public float thisMonsterTime;
    // Start is called before the first frame update
    void Start()
    {
        timerScript = GameObject.Find("World Manager").GetComponent<TimerScript>();
        timerScript.TimeLeft = thisMonsterTime;
        timerScript.TimerOn = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
