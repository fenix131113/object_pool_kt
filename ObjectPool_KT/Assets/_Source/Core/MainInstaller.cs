using Player;
using ShootSystem;
using ShootSystem.Data;
using UnityEngine;
using Zenject;

namespace Core
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private BulletPoolSettings bulletPoolSettings;
        
        public override void InstallBindings()
        {
            BindShootSystem();
            BindPlayer();
        }

        private void BindPlayer()
        {
            Container.BindInterfacesAndSelfTo<InputListener>()
                .AsSingle()
                .NonLazy();
        }

        private void BindShootSystem()
        {
            Container.Bind<BulletPool>()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<BulletPoolSettings>()
                .FromInstance(bulletPoolSettings)
                .AsSingle()
                .NonLazy();

            Container.BindFactory<Bullet, BulletFactory>()
                .AsSingle()
                .NonLazy();
        }
    }
}
