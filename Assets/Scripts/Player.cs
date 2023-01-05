using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(CharacterController))]
public class Player : NetworkBehaviour
{
    public float speed;
    public float gravity;
    private Vector3 yVel;
    private CharacterController cc;

    private void Start()
    {
        if (!isLocalPlayer) return;
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");

        if(cc.isGrounded)
        {
            yVel.y = -2f;
        }

        if(Input.GetKeyDown(KeyCode.W) && cc.isGrounded)
        {
            yVel.y = Mathf.Sqrt(gravity * -2f);
        }

        yVel.y += gravity * Time.deltaTime;
        Vector3 move = transform.right * x;

        cc.Move(move * speed * Time.deltaTime);
        cc.Move(yVel * Time.deltaTime);
    }
}
