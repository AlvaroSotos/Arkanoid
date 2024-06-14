using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Arkanoid
{
    public enum VausState
    {
        Normal, Enlarged, Laser, Catch, Destruction
    }
    public enum PowerUpType
    {
        NoPowerUp, Enlarged, Catch, Laser, Disruption, Player, Break, Slow, Enemy
    }
    public class Constants : MonoBehaviour
    {

        public const string HIGHSCOREARKANOIDKEY = "HighScore";
    }
}

