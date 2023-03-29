using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float vertical;
    float horizontal;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxisRaw("Vertical")*speed;
        horizontal = Input.GetAxisRaw("Horizontal")*speed;

        transform.Translate(horizontal,vertical,0);

    }
}
