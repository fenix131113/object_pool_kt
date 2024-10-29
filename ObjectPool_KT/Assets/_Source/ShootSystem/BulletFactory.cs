using ShootSystem.Data;
using UnityEngine;
using Zenject;

namespace ShootSystem
{
    public class BulletFactory
    {
        private readonly BulletPoolSettings _settings;
        
        [Inject]
        public BulletFactory(BulletPoolSettings settings) => _settings = settings;

        public Bullet CreateBullet()
        {
            return Object.Instantiate(_settings.Prefab); 
        }
    }
}