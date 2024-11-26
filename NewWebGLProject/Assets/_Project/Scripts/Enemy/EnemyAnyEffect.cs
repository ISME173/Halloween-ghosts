using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class EnemyAnyEffect : MonoBehaviour
{
    [SerializeField] private float _timeBeforeStopEffect = 1;

    private ParticleSystem _anyEffect;

    private void Awake() => _anyEffect = GetComponent<ParticleSystem>();
    private void Start()
    {
        _anyEffect.Play();
        Destroy(gameObject, _timeBeforeStopEffect);
    }
}
