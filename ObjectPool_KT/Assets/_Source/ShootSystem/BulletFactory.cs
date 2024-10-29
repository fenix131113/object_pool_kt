using ShootSystem.Data;
using UnityEngine;
using Zenject;

namespace ShootSystem
{
    public class BulletFactory : PlaceholderFactory<Bullet>
    {
        private readonly BulletPoolSettings _settings;
        
        [Inject]
        public BulletFactory(BulletPoolSettings settings) => _settings = settings;

        public override Bullet Create()
        {
            return Object.Instantiate(_settings.Prefab);
        }
    }
}