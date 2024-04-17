using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    void OnGUI()
    {
        if (GUILayout.Button("Back to Menu"))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
