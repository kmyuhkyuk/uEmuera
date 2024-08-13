using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using MinorShift.Emuera;

public class Scalepad : MonoBehaviour
{
    [SerializeField] private InputField scaleInput;
    
    [SerializeField] private Button saveButton;
    
    [SerializeField] private Button reloadButton;

    void Start ()
    {
        GenericUtils.SetListenerOnClick(oneperone_btn, OnOnePerOneClick);
        GenericUtils.SetListenerOnClick(autofit_btn, OnAutoFit);

        var scaleValue = emuera_main.scale_value;
        
        text.text = $"{scaleValue:F1}x";
        slider.value = GetSliderValue(scaleValue);
        
        slider.onValueChanged.AddListener(OnValueChanged);
        
        scaleInput.text = scaleValue.ToString(CultureInfo.InvariantCulture);
        
        scaleInput.onEndEdit.AddListener(value =>
        {
            if (!float.TryParse(value, out var floatValue))
            {
                floatValue = 0;
            }

            if (floatValue > 3)
            {
                floatValue = 3;
            }
            else if (floatValue < 0.5)
            {
                floatValue = 0.5f;
            }
            
            emuera_main.SetScaleValue(floatValue);
        
            slider.value = GetSliderValue(floatValue);

            scaleInput.text = floatValue.ToString(CultureInfo.InvariantCulture);
        });
        
        saveButton.onClick.AddListener(() =>
        {
            PlayerPrefs.SetFloat("Scale", emuera_main.scale_value);
        });
        
        reloadButton.onClick.AddListener(() =>
        {
            var scale = PlayerPrefs.GetFloat("Scale", 1);
            
            emuera_main.SetScaleValue(scale);

            slider.value = GetSliderValue(scale);
            
            scaleInput.text = scale.ToString(CultureInfo.InvariantCulture);
        });
    }

    private static float GetSliderValue(float scaleValue)
    {
        if (scaleValue >= 1)
            return scaleValue - 1;
        
        return (scaleValue - 1) * 2;
    }
    
    public bool IsShow { get { return gameObject.activeSelf; } }

    void OnOnePerOneClick()
    {
        emuera_main.OnePerOne();
        UpdateSlider();
    }
    void OnAutoFit()
    {
        emuera_main.AutoFit();
        UpdateSlider();
    }
    void OnValueChanged(float value)
    {
        if(value >= 0)
            value = (1 + value);
        else
            value = (1 + value / 2);
        emuera_main.SetScaleValue(value);
        text.text = string.Format("{0:F1}x", value);
        
        scaleInput.text = value.ToString(CultureInfo.InvariantCulture);
    }
    public void SetColor(Color sprite_color)
    {
        var images = GenericUtils.FindChildren<Image>(gameObject, true);
        var count = images.Count;
        Image image = null;
        for(int i=0; i<count; ++i)
        {
            image = images[i];
            if(image.sprite == null)
                continue;
            image.color = sprite_color;
        }
        var texts = GenericUtils.FindChildren<Text>(gameObject, true);
        count = texts.Count;
        Text text = null;
        for(int i=0; i<count; ++i)
        {
            text = texts[i];
            text.color = sprite_color;
        }
    }
    public void Show()
    {
        gameObject.SetActive(true);
        UpdateSlider();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    void UpdateSlider()
    {
        var value = emuera_main.scale_value;
        text.text = string.Format("{0:F1}x", value);
        if(value > 1)
            value = value - 1;
        else
            value = (value - 1) * 2;
        slider.value = value;
    }

    public EmueraMain emuera_main;
    public GameObject oneperone_btn;
    public GameObject autofit_btn;
    public Slider slider;
    public Text text;
}
