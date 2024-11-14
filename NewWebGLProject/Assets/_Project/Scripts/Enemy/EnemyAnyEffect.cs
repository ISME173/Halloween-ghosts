using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class EnemyAnyEffect : MonoBehaviour
{
    [SerializeField] private float _timeBeforeStopEffect = 1;

    private ParticleSystem _bloodEffect;

    private void Awake() => _bloodEffect = GetComponent<ParticleSystem>();
    private void Start()
    {
        _bloodEffect.Play();
        Destroy(gameObject, _timeBeforeStopEffect);
    }
}
