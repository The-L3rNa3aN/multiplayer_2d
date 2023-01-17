using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using VJ.Networking;
using VJ.UI.Game;

namespace VJ.Client
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : NetworkBehaviour
    {
        private Rigidbody2D rb;
        private GameUIManager gameUI;

        public float speed;
        private Vector2 movementHor;

        [Header("Gravity")]
        public float gConstant;
        public LayerMask groundMask;
        private Vector2 gravity;
        [SerializeField] private bool isGrounded;

        private void Start()
        {
            if(isLocalPlayer)
            {
                FollowCamera.instance.target = transform;
                if(isServer)
                    CustomNetworkManager.instance.isPlayerServer = true;
            }

            rb = GetComponent<Rigidbody2D>();
            gameUI = FindObjectOfType<GameUIManager>();
        }

        private void Update()
        {
            if (!isLocalPlayer) return;

            //Gravity code here.
            isGrounded = Physics2D.Raycast(transform.position, -transform.up, 0.6f, groundMask);

            if(!gameUI.isPaused)
                movementHor = new Vector2(Input.GetAxis("Horizontal"), 0f);
        }

        private void FixedUpdate()
        {
            if(isGrounded && gravity.y < 0f)
                gravity.y = -2f;

            gravity.y += gConstant * Time.fixedDeltaTime;
            rb.velocity = movementHor * speed + gravity * Time.fixedDeltaTime;
        }
    }
}