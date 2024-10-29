using UnityEngine;

namespace ShootSystem.Data
{
    [CreateAssetMenu(fileName = "BulletPoolSettings", menuName = "SOs/new BulletPoolSettings")]
    public class BulletPoolSettings : ScriptableObject
    {
        [field: SerializeField] public Bullet Prefab { get; private set; }
        [field: SerializeField] public int StartPoolCount { get; private set; }
    }
}