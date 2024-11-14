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
    private void OnEnable()
    {
        _playerAttackZone.AddEnemyInList += ParticlesInGunPlay;
    }
    private void OnDisable()
    {
        _playerAttackZone.AddEnemyInList -= ParticlesInGunPlay;
    }
    private void Start()
    {
        ParticlesInGunStop();
    }
    private void FixedUpdate()
    {
        if (_playerAttackZone.ClosestEnemy != null)
        {
            Shot();
        }
        else
        {
            ParticlesInGunStop();
        }
    }
    private void Shot()
    {
        _firePoint.LookAt(_playerAttackZone.ClosestEnemy.transform);

        Ray raycast = new Ray(_firePoint.position, _firePoint.forward);

        if (Physics.Raycast(raycast, out RaycastHit hit, _fireDistance))
        {
            if (hit.transform.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_fireDamage);
            }
        }
    }
    private void ParticlesInGunPlay()
    {
        if (_weaponBulletsCasings.isPlaying == false && _weaponFire.isPlaying == false)
        {
            _weaponFire.Play();
            _weaponBulletsCasings.Play();
        }
    }
    private void ParticlesInGunStop()
    {
        if (_weaponBulletsCasings.isPlaying && _weaponFire.isPlaying)
        {
            _weaponFire.Stop();
            _weaponBulletsCasings.Stop();
        }
    }
}