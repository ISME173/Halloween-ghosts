using UnityEngine;

public class PlayerWeaponFire : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireDistance;
    [SerializeField] private int _fireDamage;

    [Header("Particles"), Space]
    [SerializeField] private ParticleSystem _weaponFire;
    [SerializeField] private ParticleSystem _weaponBulletsCasings;

    private PlayerAttackZone _playerAttackZone;

    private void Awake() => _playerAttackZone = FindAnyObjectByType<PlayerAttackZone>();
    private void Start()
    {
        _weaponFire.Stop();
        _weaponBulletsCasings.Stop();
    }
    private void FixedUpdate()
    {
        if (_playerAttackZone.ClosestEnemy != null)
        {
            _weaponBulletsCasings.Play();
            _weaponFire.Play();
            Shot();
        }
        //else
        //{
        //    _weaponFire.Stop();
        //    _weaponBulletsCasings.Stop();
        //}
    }
    private void Shot()
    {
        _firePoint.LookAt(_playerAttackZone.ClosestEnemy.transform);

        Ray raycast = new Ray(_firePoint.position, _firePoint.forward);

        RaycastHit hit;
        if (Physics.Raycast(raycast, out hit, _fireDistance))
        {
            if (hit.transform.TryGetComponent(out Enemy enemy))
            {
                Debug.Log("Fire");
                //_weaponFire.Play();
                //_weaponBulletsCasings.Play();
                enemy.TakeDamage(_fireDamage);
            }
        }
    }
}