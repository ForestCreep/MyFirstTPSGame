using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public GameObject[] weapons;// 武器列表
    public float shootFlashDisappearTime = 0.05f;// 枪口火光消失时间
    private GameObject _currentGun;// 当前手持武器
    private float _lastShootTime = 0;// 上一次射击时间
    private Transform shootFlash;// 射击火光位置
    private Weapon _currentWeapon;// 当前武器类

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentGun && Input.GetButton("Fire1"))
        {
            ShootRay();
        }
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
    }

    private void ShootRay()
    {
        //var ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 20);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.name);
            var shootSoundSource = _currentGun.GetComponent<AudioSource>();
            if (shootSoundSource)
            {
                _currentWeapon = _currentGun.GetComponent<Weapon>();
                if (_currentWeapon && Time.time - _lastShootTime >= _currentWeapon.shotInterval)
                {
                    Debug.Log(Time.time);
                    shootSoundSource.Play();
                    _lastShootTime = Time.time;

                    shootFlash = _currentGun.transform.Find("MuzzleFlash");
                    if (shootFlash)
                    {
                        shootFlash.gameObject.SetActive(true);
                        StartCoroutine("HideShootFlash");
                        //Invoke("HideShootFlash", shootFlashDisappearTime);
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

    //private void HideShootFlash()
    //{
    //    shootFlash.gameObject.SetActive(false);
    //}
}
