using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ContentScale : MonoBehaviour
{
    [SerializeField] private Transform content;

    [SerializeField] private InputField xInput;

    [SerializeField] private InputField yInput;

    [SerializeField] private Button resetButton;

    [SerializeField] private Button closeButton;

    public Action IntentClose;

    private void Start()
    {
        xInput.text = PlayerPrefs.GetFloat("ContentScale_X", 1).ToString(CultureInfo.InvariantCulture);

        xInput.onEndEdit.AddListener(value =>
        {
            if (!float.TryParse(value, out var floatValue))
            {
                floatValue = 0;
            }

            PlayerPrefs.SetFloat("ContentScale_X", floatValue);

            SetContentScaleX(floatValue);

            xInput.text = floatValue.ToString(CultureInfo.InvariantCulture);
        });

        yInput.text = PlayerPrefs.GetFloat("ContentScale_Y", 1).ToString(CultureInfo.InvariantCulture);

        yInput.onEndEdit.AddListener(value =>
        {
            if (!float.TryParse(value, out var floatValue))
            {
                floatValue = 0;
            }

            PlayerPrefs.SetFloat("ContentScale_Y", floatValue);

            SetContentScaleY(floatValue);

            yInput.text = floatValue.ToString(CultureInfo.InvariantCulture);
        });

        closeButton.onClick.AddListener(() => IntentClose());

        resetButton.onClick.AddListener(() =>
        {
            PlayerPrefs.SetFloat("ContentScale_X", 1);

            SetContentScaleX(1);

            xInput.text = "1";

            PlayerPrefs.SetFloat("ContentScale_Y", 1);

            SetContentScaleY(1);

            yInput.text = "1";
        });
    }

    private void SetContentScaleX(float x)
    {
        var oldScale = content.localScale;

        content.localScale = new Vector3(x, oldScale.y, oldScale.z);
    }

    private void SetContentScaleY(float y)
    {
        var oldScale = content.localScale;

        content.localScale = new Vector3(oldScale.x, y, oldScale.z);
    }
}