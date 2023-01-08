using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using VJ.Assets.Scripts.Networking;

namespace VJ.Assets.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : NetworkBehaviour
    {
        public float speed;
        private Rigidbody2D rb;

        private Vector2 movementHor;
        private Vector2 movementVer;

        private void Start()
        {
            if(isLocalPlayer)
            {
                FollowCamera.instance.target = transform;
            }

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
}