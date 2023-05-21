using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 pos; // position of player/ball
    // public double charge = 1f; 
    public Vector3 velosity; // initial velosity with direction specified by the user
    private double force; // current total force on the ball

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        force = 0;
        
    }
}
