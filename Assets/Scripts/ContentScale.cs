using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ContentScale : MonoBehaviour
{
    [SerializeField] private Transform _content;

    [SerializeField] private InputField _xInput;

    [SerializeField] private InputField _yInput;

    [SerializeField] private Button _resetButton;

    [SerializeField] private Button _closeButton;

    public Action IntentClose;

    private void Start()
    {
        _xInput.text = PlayerPrefs.GetFloat("ContentScale_X", 1).ToString(CultureInfo.InvariantCulture);

        _xInput.onEndEdit.AddListener(value =>
        {
            if (!float.TryParse(value, out var floatValue))
            {
                floatValue = 0;
            }

            PlayerPrefs.SetFloat("ContentScale_X", floatValue);

            SetContentScaleX(floatValue);

            _xInput.text = floatValue.ToString(CultureInfo.InvariantCulture);
        });

        _yInput.text = PlayerPrefs.GetFloat("ContentScale_Y", 1).ToString(CultureInfo.InvariantCulture);

        _yInput.onEndEdit.AddListener(value =>
        {
            if (!float.TryParse(value, out var floatValue))
            {
                floatValue = 0;
            }

            PlayerPrefs.SetFloat("ContentScale_Y", floatValue);

            SetContentScaleY(floatValue);

            _yInput.text = floatValue.ToString(CultureInfo.InvariantCulture);
        });

        _closeButton.onClick.AddListener(() => IntentClose());

        _resetButton.onClick.AddListener(() =>
        {
            PlayerPrefs.SetFloat("ContentScale_X", 1);

            SetContentScaleX(1);

            _xInput.text = "1";

            PlayerPrefs.SetFloat("ContentScale_Y", 1);

            SetContentScaleY(1);

            _yInput.text = "1";
        });
    }

    private void SetContentScaleX(float x)
    {
        var oldScale = _content.localScale;

        _content.localScale = new Vector3(x, oldScale.y, oldScale.z);
    }

    private void SetContentScaleY(float y)
    {
        var oldScale = _content.localScale;

        _content.localScale = new Vector3(oldScale.x, y, oldScale.z);
    }
}