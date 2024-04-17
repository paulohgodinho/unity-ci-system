using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject ButtonTemplate;
    public TextAsset SceneList;
    
    void Start()
    {
        if (Application.isEditor)
        {
            PopulateFromBuildSettings();
            return;
        }
        
        PopulateFromFileInResources();
    }

    private void PopulateFromFileInResources()
    {
        string[] scenePaths = SceneList.text.Split("\n");
        for (int i = 0; i < scenePaths.Length; i++)
        {
            if (string.IsNullOrEmpty(scenePaths[i]) || scenePaths[i].Contains("Menu"))
            {
                continue;
            }
            CreateButton(scenePaths[i].Trim());
        }
    }

    private void PopulateFromBuildSettings()
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            CreateButton(SceneUtility.GetScenePathByBuildIndex(i));
        }
    }

    private void CreateButton(string text)
    {
        GameObject button = Instantiate(ButtonTemplate, ButtonTemplate.transform.parent);
        button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
        button.SetActive(true);
    }
}
