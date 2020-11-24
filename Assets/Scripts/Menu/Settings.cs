using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public InputField Width;
    public InputField Height;
    public InputField ObsticleCount;

    private void Start()
    {
        SettingValues.Width = int.Parse(Width.text);
        SettingValues.Height = int.Parse(Height.text);
        SettingValues.ObsticleCount = int.Parse(ObsticleCount.text);
    }


    public void ToggleSound(bool soundToggle)
    {
        SettingValues.SoundOn = soundToggle;
    }

    public void ToggleMusic(bool musicToggle)
    {
        SettingValues.MusicOn = musicToggle;
    }

    public void WidthEdited(string value)
    {
        int width = ValidateValue(value);
        SettingValues.Width = width;
        Width.text = width.ToString();
    }

    public void HeightEdited(string value)
    {
        int height = ValidateValue(value);
        SettingValues.Height = height;
        Height.text = height.ToString();
    }

    public void ObsticleCountEdited(string value)
    {
        int obsticleCount = ValidateValue(value);
        SettingValues.ObsticleCount = ValidateValue(value);
        ObsticleCount.text = obsticleCount.ToString();
    }

    private int ValidateValue(string value)
    {
        return Mathf.Abs(int.Parse(value));
    }
}
