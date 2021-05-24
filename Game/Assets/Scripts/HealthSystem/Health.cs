using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public int MaxHealth = 100;
    public int healthIncrement = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHealth(int health)
    {
        this.health = health < 0 ? 0 : (health > MaxHealth ? MaxHealth : health);
    }

    public void increaseHealth()
    {
        if(this.health + (healthIncrement * 2) > MaxHealth)
        {
            this.health = MaxHealth;
        }
        else
        {
            this.health += healthIncrement * 2;
        }
    }

    public void decreaseHealth(int multiplier)
    {
        if(this.health - healthIncrement * multiplier < 0)
        {
            this.health = 0;
        }
        else
        {
            this.health -= healthIncrement * multiplier;
        }
    }

    public bool isDead()
    {
        return health == 0;
    }
}
