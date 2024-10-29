using Player;
using UnityEngine;
using Zenject;

namespace ShootSystem
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private Transform shootPoint;
        
        private InputListener _input;
        private BulletPool _bulletPool;
        
        [Inject]
        private void Construct(InputListener input, BulletPool bulletPool)
        {
            _input = input;
            _bulletPool = bulletPool;
        }

        private void Start()
        {
            Bind();
        }

        private void OnDestroy() => Expose();

        private void Shoot()
        {
            var bullet = _bulletPool.GetFromPool();
            bullet.transform.position = shootPoint.position;
            bullet.gameObject.SetActive(true);
        }

        private void Bind()
        {
            _input.OnShootInput += Shoot;
        }

        private void Expose()
        {
            _input.OnShootInput -= Shoot;
        }
    }
}