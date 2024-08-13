﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class CustomResolution : MonoBehaviour
{
    [SerializeField] private GameObject _customResolutionIcon;

    [SerializeField] private InputField _xResolutionInput;

    [SerializeField] private InputField _yResolutionInput;

    [SerializeField] private Button _saveResolutionButton;

    [SerializeField] private Button _closeResolutionButton;

    public Action HideResolutionIcon;

    private void Start()
    {
        _xResolutionInput.onEndEdit.AddListener(value =>
        {
            if (!int.TryParse(value, out var intValue) || intValue < 960)
            {
                intValue = 960;
            }

            var resolutionY = PlayerPrefs.GetInt("Resolution_Y", intValue);

            if (intValue < resolutionY)
            {
                intValue = resolutionY;
            }

            _xResolutionInput.text = intValue.ToString();
        });

        _yResolutionInput.onEndEdit.AddListener(value =>
        {
            if (!int.TryParse(value, out var intValue) || intValue < 540)
            {
                intValue = 540;
            }

            var resolutionX = PlayerPrefs.GetInt("Resolution_X", intValue);

            if (intValue > resolutionX)
            {
                intValue = resolutionX;
            }

            _yResolutionInput.text = intValue.ToString();
        });

        _closeResolutionButton.onClick.AddListener(() => gameObject.SetActive(false));

        _saveResolutionButton.onClick.AddListener(() =>
        {
            PlayerPrefs.SetInt("Resolution_X", int.Parse(_xResolutionInput.text));
            PlayerPrefs.SetInt("Resolution_Y", int.Parse(_yResolutionInput.text));

            ResolutionHelper.resolution_index = 100;
            ResolutionHelper.Apply();
            HideResolutionIcon();
            _customResolutionIcon.SetActive(true);

            gameObject.SetActive(false);
        });
    }

    private void OnEnable()
    {
        _xResolutionInput.text = PlayerPrefs.GetInt("Resolution_X", 960).ToString();
        _yResolutionInput.text = PlayerPrefs.GetInt("Resolution_Y", 540).ToString();
    }

    public void SetIconActive(bool active)
    {
        _customResolutionIcon.SetActive(active);
    }
}