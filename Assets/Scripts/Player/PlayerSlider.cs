using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSlider : MonoBehaviour
{
    #region -Declared Variables-

    [Header("Health")]
    [SerializeField] private int health;
    [SerializeField] private int currentHealth;
    [SerializeField] private Slider hpSlider;

    [Header("Energy")]
    [SerializeField] private int energy;
    [SerializeField] private int currentEnergy;
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
        StartCoroutine(LerpSlider(hpSlider, currentHealth)); // hp slider lerp
        StartCoroutine(LerpSlider(energySlider, currentEnergy)); // energy slider lerp
        
        StartCoroutine(Recovery(energySlider, currentEnergy, 10)); // energy recovery
    }

    #endregion

    IEnumerator LerpSlider(Slider slider, int currentValue)
    {
        slider.value = Mathf.Lerp(slider.value, currentValue, Time.deltaTime);
        yield return new WaitForSeconds(0.01f);
    }
    
    public void DecreaseHp(int value)
    {
        if (hpSlider.value <= 0 && currentHealth <= 0) return;
        hpSlider.value = currentHealth;
        currentHealth -= value;
        
        Debug.Log("Player hp : " + currentHealth);
    }
    
    public void DecreaseEnergy(int value)
    {
        if (energySlider.value <= 0 && currentEnergy <= 0) return;
        energySlider.value = currentEnergy;
        currentEnergy -= value;
        
        Debug.Log("Player energy : " + currentEnergy);
    }

    IEnumerator Recovery(Slider slider, int currentValue, int recoveryValue)
    {
        yield return new WaitForSeconds(5f);
        slider.value = currentValue;
        currentValue += recoveryValue;
        slider.value = Mathf.Lerp(slider.value, currentValue, Time.deltaTime);
        
    }
}
