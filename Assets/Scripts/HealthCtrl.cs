using UnityEngine;

public class HealthCtrl : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;

    public Animator animator;
    public CharacterController charCtrl;

    bool isDead = false;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHP -= damage;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        currentHP = 0;

        if (animator != null)
            animator.SetTrigger("Die");

        if (charCtrl != null)
            charCtrl.enabled = false;

    }
}
