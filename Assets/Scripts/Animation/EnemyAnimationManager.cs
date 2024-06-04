using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyAnimationManager: MonoBehaviour
{
    [SerializeField] private EnemyController _enemy;

    [Header("Components")]
    [SerializeField] private Animator _animator;

    public void FinishAttackAnimation()
    {
        _enemy.ToAttack();
    }
}
