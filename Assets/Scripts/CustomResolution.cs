using System;
using UnityEngine;
using UnityEngine.UI;

public class CustomResolution : MonoBehaviour
{
    [SerializeField] private GameObject customResolutionIcon;

    [SerializeField] private InputField xResolutionInput;

    [SerializeField] private InputField yResolutionInput;

    [SerializeField] private Button saveResolutionButton;

    [SerializeField] private Button closeResolutionButton;

    public Action HideResolutionIcon;

    private void Start()
    {
        xResolutionInput.onEndEdit.AddListener(value =>
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

            xResolutionInput.text = intValue.ToString();
        });

        yResolutionInput.onEndEdit.AddListener(value =>
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

            yResolutionInput.text = intValue.ToString();
        });

        closeResolutionButton.onClick.AddListener(() => gameObject.SetActive(false));

        saveResolutionButton.onClick.AddListener(() =>
        {
            PlayerPrefs.SetInt("Resolution_X", int.Parse(xResolutionInput.text));
            PlayerPrefs.SetInt("Resolution_Y", int.Parse(yResolutionInput.text));

            ResolutionHelper.resolution_index = 100;
            ResolutionHelper.Apply();
            HideResolutionIcon();
            customResolutionIcon.SetActive(true);

            gameObject.SetActive(false);
        });
    }

    private void OnEnable()
    {
        xResolutionInput.text = PlayerPrefs.GetInt("Resolution_X", 960).ToString();
        yResolutionInput.text = PlayerPrefs.GetInt("Resolution_Y", 540).ToString();
    }

    public void SetIconActive(bool active)
    {
        customResolutionIcon.SetActive(active);
    }
}