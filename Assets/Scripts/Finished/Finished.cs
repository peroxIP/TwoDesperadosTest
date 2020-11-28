using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class Finished : MonoBehaviour
{
    public Text text;

    public void SetText(string t)
    {
        text.text = t;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
