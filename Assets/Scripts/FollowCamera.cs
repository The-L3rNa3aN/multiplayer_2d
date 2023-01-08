using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace VJ.Assets.Scripts
{
    public class FollowCamera : MonoBehaviour
    {
        public Transform target;                        //An error will pop-up at the start. Not game-breaking, and there's nothing you can do about it.
        public Vector3 offset;
        public float smooth = 0.125f;
        public static FollowCamera instance;

        private void Awake()
        {
            //Singleton
            if (instance != null && instance != this)
                Destroy(this);
            else
                instance = this;
        }

        private void LateUpdate()
        {
            Vector3 desiredPos = target.position + offset;
            Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smooth);
            transform.position = smoothPos;
        }
    }
}
