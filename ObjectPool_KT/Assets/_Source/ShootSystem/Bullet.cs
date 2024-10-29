using System;
using System.Collections;
using UnityEngine;

namespace ShootSystem
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float lifetime;

        public event Action<Bullet> OnBulletLifeEnd;

        public void OnEnable() => StartCoroutine(DisableCoroutine());

        private void Update() => transform.position += transform.up * (Time.deltaTime * speed);

        private void OnDestroy() => OnBulletLifeEnd = null;

        private IEnumerator DisableCoroutine()
        {
            yield return new WaitForSeconds(lifetime);

            OnBulletLifeEnd?.Invoke(this);
            OnBulletLifeEnd = null;
        }
    }
}