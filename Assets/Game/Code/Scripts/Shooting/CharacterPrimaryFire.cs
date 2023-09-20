using System.Collections;
using UnityEngine;

public class CharacterPrimaryFire : MonoBehaviour
{
    [SerializeField]
    private Transform _firingPoint;
    [SerializeField]
    private float _firingRate;

    private ObjectPooler _objectPooler;
    private WaitForSeconds _waitForSeconds;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        _waitForSeconds = new WaitForSeconds(1 / _firingRate);
    }

    public IEnumerator FireCoroutine()
    {
        while (true)
        {
            Fire();
            yield return _waitForSeconds;
        }
    }

    private void Fire()
    {
        GameObject bullet = _objectPooler.GetPooledObject("Bullet", _firingPoint.position, _firingPoint.rotation);

        if (bullet != null)
        {
            bullet.GetComponent<Projectile>().SetShootDirection(_firingPoint.forward);
        }
    }
}
