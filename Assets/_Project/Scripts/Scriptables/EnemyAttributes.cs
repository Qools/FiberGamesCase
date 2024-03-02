using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttributes", menuName = "ScriptableObjects/EnemyAttributes", order = 1)]
public class EnemyAttributes : ScriptableObject
{
    public GameObject deadEffect;

    public int healthPoints;
    public float speed;
    public float minDistanceToWaypoint;
    public int killReward;
    public float range;
    public int attackValue;
}
