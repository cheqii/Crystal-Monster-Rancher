using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region -Declared Variables-

    [Header("Health")]
    [SerializeField] private int health;
    [SerializeField] private int currentHealth;

    [Header("Energy")]
    [SerializeField] private int energy;
    [SerializeField] private int currentEnergy;
    
    #endregion

    #region -Unity Event Functions-
    
    void Start()
    {
        currentHealth = health;
        currentEnergy = energy;
    }
    void Update()
    {
        
    }

    #endregion

    void LerpHealth()
    {
        // Mathf.Lerp(currentHealth, health, Time.deltaTime);
    }
    
    public void TakeDamage(int value)
    {
        
    }
}
