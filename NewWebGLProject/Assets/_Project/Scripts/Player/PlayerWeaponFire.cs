using UnityEngine;

public class PlayerWeaponFire : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;

    [Header("Particles"), Space]
    [SerializeField] private ParticleSystem _weaponFire;

    private float _fireDistance;
    private int _fireDamage;
    private PlayerAttackZone _playerAttackZone;

    private void Awake() => _playerAttackZone = FindAnyObjectByType<PlayerAttackZone>();
    private void OnEnable() => _playerAttackZone.AddEnemyInList += ParticlesInGunPlay;
    private void OnDisable()
    {
        _playerAttackZone.AddEnemyInList -= ParticlesInGunPlay;
        _weaponFire.Stop();
    }
    private void Start()
    {
        ParticlesInGunStop();
        GlobalStatesWhenPlayerDied.Instance.AddBehaviourInListToSetEnebledFalseWhenPlayerDied(this);

        _fireDistance = PlayerStates.Instance.AttackDistance;
        _fireDamage = (int)PlayerStates.Instance.AttackDamage;
    }
    private void FixedUpdate()
    {
        if (_playerAttackZone.ClosestEnemy != null)
            Shot();
        else
            ParticlesInGunStop();
    }
    private void Shot()
    {
        _firePoint.LookAt(_playerAttackZone.ClosestEnemy.transform);

        Ray raycast = new Ray(_firePoint.position, _firePoint.forward);

        if (Physics.Raycast(raycast, out RaycastHit hit, _fireDistance))
        {
            if (hit.transform.TryGetComponent(out Enemy enemy))
                enemy.TakeDamage(_fireDamage);
        }
    }
    private void ParticlesInGunPlay()
    {
        if (_weaponFire.isPlaying == false)
            _weaponFire.Play();
    }
    private void ParticlesInGunStop()
    {
        if (_weaponFire.isPlaying)
            _weaponFire.Stop();
    }
}