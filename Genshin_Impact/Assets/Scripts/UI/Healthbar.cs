using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Healthbar : MonoBehaviour
{
    public Slider sliderUI;
    [SerializeField] private EnemyHealth enemyHealth;
    // Start is called before the first frame update
    void Start()
    {
        SetMaxHealth(enemyHealth.MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthbar();
    }

    public void SetMaxHealth(float maxHealth)
    {
        sliderUI.maxValue = maxHealth;
        sliderUI.value = maxHealth;
    }

    public void UpdateHealthbar()
    {
        sliderUI.value = enemyHealth.CurrentHealth;
    }

}
