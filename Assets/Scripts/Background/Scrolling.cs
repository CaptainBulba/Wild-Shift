using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public float backScrollSpeed = -5f;
    public float foreScrollSpeed = -4f;
    private float scrollSpeed;

    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private PolygonCollider2D pc;

    private float lenght;
 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (gameObject.layer == LayerMask.NameToLayer("Background"))
        {
            bc = GetComponent<BoxCollider2D>();
            scrollSpeed = backScrollSpeed;
            lenght = bc.size.x;
        }

        if (gameObject.layer == LayerMask.NameToLayer("Foreground"))
        {
            pc = GetComponent<PolygonCollider2D>();
            scrollSpeed = foreScrollSpeed;
            lenght = pc.bounds.size.x;
        }

        rb.velocity = new Vector2(scrollSpeed, 0);
    }
    void Update()
    {
        if (transform.position.x > lenght && scrollSpeed > 0)
        {
            Vector2 backgroundOffset = new Vector2(lenght * -2f, 0);
            transform.position = (Vector2)transform.position + backgroundOffset;
        }

        else if (transform.position.x < -lenght && scrollSpeed < 0)
        {
            Vector2 backgroundOffset = new Vector2(lenght * 2f, 0);
            transform.position = (Vector2)transform.position + backgroundOffset;
        }
    }
}