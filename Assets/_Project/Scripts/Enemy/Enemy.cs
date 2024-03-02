using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyAnimationController enemyAnimattor;

    public EnemyAttributes enemyAttributes;
    [SerializeField] private Image healthBar;

    private int enemyHealthPoints;
    private float _healthPoints;

    public bool isDying = false;
    private void Start()
    {
        enemyHealthPoints = enemyAttributes.healthPoints;
        _healthPoints = enemyAttributes.healthPoints;
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
}
