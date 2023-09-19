using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _maxBulletTravelTime;

    private Vector3 _shootDirection;

    private void OnEnable()
    {
        Invoke(nameof(DeactivateBullet), _maxBulletTravelTime);
    }

    private void Update()
    {
        transform.position += _shootDirection * Time.deltaTime * _speed;
    }

    public void SetShootDirection(Vector3 shootDirection)
    {
        _shootDirection = shootDirection;
    }

    private void DeactivateBullet()
    {
        gameObject.SetActive(false);
    }
}
