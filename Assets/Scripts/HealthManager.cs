using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private UnityEvent onDeath;
    [SerializeField] private UnityEvent<float> onHealthChanged;
    public ParticleSystem deathEffect;

    private int _currentHealth;
    private Quaternion damageDirection;

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
                ParticleSystem dEffect = Instantiate(deathEffect);
                dEffect.transform.rotation = damageDirection;
                dEffect.transform.position = this.transform.position;
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

    public void ApplyImpactDamage(int damage, Quaternion d)
    {
        this.damageDirection = d;
        CurrentHealth -= damage;
    }

    public void ApplyDamage(int damage)
    {
        CurrentHealth -= damage;
    }
}