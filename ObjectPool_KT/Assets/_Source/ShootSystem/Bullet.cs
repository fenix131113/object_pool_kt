using System;
using System.Collections;
using Core.ObjectPool;
using UnityEngine;

namespace ShootSystem
{
    public class Bullet : MonoBehaviour, IObjectPoolItem<Bullet>
    {
        [SerializeField] private float speed;
        [SerializeField] private float lifetime;

        public event Action<Bullet> OnObjectLifeEndRequest;

        public void OnEnable() => StartCoroutine(DisableCoroutine());

        private void Update() => transform.position += transform.up * (Time.deltaTime * speed);

        private void OnDestroy() => OnObjectLifeEndRequest = null;

        private IEnumerator DisableCoroutine()
        {
            yield return new WaitForSeconds(lifetime);

            OnObjectLifeEndRequest?.Invoke(this);
            OnObjectLifeEndRequest = null;
            gameObject.SetActive(false);
        }
    }
}