using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSlider : Singleton<PlayerSlider>
{
    #region -Declared Variables-

    private bool isTakeDamage;
    private bool isRecovery;
    public bool isUseEnergy;
    public bool isRecoveredE;
    
    [Header("Health")]
    [SerializeField] private float health;
    [SerializeField] private float currentHealth;
    public float CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = value;
    }
    
    [SerializeField] private Slider hpSlider;

    [Header("Energy")]
    [SerializeField] private float energy;
    [SerializeField] private float currentEnergy;
    public float CurrentEnergy
    {
        get => currentEnergy;
        set => currentEnergy = value;
    }
    
    [SerializeField] private Slider energySlider;
    
    
    #endregion

    
    #region -Unity Event Functions-
    
    void Start()
    {
        // set current health and energy to max
        currentHealth = health;
        currentEnergy = energy;
        
        // set health slider value 
        hpSlider.maxValue = health;
        hpSlider.value = currentHealth;
        
        // set energy slider value
        energySlider.maxValue = energy;
        energySlider.value = currentEnergy;
    }
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        StartCoroutine(LerpHealth()); // hp slider lerp
        if(isUseEnergy) StartCoroutine(LerpEnergy()); // energy slider lerp
        
        if (isRecoveredE && !isUseEnergy) StartCoroutine(LerpEnergyRecovery()); // energy recovery lerp
    }

    #endregion

    
    #region -Lerp Functions-

    IEnumerator LerpHealth()
    {
        hpSlider.value = Mathf.Lerp(hpSlider.value, currentHealth, Time.deltaTime);
        yield return new WaitForSeconds(0.01f);
    }
    
    IEnumerator LerpEnergy()
    {
        energySlider.value = Mathf.Lerp(energySlider.value, currentEnergy, Time.deltaTime);
        yield return new WaitForSeconds(0.01f);
    }
    
    IEnumerator LerpEnergyRecovery()
    {
        // make recovery value lerp to current value in every 5 seconds
        if (isUseEnergy) isRecoveredE = false;
        yield return new WaitForSeconds(5f);
        RecoveryEnergy();
        energySlider.value = Mathf.Lerp(energySlider.value, currentEnergy, Time.deltaTime);
        if (currentEnergy >= energySlider.maxValue)
        {
            currentEnergy = energy;
            isRecoveredE = false;
        }
    }

    #endregion

    public void DecreaseHp(float value)
    {
        if (hpSlider.value <= 0 || currentHealth <= 0) return;
        hpSlider.value = currentHealth;
        currentHealth -= value;
        
        isTakeDamage = true;
        Debug.Log("Player hp : " + currentHealth);
    }
    
    public void DecreaseEnergy(float value)
    {
        if (energySlider.value <= 0 || currentEnergy <= 0) return;
        energySlider.value = currentEnergy;
        currentEnergy -= value;
        
        isUseEnergy = true;
        isRecoveredE = true;
    }

    void RecoveryEnergy()
    {
        if (currentEnergy >= energy || isUseEnergy) return;
        energySlider.value = currentEnergy;
        currentEnergy += 1;
    }
}
