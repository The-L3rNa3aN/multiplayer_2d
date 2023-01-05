using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : NetworkBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    private Vector2 movementHor;
    private Vector2 movementVer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isLocalPlayer) return;
        movementHor = new Vector2(Input.GetAxis("Horizontal"), movementHor.y);
    }

    private void FixedUpdate()
    {
        rb.velocity = (movementHor + movementVer) * speed;
    }
}
