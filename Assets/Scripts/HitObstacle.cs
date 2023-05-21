using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObstacle : MonoBehaviour
{
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
    }

    void ChangeSprite()
    {
        spriteRenderer.sprite = spriteArray[1];
    }

    private void OnCollisionEnter2D(Collision2D obj) 
    {
        if (obj.gameObject.CompareTag("Obstacle")) {
            var speed = lastVelocity.magnitude;
            var dir = Vector2.Reflect(lastVelocity.normalized, obj.contacts[0].normal);
            rb.velocity = dir * Mathf.Max(speed, 0f);
        } else if (obj.gameObject.CompareTag("Spike"))
        {
            ChangeSprite();
            rb.velocity = 0*rb.velocity;
        }

    }
}
