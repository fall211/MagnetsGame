using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float charge = 1f; // It is actually charge not magnet! This is the intensity of the "magnet". Set to 1 by default for simplicity. 
    // public Vector3 pos = transform.position; // actually 2D, position of the magnet
    public float effRadius = 10.0f; // effective raduis

    // private Vector3 ballPos = player.transform.position;
    private Vector3 ballPos; // the position of ball import here
    private float distBall; // distance from magnet to ball
    public Vector3 subForce; // maybe make it static

    public GameObject player; // binding to player in start

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0]; // assume there is only one player

        // Vector3 pos = transform.position;
        // ballPos = player.transform.position;
        // distBall = Mathf.Sqrt(Vector3.Dot(pos, pos));
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0]; // assume there is only one player
        if (player == null) {
            return;
        }

        Vector3 pos = transform.position;
        ballPos = player.transform.position; // update ball position
        Vector3 dist = pos - ballPos;
        // Debug.Log(dist);

        distBall = Mathf.Sqrt(Vector3.Dot(dist, dist));
        // Debug.Log(distBall);
        // Debug.Log(distBall);
        // Debug.Log(effRadius);
        // Debug.Log(distBall <= effRadius);

        // assign foce considering effective raduis
        if (distBall <= effRadius) {
            float subForceNorm = charge/(distBall*distBall);
            float theta = Mathf.Atan2(dist.y, dist.x);

            subForce = new Vector3(subForceNorm * Mathf.Cos(theta), subForceNorm * Mathf.Sin(theta), 0f);
        } else {
            subForce = new Vector3(0f,0f,0f);
        }
    }

}
