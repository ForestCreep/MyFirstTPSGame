using System.Collections;
using UnityEngine;

public class MaleRoleWeaponController : MonoBehaviour
{
    public GameObject[] Weapons;// 武器列表
    public float ShootFlashDisappearTime = 0.05f;// 枪口火光消失时间
    private Animator _animator;// 人物动画
    private GameObject _currentGun;// 当前手持武器
    private float _lastShootTime = 0;// 上一次射击时间
    private Transform _shootFlash;// 射击火光位置
    private Weapon _currentWeapon;// 当前武器类
    public GameObject[] BulletHole;// 弹孔

    // Use this for initialization
    void Start()
    {
        // 光标锁定
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
        foreach (var weapon in Weapons)
        {
            if (weapon.name == other.name)
            {
                // 无枪向有枪动画切换
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
        // 射线可视化绘制
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 200);
        var shootSoundSource = _currentGun.GetComponent<AudioSource>();
        if (shootSoundSource)
        {
            _currentWeapon = _currentGun.GetComponent<Weapon>();

            // 射击间隔必须大于等于武器最小射击间隔
            if (_currentWeapon && Time.time - _lastShootTime >= _currentWeapon.ShotInterval)
            {
                //Debug.Log(Time.time);
                if (_animator && UIManager.Instance.GetPlayerIsAlive())
                {
                    shootSoundSource.Play();
                    // 重置上一次射击时刻
                    _lastShootTime = Time.time;
                    //_animator.SetTrigger("IsShoot");

                    _shootFlash = _currentGun.transform.Find("MuzzleFlash");
                    if (_shootFlash)
                    {
                        _shootFlash.gameObject.SetActive(true);
                        // 开启协程特定时间灯光特效消失
                        StartCoroutine("HideShootFlash");
                        //Invoke("HideShootFlash", shootFlashDisappearTime);
                    }

                    // 射线发射
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        // 贴合弹孔
                        var hole = BulletHolesController.Instance.GetBulletHole(hit.transform.tag);
                        if (hole != null)
                        {
                            hole.transform.forward = hit.normal;
                            hole.transform.position = hit.point;
                            hole.transform.Rotate(0, 0, Random.Range(0, 360));
                            Destroy(hole, 3);
                        }

                        // 击中敌人使其掉血
                        hit.collider.SendMessage("ReceiveDamage", _currentWeapon.DamageValue, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
        }
    }

    private IEnumerator HideShootFlash()
    {
        yield return new WaitForSeconds(ShootFlashDisappearTime);
        _shootFlash.gameObject.SetActive(false);
    }

    /// <summary>
    /// 使枪始终指向看向点
    /// </summary>
    /// <param name="layerIndex"></param>
    private void OnAnimatorIK(int layerIndex)
    {
        if (_currentGun)
        {
            // 设置看向的位置
            var pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 10));
            pos = pos - _currentGun.transform.position;
            //_currentGun.transform.right = -pos;
            // 获取枪上的左手参考点
            var leftHandPosRef = _currentGun.GetComponent<Weapon>().LeftHandPos;
            // 设置左手IK位置
            _animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandPosRef.position);
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            // 设置左手IK旋转
            //_animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandPosRef.rotation);
            //_animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
        }
    }
}
