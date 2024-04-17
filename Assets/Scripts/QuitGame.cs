using UnityEngine;

public class QuitGame : MonoBehaviour
{
    void OnGUI()
    {
        if (GUILayout.Button("Quit App"))
        {
            Application.Quit();
        }
    }
}
