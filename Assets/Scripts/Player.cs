using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // public float charge = 1f; 
    public Vector3 velosity; // initial velosity with direction specified by the user
    public Vector3 force; // current total force on the ball
    const float STEP = 0.001f; // STEP, should be const, 0.001 would fast but not that accurate

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
