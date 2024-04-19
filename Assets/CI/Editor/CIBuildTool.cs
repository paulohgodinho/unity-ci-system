using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

public class CIBuildTool
{
    private static string[] _defineCache = {};

    private static Dictionary<string, BuildTarget> PlatformDict = new Dictionary<string, BuildTarget>()
    {
        {"Windows", BuildTarget.StandaloneWindows64},
        {"Mac", BuildTarget.StandaloneOSX},
        {"Linux", BuildTarget.StandaloneLinux64}
    };
    
    // External Build
    [UsedImplicitly]
    public static void DoBuild()
    {
        string selectedPlatform = "";
        string selectedTemplate = "";
        
        var args = System.Environment.GetCommandLineArgs();
        foreach (var arg in args)
        {
            Debug.Log("- " + arg);
            if (arg.StartsWith("platform:"))
            {
                selectedPlatform = arg.Split(':')[1];
                continue;
            }

            if (arg.StartsWith("template:"))
            {
                selectedTemplate = arg.Split(':')[1];
                continue;
            }
        }
        
        Debug.Log("Selected Platform: " + selectedPlatform);
        Debug.Log("Selected Template: " + selectedTemplate);

        if (PlatformDict.ContainsKey(selectedPlatform) == false)
        {
            Debug.LogError("Platform not found");
            return;
        }

        BuildTarget buildTarget = PlatformDict[selectedPlatform];

        string templateToLoad = "Assets/CI/Templates/" + selectedTemplate;
        Debug.Log(templateToLoad);
        var template = AssetDatabase.LoadAssetAtPath<BuildTemplate>(templateToLoad);
        if (template == null)
        {
            Debug.LogError("Issue loading template");
            return;
        }
        
        DoBuild(template, buildTarget);
    }
    
    public static void DoBuild(BuildTemplate buildTemplate, BuildTarget buildTarget)
    {
        _defineCache = new string[]{};
        
        bool scriptDebugging = buildTemplate.scriptDebugging;
        bool developmentBuild = buildTemplate.developmentBuild;
        var sceneList = buildTemplate.sceneList;
        var defines = buildTemplate.defines;
        
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.target = buildTarget;
        buildPlayerOptions.options = developmentBuild ? BuildOptions.Development : BuildOptions.None;

        if (scriptDebugging == true)
        {
            buildPlayerOptions.options = BuildOptions.Development | BuildOptions.AllowDebugging;
        }

        List<string> scenesFullPath = new List<string>();
        foreach (var sceneAsset in sceneList)
        {
            if (sceneAsset == null)
            {
                Debug.LogWarning("NULL SceneAsset found in BuildTemplate");
                continue;
            }
            
            string scenePath = AssetDatabase.GetAssetPath(sceneAsset);
            scenesFullPath.Add(scenePath);
        }
        buildPlayerOptions.scenes = scenesFullPath.ToArray();
        WriteScenesToTextFileInResources(scenesFullPath);
        
        CacheAndEraseAllEditorDefines(buildTarget, ref _defineCache);
        WriteDefinesToTextFileInResources(defines);
        buildPlayerOptions.extraScriptingDefines = defines;
        
        RestoreEditorDefinesFromCache(buildTarget, ref _defineCache);
        Debug.Log("Finished");

        
        buildPlayerOptions.locationPathName = GetBuildTargetPath(buildTarget, buildTemplate.name);
        Debug.Log("Building to " + buildPlayerOptions.locationPathName);
        var buildReport = BuildPipeline.BuildPlayer(buildPlayerOptions);
        if (Application.isBatchMode == false)
        {
            EditorUtility.RevealInFinder(buildPlayerOptions.locationPathName);
        }
    }

    private static string GetBuildTargetPath(BuildTarget buildTarget, string templateName)
    {
        var timeStamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH-mm-ss", CultureInfo.InvariantCulture);
        var projectPath = new DirectoryInfo(Application.dataPath).Parent;
        var noSpaceProductName = Application.productName.Replace(" ", string.Empty) + ".exe";
        var targetFolder = Path.Join(projectPath.FullName, "buildOutput/" + timeStamp + "_" + templateName + "_" + buildTarget + "/");

        string toAppend = "";
        
        return Path.Join(targetFolder, noSpaceProductName) + toAppend;
    }

    private static void WriteScenesToTextFileInResources(List<string> sceneList)
    {
        if (sceneList == null)
        {
            return;
        }
        
        var path = "Assets/Resources/scenes.txt";
        File.WriteAllText(path, string.Empty);
        StreamWriter writer = new StreamWriter(path, true);
        foreach (var scene in sceneList)
        {
            writer.WriteLine(scene);
        }
        
        writer.Close();
        AssetDatabase.ImportAsset(path);
    }
    
    private static void WriteDefinesToTextFileInResources(string[] defines)
    {
        if (defines == null)
        {
            return;
        }
        
        var path = "Assets/Resources/defines.txt";
        File.WriteAllText(path, string.Empty);
        StreamWriter writer = new StreamWriter(path, true);
        foreach (var define in defines)
        {
            writer.WriteLine(define);
        }
        
        writer.Close();
        AssetDatabase.ImportAsset(path);
    }
    
    private static void CacheAndEraseAllEditorDefines(BuildTarget buildTarget, ref string[] cache)
    {
        if (cache == null)
        {
            return;
        }
        
        var currentBuildGroup = BuildPipeline.GetBuildTargetGroup(buildTarget);
        var namedBuildTargetGroup = NamedBuildTarget.FromBuildTargetGroup(currentBuildGroup);
        PlayerSettings.GetScriptingDefineSymbols(namedBuildTargetGroup, out cache);
        PlayerSettings.SetScriptingDefineSymbols(namedBuildTargetGroup, new string[]{});
    }
    
    private static void RestoreEditorDefinesFromCache(BuildTarget buildTarget, ref string[] cache)
    {
        var currentBuildGroup = BuildPipeline.GetBuildTargetGroup(buildTarget);
        var namedBuildTargetGroup = NamedBuildTarget.FromBuildTargetGroup(currentBuildGroup);
        PlayerSettings.SetScriptingDefineSymbols(namedBuildTargetGroup, cache);
        cache = new string[]{};
    }
}
