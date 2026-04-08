using TMPro;
using UnityEngine;

public class ResolutionSetting : MonoBehaviour
{
    public TextMeshProUGUI resolutionText;

    string[] resolutions = {
        "1280 x 720",
        "1600 x 900",
        "1920 x 1080",
        "2560 x 1440"
    };

    int index = 2;

    void Start()
    {
        UpdateText();
    }

    public void Next()
    {
        index++;
        if (index >= resolutions.Length) index = 0;
        UpdateText();
    }

    public void Prev()
    {
        index--;
        if (index < 0) index = resolutions.Length - 1;
        UpdateText();
    }

    void UpdateText()
    {
        resolutionText.text = resolutions[index];
    }
}