using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject[] weapons;
    private Animator _animator;
    private GameObject _currentGun;

    // Use this for initialization
    void Start()
    {
        Debug.Log(Quaternion.identity);
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //ShootReaction();
    }

    private void ShootReaction()
    {
        if (Input.GetMouseButton(0))
        {
            _animator.SetTrigger("Shoot");
        }
        else
        {
            _animator.ResetTrigger("Shoot");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        // 先播放拾取动画

        // 每次拾取枪时判断手中是否持枪，并将新生成的枪生成于合适的位置
        if (_currentGun != null)
        {
            _currentGun.SetActive(false);
            // 设置新生成的枪的类型，位置，旋转
            var newWeapon = Instantiate(_currentGun, transform.position + Vector3.forward * 3 + new Vector3(0, 0.1f, 0), Quaternion.identity);
            newWeapon.name = _currentGun.name;
            _currentGun = null;
            newWeapon.SetActive(true);
        }

        // 拾取枪支判断
        foreach (var weapon in weapons)
        {
            if (weapon.name == other.name)
            {
                _currentGun = weapon;
                weapon.SetActive(true);
            }
            else
            {
                weapon.SetActive(false);
            }
        }

        Destroy(other.gameObject);
        //DestroyImmediate(other.gameObject);
    }
}
