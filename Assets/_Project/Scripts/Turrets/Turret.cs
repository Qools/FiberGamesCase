using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Turret : MonoBehaviour
{
    private Transform target;
    [SerializeField] private TurretAttributes turretAttributes;
    [SerializeField] private Transform partToRotate;
    [SerializeField] private Transform firePoint;

    private float fireRateCountDown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTargets", 0f, 0.5f);
    }

    private void UpdateTargets()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(PlayerPrefKeys.enemy);
        float shortesDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (var enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (enemy.GetComponent<Enemy>().isDying)
            {
                continue;
            }

            if (distanceToEnemy < shortesDistance)
            {
                shortesDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortesDistance <= turretAttributes.range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }


        LookAtTarget();

        CalculateFireRate();
    }

    private void CalculateFireRate()
    {
        if (fireRateCountDown <= 0f)
        {
            Shoot();

            fireRateCountDown = 1f / turretAttributes.fireRate;
        }

        fireRateCountDown -= Time.deltaTime;
    }

    private void Shoot()
    {
        GameObject projectileGO = Instantiate(turretAttributes.projectile, firePoint.position, firePoint.rotation);

        Projectile projectile = null;

        if (projectileGO.TryGetComponent(out Projectile _projectile))
        {
            projectile = _projectile;
        }

        projectile.SetTarget(target);
    }

    private void LookAtTarget()
    {
        float angle = Mathf.Atan2(
            target.position.y - transform.position.y, 
            target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        partToRotate.rotation = Quaternion.RotateTowards(
            partToRotate.rotation, 
            targetRotation, 
            turretAttributes.turretRotationSpeed * Time.deltaTime);
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.forward, turretAttributes.range);
    }
#endif
}
