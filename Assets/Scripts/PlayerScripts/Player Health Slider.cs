using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class PlayerHealthSlider : MonoBehaviour
{
    public TextMeshProUGUI numberText;//Takes the number text text mesh variable to change in the SetNumberText function
    private Slider slider;
    public PlayerHealth p;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        slider.maxValue = p.getMaxHealth();
        slider.value = p.getHealth();
        SetNumberText();
    }

    // Update is called once per frame
    public void SetNumberText()
    {
        numberText.text = p.getHealth().ToString();
    }
}
