using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform Target;
    private NavMeshAgent _agent;
    private Animator _animator;
    public float Hp = 100;// 生命上限
    public Transform RevivePoint;// 复活点
    private bool _isAlive = true;
    public float MaxShootDistance;
    public GameObject[] Weapons;
    private GameObject _currentGun;
    private Weapon _currentWeapon;
    private float _lastShootTime;
    private Transform _shootFlash;
    public float ShootFlashDisappearTime = 0.05f;

    // Use this for initialization
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        // 随机获取一把枪支
        GetWeaponRandomly();
    }

    private void GetWeaponRandomly()
    {
        var weaponIndex = Random.Range(0, Weapons.Length);
        foreach (var item in Weapons)
        {
            if (item.name == Weapons[weaponIndex].name)
            {
                item.SetActive(true);
                _currentGun = item;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Hp > 0)
        {
            _agent.SetDestination(Target.position);
            var velocity = transform.InverseTransformDirection(_agent.desiredVelocity);
            _animator.SetFloat("SpeedXWithWeapon", velocity.x);
            _animator.SetFloat("SpeedZWithWeapon", velocity.z);

            // 当达到最大射程范围时可以向玩家射击
            if (Vector3.Distance(transform.position, Target.position) <= MaxShootDistance)
            {
                ShootToPlayer();
                transform.LookAt(Target);
            }
        }
    }

    private void ShootToPlayer()
    {
        var enemyPosition = transform.position;
        var targetPosition = Target.position;
        var direction = Target.position - transform.position;

        // 向玩家发射射线
        Ray ray = new Ray(new Vector3(enemyPosition.x, enemyPosition.y + 1.4f, enemyPosition.z), new Vector3(direction.x, direction.y, direction.z));
        Debug.DrawRay(ray.origin, ray.direction * 20);

        var shootSoundSource = _currentGun.GetComponent<AudioSource>();
        if (shootSoundSource)
        {
            _currentWeapon = _currentGun.GetComponent<Weapon>();
            if (_currentWeapon && Time.time - _lastShootTime >= 3)
            {
                //Debug.Log(Time.time);
                if (_animator)
                {
                    shootSoundSource.Play();
                    _lastShootTime = Time.time;
                    //_animator.SetTrigger("IsShoot");
                    _shootFlash = _currentGun.transform.Find("MuzzleFlash");
                    if (_shootFlash)
                    {
                        _shootFlash.gameObject.SetActive(true);
                        StartCoroutine("HideShootFlash");
                    }

                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        Debug.Log(hit.collider.name);
                        // 贴合弹孔

                        // 击中玩家使其掉血
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

    private void ReceiveDamage(float value)
    {
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

        CountManager.Instance.SetEnemyHp(Hp);
    }

    private void OnFinishedDead()
    {
        _animator.SetTrigger("Revive");
        _isAlive = true;
        Hp = 100;
        transform.position = RevivePoint.transform.position;
        _agent.isStopped = false;
    }
}
