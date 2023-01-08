using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace VJ.Assets.Scripts
{
    public class FollowCamera : MonoBehaviour
    {
        public Transform target;
        public static FollowCamera instance;

        private void Awake()
        {
            //Singleton
            if (instance != null && instance != this)
                Destroy(this);
            else
                instance = this;
        }
    }
}
