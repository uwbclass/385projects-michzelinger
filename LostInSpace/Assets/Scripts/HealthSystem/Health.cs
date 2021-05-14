using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int health;
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

    public void decreaseHealth()
    {
        if(this.health - healthIncrement < 0)
        {
            this.health = 0;
        }
        else
        {
            this.health -= healthIncrement;
        }
    }

    public bool isDead()
    {
        return health == 0;
    }
}
