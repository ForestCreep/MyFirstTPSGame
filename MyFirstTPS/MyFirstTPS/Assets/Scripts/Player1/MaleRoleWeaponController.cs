using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaleRoleWeaponController : MonoBehaviour
{
    public GameObject[] weapons;// 武器列表
    public float shootFlashDisappearTime = 0.05f;// 枪口火光消失时间
    private Animator _animator;// 人物动画
    private GameObject _currentGun;// 当前手持武器
    private float _lastShootTime = 0;// 上一次射击时间
    private Transform shootFlash;// 射击火光位置
    private Weapon _currentWeapon;// 当前武器类
    public GameObject bulletHole;// 弹孔

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
    }

    private void OnTriggerStay(Collider other)
    {
        // 销毁地上的枪支
        if (other.name == "Terrain")
        {
            return;
        }

        if (Input.GetKey(KeyCode.F))
            PickUpWeapon(other);
    }

    private void PickUpWeapon(Collider other)
    {
        foreach (var weapon in weapons)
        {
            if (weapon.name == other.name)
            {
                _animator.SetBool("HasWeapon", true);
                if (_currentGun)
                {
                    // 每次拾取枪时判断手中是否持枪，并将新生成的枪生成于合适的位置
                    _currentGun.SetActive(false);
                    // 设置新生成的枪的类型，位置，旋转
                    var newWeapon = Instantiate(_currentGun, transform.position + Vector3.forward * 3 + new Vector3(0, 0.1f, 0), Quaternion.identity);
                    //var newWeapon = Instantiate(_currentGun);
                    //newWeapon.transform.eulerAngles = new Vector3(-90, 0, 0);
                    //newWeapon.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z + 0.3f);

                    //var weaponRigidbody = newWeapon.AddComponent<Rigidbody>();
                    //newWeapon.GetComponent<BoxCollider>().isTrigger = false;
                    //var direction = transform.TransformDirection(transform.forward);
                    //weaponRigidbody.AddForce(direction, ForceMode.Impulse);
                    //StartCoroutine("ResetWeapon", newWeapon);

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
        Destroy(other.gameObject);
        Debug.Log(other.name);
    }

    //private IEnumerator ResetWeapon(GameObject newWeapon)
    //{
    //    yield return new WaitForSeconds(1);
    //    Destroy(newWeapon.GetComponent<Rigidbody>());
    //    newWeapon.GetComponent<BoxCollider>().isTrigger = true;
    //}

    private void Shoot()
    {
        //var ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 20);
        var shootSoundSource = _currentGun.GetComponent<AudioSource>();
        if (shootSoundSource)
        {
            _currentWeapon = _currentGun.GetComponent<Weapon>();
            if (_currentWeapon && Time.time - _lastShootTime >= _currentWeapon.shotInterval)
            {
                //Debug.Log(Time.time);
                if (_animator)
                {
                    shootSoundSource.Play();
                    _lastShootTime = Time.time;
                    //_animator.SetTrigger("IsShoot");
                    shootFlash = _currentGun.transform.Find("MuzzleFlash");
                    if (shootFlash)
                    {
                        shootFlash.gameObject.SetActive(true);
                        StartCoroutine("HideShootFlash");
                        //Invoke("HideShootFlash", shootFlashDisappearTime);
                    }

                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        Debug.Log(hit.collider.name);
                        // 贴合弹孔

                        // 击中敌人使其掉血
                        hit.collider.SendMessage("ReceiveDamage", _currentWeapon.DamageValue, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
        }
    }

    private IEnumerator HideShootFlash()
    {
        yield return new WaitForSeconds(shootFlashDisappearTime);
        shootFlash.gameObject.SetActive(false);
    }
}
