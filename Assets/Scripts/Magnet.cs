using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public double charge = 1f; // It is actually charge not magnet! This is the intensity of the "magnet". Set to 1 by default for simplicity. 
    public Vector3 pos; // actually 2D, position of the magnet
    public double effRadius = 1f; // effective raduis

    // private Vector3 ballPos = player.transform.position;
    private Vector3 ballPos; // the position of ball import here
    private double distBall;
    public double subForce;

    // Start is called before the first frame update
    void Start()
    {
        ballPos = player.transform.position;
        distBall = Mathf.Sqrt(Vector3.Dot(pos, pos));
    }

    // Update is called once per frame
    void Update()
    {
        ballPos = player.transform.position; // update ball position
        distBall = Mathf.Sqrt(Vector3.Dot(pos, pos));

        // assign foce considering effective raduis
        if (distBall <= effRadius) {
            subForce = charge/(distBall*distBall);
        } else {
            subForce = 0;
        }
    }

}
