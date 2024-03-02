using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(EnemyAnimationController))]
[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyAnimationController enemyAnimattor;
    [SerializeField] private EnemyMovement enemyMovement;

    public EnemyAttributes enemyAttributes;
    [SerializeField] private Image healthBar;

    [SerializeField] private Transform _firePoint;
    private Transform _target;

    public int id;
    private int enemyHealthPoints;
    private float _healthPoints;
    private float _fireRateCountDown = 0f;

    private string _attackAnimationName;

    public bool isDying = false;
    public bool isRanged;
    private bool _isAttacking = false;

    private void Start()
    {
        _isAttacking = false;

        enemyHealthPoints = enemyAttributes.healthPoints;
        _healthPoints = enemyAttributes.healthPoints;

        enemyAnimattor = GetComponent<EnemyAnimationController>();
        enemyMovement = GetComponent<EnemyMovement>();

        if (isRanged)
        {
            _attackAnimationName = PlayerPrefKeys.enemyShot;
        }

        else
        {
            _attackAnimationName = PlayerPrefKeys.enemySlash;
        }

        InvokeRepeating(nameof(UpdateTargets), 0f, 0.1f);
    }

    private void Update()
    {
        if (!_isAttacking)
        {
            return;
        }

        _calculateFireRate();
    }

    private void _calculateFireRate()
    {
        if (_fireRateCountDown <= 0f)
        {
            _attack();

            _fireRateCountDown = 1f / enemyAttributes.attackRate;
        }

        _fireRateCountDown -= Time.deltaTime;
    }

    private void _attack()
    {
        if (isRanged)
        {
            GameObject projectileGO = Instantiate(enemyAttributes.projectile, _firePoint.position, _firePoint.rotation, this.transform.parent);

            if (projectileGO.TryGetComponent(out Projectile _projectile))
            {
                _projectile.SetTarget(_target);
            }
        }

        else
        {
            BusSystem.CallLivesReduced(enemyAttributes.attackValue);
        }

    }

    public void TakeDamage(int _damage)
    {
        if (isDying)
        {
            return;
        }

        enemyHealthPoints -= _damage;
        _healthPoints -= _damage;

        healthBar.DOFillAmount(_healthPoints / enemyAttributes.healthPoints, 0.5f);

        if (enemyHealthPoints <= 0)
        {
            isDying = true;

            EnemyDeath();
        }
    }

    public void EnemyDeath()
    {
        enemyAnimattor.OnEnemyDeath();

        PlayDeathEffect();

        AddRewardMoney();
        Destroy(gameObject, 1f);
    }

    private void PlayDeathEffect()
    {
        GameObject vfx = Instantiate(enemyAttributes.deadEffect, transform.position, Quaternion.identity);

        Destroy(vfx, 1f);
    }

    private void AddRewardMoney()
    {
        PlayerStats.Money += enemyAttributes.killReward;
        Debug.Log(PlayerStats.Money);
    }

    private void OnDestroy()
    {
        PlayerStats.KilledEnemy++;

        BusSystem.CallEnemyDestroyed();
    }

    private void UpdateTargets()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(PlayerPrefKeys.castle);
        float shortesDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (var enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortesDistance)
            {
                shortesDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortesDistance <= enemyAttributes.range)
        {
            _target = nearestEnemy.transform;
            enemyMovement.EndPath();
            enemyAnimattor.PlayAttackAnimation(_attackAnimationName);
            _isAttacking = true;

            CancelInvoke(nameof(UpdateTargets));
        }
    }
}
