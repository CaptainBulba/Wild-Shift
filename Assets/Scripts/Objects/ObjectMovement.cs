using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{ 
    private Rigidbody2D rb;
    private float speed;
    public float movementSpeed = 1f;
    public float direction;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (direction == 1)
        {
            speed = -movementSpeed;
        }
        else
        {
            speed = movementSpeed;
        }
    }
   
    void Update()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }
}
