using UnityEngine;
using UnityEngine.UI;

public class CustomTargetFrameRate : MonoBehaviour
{
    [SerializeField] private InputField targetFrameRateInput;

    [SerializeField] private Button resetButton;

    [SerializeField] private Button closeButton;

    private void Start()
    {
        targetFrameRateInput.text = PlayerPrefs.GetInt("TargetFrameRate", 24).ToString();

        targetFrameRateInput.onEndEdit.AddListener(value =>
        {
            if (!int.TryParse(value, out var intValue))
            {
                intValue = 24;
            }

            if (intValue == 0)
            {
                intValue = -1;
            }

            Application.targetFrameRate = intValue;

            PlayerPrefs.SetInt("TargetFrameRate", intValue);

            targetFrameRateInput.text = intValue.ToString();
        });

        closeButton.onClick.AddListener(() => gameObject.SetActive(false));

        resetButton.onClick.AddListener(() =>
        {
            Application.targetFrameRate = 24;

            PlayerPrefs.SetInt("TargetFrameRate", 24);

            targetFrameRateInput.text = "24";
        });
    }
}