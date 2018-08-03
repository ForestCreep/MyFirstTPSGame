using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent _agent;
    private Animator _animator;
    public float Hp = 100;// 生命上限
    public Transform revivePoint;// 复活点
    private bool _isAlive = true;

    // Use this for initialization
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Hp > 0)
        {
            if (_agent)
            {
                _agent.SetDestination(target.position);
                var velocity = transform.InverseTransformDirection(_agent.desiredVelocity);
                _animator.SetFloat("SpeedXWithoutWeapon", velocity.x);
                _animator.SetFloat("SpeedZWithoutWeapon", velocity.z);
            }
        }
    }

    private void ReceiveDamage(float value)
    {
        // hp=10;
        // value=30
        // hp=-20
        // -->120
        // value=10
        // -->130
        // -->100
        if (Hp > 0)
        {
            Hp -= value;
            CountManager.Instance.OnDamagePlayerToEnemy(value);
        }
        if (Hp <= 0)
        {
            value += Hp;
            if (_isAlive)
            {
                _isAlive = false;
                _animator.SetTrigger("EnemyIsDead");
                _agent.isStopped = true;// 停止追踪玩家
                CountManager.Instance.OnDamagePlayerToEnemy(Hp);
            }
        }
    }

    private void OnFinishedDead()
    {
        _animator.SetTrigger("Revive");
        _agent.isStopped = false;
        _isAlive = true;
        Hp = 100;
        transform.position = revivePoint.transform.position;
    }
}
