using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManagement : MonoBehaviour
{
    public static int numberOfX;
    public static int numberOfY;
    public static int numberOfZ;
    public static int numberOfH;
    public TextMeshProUGUI coinsText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = "X: " + numberOfX + " Y: " + numberOfY + " Z: " + numberOfZ + " H: " + numberOfH;
    }
}
