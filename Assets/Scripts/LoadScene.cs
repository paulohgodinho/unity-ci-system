using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public TextMeshProUGUI buttomText;

    public void OnClick()
    {
        SceneManager.LoadScene(buttomText.text);
    }
}
