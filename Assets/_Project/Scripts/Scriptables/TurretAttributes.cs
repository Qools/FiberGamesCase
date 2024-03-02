using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretAttributes", menuName = "ScriptableObjects/TurretAttributes", order = 1)]
public class TurretAttributes : ScriptableObject
{
    public GameObject projectile;

    public float range = 15f;
    public float turretRotationSpeed = 0.25f;
    public float fireRate = 1f;
}
