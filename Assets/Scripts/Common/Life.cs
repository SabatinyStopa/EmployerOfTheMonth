using UnityEngine;
using System;

namespace EmployerOfTheMonth.Common
{
    public class Life : MonoBehaviour
    {
        public Action OnDie;
        [SerializeField] private int totalLife = 100;
        private int currentLife;

        private void Start() => currentLife = totalLife;

        public void TakeDamage(int amount)
        {
            currentLife -= amount;

            if (currentLife <= 0) Die();
        }

        public void Heal(int amount)
        {
            currentLife += amount;

            if (currentLife > totalLife) currentLife = totalLife;
        }

        private void Die() => OnDie?.Invoke();
    }
}
