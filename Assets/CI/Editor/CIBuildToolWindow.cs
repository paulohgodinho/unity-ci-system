using UnityEditor;
using UnityEngine;

public class CIBuildToolWindow : EditorWindow
{
    [MenuItem("CI/Build Tool")]
    static void Init()
    {
        CIBuildToolWindow buildToolWindow =
            (CIBuildToolWindow)GetWindow(typeof(CIBuildToolWindow), false, "Build Tool");
        buildToolWindow.Show();
    }

    private string[] targetPlatforms = new[]
    {
        "Windows x64",
        "Mac Universal",
        "Linux"
    };

    private int selectedPlatformIndex = 0;
    [SerializeField] private BuildTemplate selectedBuildTemplate;

    void OnGUI()
    {
        GUI.enabled = true;
        
        EditorGUILayout.Space(5);
        selectedPlatformIndex = GUILayout.Toolbar(selectedPlatformIndex, targetPlatforms);
        EditorGUILayout.Space(10);
        GUILayout.Label("Building for: " + targetPlatforms[selectedPlatformIndex], EditorStyles.boldLabel);
        GUILayout.Label("Selected Template:", EditorStyles.boldLabel);
        selectedBuildTemplate = EditorGUILayout.ObjectField(selectedBuildTemplate, typeof(BuildTemplate), false) as BuildTemplate;

        if (selectedBuildTemplate == null)
        {
            GUI.enabled = false;
        }
        
        GUILayout.Space(10);
        if (GUILayout.Button("Build Now"))
        {
            BuildTarget buildTarget = BuildTarget.StandaloneWindows64;
            switch (selectedPlatformIndex)
            {
                case 0:
                    buildTarget = BuildTarget.StandaloneWindows64;
                    break;
                case 1:
                    buildTarget = BuildTarget.StandaloneOSX;
                    break;
                case 2:
                    buildTarget = BuildTarget.StandaloneLinux64;
                    break;
            }

            if (selectedBuildTemplate == null)
            {
                Debug.LogWarning("No Template Selected");
                return;
            }
            
            CIBuildTool.DoBuild(selectedBuildTemplate, buildTarget);
        }
    }
}