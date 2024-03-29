using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VJ.Client;

namespace VJ.UI.Game
{
    public class ScoreboardItem : MonoBehaviour
    {
        public Text pName;
        public Text pScore;
        public void InitializeItem(PlayerTag playerTag)
        {
            pName.text = playerTag.playerName;
            pScore.text = playerTag.score.ToString();
        }
    }
}