using UnityEngine;

public class Health : MonoBehaviour
{
    protected int maxHP = 100;
    protected int curHP;
    
    protected virtual void Start() {
        ResetHealth();
    }
    
    void Update() {
        if (curHP < 0) {
            Die();
        }
    }

    void Heal(int hp) {
        curHP += hp;

        if (curHP > maxHP) {
            curHP = maxHP;
        }
    }

    public virtual void ResetHealth() {
        curHP = maxHP;
    }

    public virtual void TakeDamage(int damage) {
        curHP -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. HP: {curHP}");
        if (curHP <= 0) {
            Die();
        }
    }

    public virtual void Die() {
        Destroy(gameObject);
    }
    
    public void AddMaxHP() {
        maxHP += 10;
    }
}