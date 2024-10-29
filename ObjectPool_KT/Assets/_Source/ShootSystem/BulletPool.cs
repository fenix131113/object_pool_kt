using System.Collections.Generic;
using ShootSystem.Data;
using UnityEngine;
using Zenject;

namespace ShootSystem
{
    public class BulletPool
    {
        private readonly Queue<Bullet> _bullets = new();
        private readonly List<Bullet> _unavailableBullets = new();
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
                PutToPool(_bulletFactory.Create());
        }

        public Bullet GetFromPool()
        {
            if (_bullets.Count == 0)
                PutToPool(_bulletFactory.Create());
            
            var bullet = _bullets.Dequeue();
            _unavailableBullets.Add(bullet);

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

            if(_unavailableBullets.Contains(bullet))
                _unavailableBullets.Remove(bullet);
            
            bullet.gameObject.SetActive(false);
            _bullets.Enqueue(bullet);
        }
    }
}