using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObstacle : MonoBehaviour
{
    Quaternion startRotation;
    Quaternion endRotation;
    float rotationProgress = -1;
    private Rigidbody2D rb;
    Vector2 lastVelocity;
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        spriteRenderer.sprite = spriteArray[0];
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rb.velocity;
        if (rotationProgress < 1 && rotationProgress >= 0){
            rotationProgress += Time.deltaTime * (float)1;

            // Here we assign the interpolated rotation to transform.rotation
            // It will range from startRotation (rotationProgress == 0) to endRotation (rotationProgress >= 1)
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, rotationProgress);
        }
        if (rotationProgress >=1)
        {
            ChangeSprite(1);
            Vector3 newRotation = new Vector3(0, 0, 180);
            transform.eulerAngles = newRotation;
        }
    }

    void ChangeSprite(int index)
    {
        spriteRenderer.sprite = spriteArray[index];
    }

    private void OnCollisionEnter2D(Collision2D obj) 
    {
        if (obj.gameObject.CompareTag("Obstacle")) {
            var speed = lastVelocity.magnitude;
            var dir = Vector2.Reflect(lastVelocity.normalized, obj.contacts[0].normal);
            rb.velocity = dir * Mathf.Max(speed, 0f);
        } else if (obj.gameObject.CompareTag("Spike"))
        {
            rb.velocity = 0*rb.velocity;
            Vector3 newRotation = new Vector3(0, 0, 0);
            transform.eulerAngles = newRotation;
            StartRotating(-180);
        } else if (obj.gameObject.CompareTag("Finish"))
        {
            rb.velocity = 0*lastVelocity;
            Destroy(obj.gameObject);
            ChangeSprite(2);
        }

    }

    // Call this to start the rotation
    void StartRotating(float zPosition)
    {
        // Here we cache the starting and target rotations
        startRotation = transform.rotation;
        endRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, zPosition);

        // This starts the rotation, but you can use a boolean flag if it's clearer for you
        rotationProgress = 0;
    }
}

