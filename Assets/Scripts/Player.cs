using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // public float charge = 1f; 
    public Vector3 velosity; // initial velosity with direction specified by the user
    private Vector3 force; // current total force on the ball
    const float STEP = 0.0001f; // STEP, should be const

    void totalForce(){
        force = new Vector3(0,0,0);
        var objs = GameObject.FindGameObjectsWithTag("Magnet");
        foreach (var obj in objs)
        {
            force += obj.GetComponent<Magnet>().subForce;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        totalForce();

        Vector3 deltaPosition = velosity * STEP;
        transform.position += deltaPosition;

        velosity += force * STEP;
    }
}
