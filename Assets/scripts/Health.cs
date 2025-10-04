using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int healthPoints;
    [SerializeField] int maxHealthPoints;

    int GetHealth() 
    {
        return healthPoints;
    }

    int GetMaxHealth() 
    {
        return maxHealthPoints;
    }

    void SetHealth(int health) 
    {
        healthPoints = health;
    }

    void TakeDamage(int damage) 
    {
        healthPoints -= damage;
    }

    void GainHealth(int healing) 
    {
        healthPoints += healing;
        if (healthPoints > maxHealthPoints) 
        {
            healthPoints = maxHealthPoints;
        }
    }

    void Die() 
    {
        //IMPLEMENT - EITHER CALL AN EVENT OR MAKE THIS INHERITANCE BASED/DERIVED CLASSES CALL THIS FUNC WITH THEIR OWN IMPLEMENTATION
    }
}
