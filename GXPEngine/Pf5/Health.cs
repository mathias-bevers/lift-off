using System;
using GXPEngine;

namespace Mathias
{
	public class Health
	{
		public event Action DiedEvent;
		public event Action<int> HealthChangedEvent;
		public int CurrentHealth { get; private set; }
		public float HealthNormalized => (float)CurrentHealth/maxHealth;

		private readonly int maxHealth;

		public Health(int maxHealth)
		{
			this.maxHealth = maxHealth;
			CurrentHealth = maxHealth;
		}

		public void TakeDamage(int amount = 1)
		{
			CurrentHealth = Mathf.Clamp(CurrentHealth - amount, 0, maxHealth);
			HealthChangedEvent?.Invoke(CurrentHealth);

			if (CurrentHealth > 0) { return; }

			DiedEvent?.Invoke();
		}

		public void Heal(int amount = 1)
		{
			CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, maxHealth);
			HealthChangedEvent?.Invoke(CurrentHealth);
		}
	}
}