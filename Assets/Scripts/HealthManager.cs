using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private UnityEvent onDeath;
    [SerializeField] private UnityEvent<float> onHealthChanged;

    private int _currentHealth;

    private int CurrentHealth
    {
        get => this._currentHealth;
        set
        {
            this._currentHealth = value;
            var frac = this._currentHealth / (float)this.startingHealth;
            this.onHealthChanged.Invoke(frac);
            if (CurrentHealth <= 0)
            {
                this.onDeath.Invoke();
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        ResetHealthToStarting();
    }

    public void ResetHealthToStarting()
    {
        CurrentHealth = this.startingHealth;
    }

    public void ApplyDamage(int damage)
    {
        CurrentHealth -= damage;
    }
}