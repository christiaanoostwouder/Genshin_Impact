using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthbar : MonoBehaviour
{
    public Slider sliderUI;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        SetMaxHealth(playerHealth.MaxHealth);
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
        sliderUI.value = playerHealth.CurrentHealth;
        text.text = playerHealth.CurrentHealth.ToString() + "/" + playerHealth.MaxHealth.ToString();
    }
}
