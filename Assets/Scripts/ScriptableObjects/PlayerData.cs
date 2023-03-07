using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public int health;
    public int maxHealth;
    public int lives;
    public int score;
    public Vector3 currentPosition;
}
