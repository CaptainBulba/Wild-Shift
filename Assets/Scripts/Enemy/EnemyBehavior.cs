using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform objectDetection;
    public float objectRange = 0.5f;
    public LayerMask objectLayer;
    public float jumpForce;

    private Rigidbody2D rb;
    private Animator an;
  
    bool isObjectNear = false;
    private bool inAir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
    }

    void Update()
    { 
        isObjectNear = Physics2D.OverlapCircle(objectDetection.position, objectRange, objectLayer);
        if (isObjectNear && !inAir)
        {
            an.Play("Bear-Jump", -1, 0f);
            rb.velocity = new Vector2(0, jumpForce);
            inAir = true;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Object" && inAir)
        {
            an.Play("Bear", -1, 0f);
            inAir = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (objectDetection == null) return;
        Gizmos.DrawWireSphere(objectDetection.position, objectRange);   
    }
}
