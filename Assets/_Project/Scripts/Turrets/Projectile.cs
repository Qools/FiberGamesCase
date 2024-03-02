using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileAttributes projectileAttributes;
    private Transform target;

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Movement();
        LookAtTarget();
    }

    private void Movement()
    {
        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = projectileAttributes.speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }


        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    private void LookAtTarget()
    {
        float angle = Mathf.Atan2(
           target.position.y - transform.position.y,
           target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            projectileAttributes.projectileRotationSpeed * Time.deltaTime);
    }

    private void HitTarget()
    {
        if (projectileAttributes.particleEffect != null)
        {
            GameObject particleEffect = Instantiate(projectileAttributes.particleEffect, transform.position, transform.rotation);

            Destroy(particleEffect, 2f);
        }


        if (projectileAttributes.explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            DamageTarget(target);
        }

        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, projectileAttributes.explosionRadius);

        foreach (var collider in colliders)
        {
            if (collider.CompareTag(PlayerPrefKeys.enemy))
            {
                DamageTarget(collider.transform);
            }
        }
    }

    private void DamageTarget(Transform enemy)
    {
        if (enemy.TryGetComponent(out Enemy _enemy))
        {
            _enemy.TakeDamage(projectileAttributes.damage);
        }
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, projectileAttributes.explosionRadius);
    }
}
