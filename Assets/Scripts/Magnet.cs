using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float charge = 30f; // It is actually charge not magnet! This is the intensity of the "magnet". 1 is too small, adjust this
    // public Vector3 pos = transform.position; // actually 2D, position of the magnet
    public float effRadius = 5.0f; // effective raduis

    // private Vector3 ballPos = player.transform.position;
    private Vector3 ballPos; // the position of ball import here
    private float distBall; // distance from magnet to ball
    public Vector3 subForce; // maybe make it static

    public GameObject player; // binding to player in start
    // public GameObject effCircle;
    public LineRenderer circleRenderer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0]; // assume there is only one player

        circleRenderer = gameObject.AddComponent<LineRenderer>();
        circleRenderer.material = new Material(Shader.Find("Diffuse"));
        circleRenderer.startWidth = 0.1f;
        circleRenderer.endWidth = 0.1f;
        Color color = Color.black;
        circleRenderer.startColor = color;
        circleRenderer.endColor = color;

        // DrawCircle(effRadius);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length == 0) {
            return;
        }
        player = GameObject.FindGameObjectsWithTag("Player")[0]; // assume there is only one player
        if (player == null) {
            return;
        }

        Vector3 pos = transform.position;
        ballPos = player.transform.position; // update ball position
        Vector3 dist = pos - ballPos;
        // Debug.Log(dist);

        // NOTE: set every z = 0, otherwise, it does not work

        distBall = Mathf.Sqrt(Vector3.Dot(dist, dist));
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
        DrawCircle(effRadius);
    }

    void DrawCircle(float radius) {
        int steps = 1000;
        circleRenderer.positionCount = steps+3;
        for (int i = 0; i <= steps+2; i++)
        {
            float theta = 2 * Mathf.PI * i / ((float) steps);
            float x = transform.position.x + radius * Mathf.Cos(theta);
            float y = transform.position.y + radius * Mathf.Sin(theta);
            Vector3 currentPosition = new Vector3(x, y, 0);
            circleRenderer.SetPosition(i, currentPosition);
        }
    }

}
