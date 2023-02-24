using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectitle : MonoBehaviour
{
    private AttackDetails attackDetails;

    private float speed;
    private float travelDistance;
    private float xStartPos;

    [SerializeField]
    private float gravity;
    [SerializeField]
    private float damageRadius;

    private Rigidbody2D rb;
    private bool isGravityOn;
    private bool hasHitGround;

    [SerializeField]
    private LayerMask whatisGround;
    [SerializeField]
    private LayerMask whatisPlayer;
    [SerializeField]
    private Transform damagePosition;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0.0f;
        rb.velocity =transform.right * speed;

        isGravityOn = false;

        xStartPos = transform.position.x;
    }

    private void FixedUpdate()
    {
        if(!hasHitGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatisPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatisGround);
            //f cham abs gia tri tuyet doi vi tri bat dau - bien doi vi tri cham x l
            if(Mathf.Abs(xStartPos - transform.position.x) >= travelDistance && !isGravityOn)
            {
                isGravityOn = true;
                rb.gravityScale = gravity;
            }
        }
    }
}
