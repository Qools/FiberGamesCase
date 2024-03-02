using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void OnEnemyDeath()
    {
        animator.SetTrigger(PlayerPrefKeys.enemyDead);
    }
}
