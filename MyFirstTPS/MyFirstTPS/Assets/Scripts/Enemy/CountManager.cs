using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountManager : MonoBehaviour
{
    public static CountManager Instance;// 唯一实例 
    private float _sumDamageEnemyToPlayer;
    public float _sumDamagePlayerToEnemy;

    private CountManager()
    {

    }

    // Use this for initialization
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDamageEnemyToPlayer(float value)
    {
        _sumDamageEnemyToPlayer += value;
    }

    public void OnDamagePlayerToEnemy(float value)
    {
        _sumDamagePlayerToEnemy += value;
    }
}
