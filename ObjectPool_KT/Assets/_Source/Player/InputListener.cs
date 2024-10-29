using System;
using UnityEngine;
using Zenject;

namespace Player
{
    public class InputListener : ITickable
    {
        public event Action OnShootInput;
        
        public void Tick()
        {
            ShootInputHandler();
        }

        private void ShootInputHandler()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                OnShootInput?.Invoke();
        }
    }
}