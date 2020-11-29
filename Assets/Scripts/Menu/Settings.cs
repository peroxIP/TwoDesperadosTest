using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public InputField Width;
    public InputField Height;

    private void Start()
    {
        SettingValues.Width = int.Parse(Width.text);
        SettingValues.Height = int.Parse(Height.text);
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
    
    private int ValidateValue(string value)
    {
        int temp = Mathf.Abs(int.Parse(value));
        if (temp < 5)
        {
            return 5;
        }
        return temp;
    }
}
