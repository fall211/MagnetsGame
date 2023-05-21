using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magnetMovement : MonoBehaviour
{

    [SerializeField] Vector2 beginning;
    [SerializeField] Vector2 end;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = beginning;
    }

    // Update is called once per frame
    void Update()
    {
        // Uses sin function to iterate between the beginning and end
        // Uses formula to oscillate between the two points:
        // Look here for reference: https://math.stackexchange.com/questions/3438653/why-lambda-x-1-lambday-lvert-lambda-in-0-1-represents-th

        float lambda = Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup));
        transform.position =  (1 - lambda) * beginning + lambda * end;
    }
}
