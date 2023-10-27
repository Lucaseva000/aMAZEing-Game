using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextSlider : MonoBehaviour
{
    public TextMeshProUGUI numberText;//Takes the number text text mesh variable to change in the SetNumberText function
    private Slider slider; 

    void Start()//Makes it so that the volume slider always starts at the same value as the slider
    {
        slider = GetComponent<Slider>();
        SetNumberText(slider.value);
    }

    public void SetNumberText(float value)
    {
        numberText.text = value.ToString();
    }
}
