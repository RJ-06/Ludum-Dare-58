using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int healthPoints;
    [SerializeField] int maxHealthPoints;

    public int GetHealth() 
    {
        return healthPoints;
    }

    public int GetMaxHealth() 
    {
        return maxHealthPoints;
    }

    public void SetHealth(int health) 
    {
        healthPoints = health;
    }

    public void TakeDamage(int damage) 
    {
        healthPoints -= damage;
    }

    public void GainHealth(int healing) 
    {
        healthPoints += healing;
        if (healthPoints > maxHealthPoints) 
        {
            healthPoints = maxHealthPoints;
        }
    }

    public void Die() 
    {
        //IMPLEMENT - EITHER CALL AN EVENT OR MAKE THIS INHERITANCE BASED/DERIVED CLASSES CALL THIS FUNC WITH THEIR OWN IMPLEMENTATION
    }
}
