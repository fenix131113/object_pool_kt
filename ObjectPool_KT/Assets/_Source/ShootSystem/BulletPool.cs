using System.Collections.Generic;
using ShootSystem.Data;
using UnityEngine;
using Zenject;

namespace ShootSystem
{
    public class BulletPool
    {
        private readonly Queue<Bullet> _bullets = new();
        private readonly BulletPoolSettings _settings;
        private readonly BulletFactory _bulletFactory;
        
        [Inject]
        public BulletPool(BulletPoolSettings settings, BulletFactory bulletFactory)
        {
            _settings = settings;
            _bulletFactory = bulletFactory;

            Init();
        }

        private void Init()
        {
            for (var i = 0; i < _settings.StartPoolCount; i++)
                PutToPool(_bulletFactory.CreateBullet());
        }

        public Bullet GetFromPool()
        {
            if (_bullets.Count == 0)
                PutToPool(_bulletFactory.CreateBullet());
            
            var bullet = _bullets.Dequeue();

            bullet.OnBulletLifeEnd += PutToPool;
            
            return bullet;
        }

        public void PutToPool(Bullet bullet)
        {
            if (_bullets.Contains(bullet))
            {
                #if UNITY_EDITOR
                Debug.LogWarning("This bullet already exists in the pool!");
                #endif
                return;
            }

            bullet.gameObject.SetActive(false);
            _bullets.Enqueue(bullet);
        }
    }
}