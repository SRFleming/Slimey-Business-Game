using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private UnityEvent onDeath;
    [SerializeField] private UnityEvent<float> onHealthChanged;
    private GameObject PlayerDeath;
    private GameObject EnemyKilled;
    public ParticleSystem Win;
    public ParticleSystem deathEffect;
    public HealthBar healthBar;

    private Quaternion damageDirection;
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
                if(gameObject.tag == "Player"){
                PlayerDeath = GameObject.Find("Sounds");
                PlayerDeath.GetComponent<PlaySounds>().PlayDeath();
                }
                if(gameObject.tag == "Enemy"){
                EnemyKilled = GameObject.Find("Sounds");
                EnemyKilled.GetComponent<PlaySounds>().PlayEnemyKilled();
                }
                if(gameObject.tag == "Boss"){
                EnemyKilled = GameObject.Find("Sounds");
                EnemyKilled.GetComponent<PlaySounds>().PlayWin();
                }
                ParticleSystem dEffect = Instantiate(deathEffect);
                dEffect.transform.rotation = damageDirection;
                dEffect.transform.position = this.transform.position;
                Destroy(gameObject);
            }
            if(gameObject.tag == "Player"){
                healthBar.SetHealth(_currentHealth);
            }

            if(gameObject.tag == "Boss"){
                healthBar.SetHealth(_currentHealth);
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