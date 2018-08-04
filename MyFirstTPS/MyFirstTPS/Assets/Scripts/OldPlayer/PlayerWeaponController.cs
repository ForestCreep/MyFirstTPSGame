using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public GameObject[] Weapons;// 武器列表
    public float ShootFlashDisappearTime = 0.05f;// 枪口火光消失时间
    private Animator _animator;// 人物动画
    private GameObject _currentGun;// 当前手持武器
    private float _lastShootTime = 0;// 上一次射击时间
    private Transform _shootFlash;// 射击火光位置
    private Weapon _currentWeapon;// 当前武器类

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentGun && Input.GetButton("Fire1"))
        {
            Shoot();
        }
        //else
        //{
        //    if(_animator)
        //    {
        //        _animator.ResetTrigger("IsShoot");
        //    }
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        // 销毁地上的枪支
        if (other.name == "Terrain")
        {
            return;
        }
        Destroy(other.gameObject);
        Debug.Log(other.name);
        // 先播放拾取动画
        // 拾取枪支判断
        foreach (var weapon in Weapons)
        {
            if (weapon.name == other.name)
            {
                if (_currentGun)
                {
                    // 每次拾取枪时判断手中是否持枪，并将新生成的枪生成于合适的位置
                    _currentGun.SetActive(false);
                    // 设置新生成的枪的类型，位置，旋转
                    var newWeapon = Instantiate(_currentGun, transform.position + Vector3.forward * 3 + new Vector3(0, 0.1f, 0), Quaternion.identity);
                    newWeapon.name = _currentGun.name;
                    _currentGun = null;
                    newWeapon.SetActive(true);
                }

                _currentGun = weapon;
                weapon.SetActive(true);
            }
            else
            {
                weapon.SetActive(false);
            }
        }
    }

    private void Shoot()
    {
        //var ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 20);
        var shootSoundSource = _currentGun.GetComponent<AudioSource>();
        if (shootSoundSource)
        {
            _currentWeapon = _currentGun.GetComponent<Weapon>();
            if (_currentWeapon && Time.time - _lastShootTime >= _currentWeapon.ShotInterval)
            {
                //Debug.Log(Time.time);
                if(_animator)
                {
                    shootSoundSource.Play();
                    _lastShootTime = Time.time;
                    //_animator.SetTrigger("IsShoot");
                    _shootFlash = _currentGun.transform.Find("MuzzleFlash");
                    if (_shootFlash)
                    {
                        _shootFlash.gameObject.SetActive(true);
                        StartCoroutine("HideShootFlash");
                        //Invoke("HideShootFlash", shootFlashDisappearTime);
                    }
                }
            }
        }
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.name);
        }
    }

    private IEnumerator HideShootFlash()
    {
        yield return new WaitForSeconds(ShootFlashDisappearTime);
        _shootFlash.gameObject.SetActive(false);
    }

    //private void HideShootFlash()
    //{
    //    shootFlash.gameObject.SetActive(false);
    //}
}
