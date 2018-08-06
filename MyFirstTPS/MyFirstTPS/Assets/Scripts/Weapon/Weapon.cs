using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float ShotInterval = 0;// 射击间隔
    public int Id;// 武器ID
    public string WeaponName;// 武器名
    public int BaseCapacity;// 基础弹夹容量
    public int ExpandedCapacity;// 扩容后弹夹容量
    public float DamageValue = 0;// 武器伤害
    public Transform LeftHandPos;// 左手位置

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
