using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent _agent;
    private Animator _animator;

    // Use this for initialization
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
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
