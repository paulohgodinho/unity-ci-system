using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

public class CIBuildTool
{
    private static string[] _defineCache = {};
    
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
        EditorUtility.RevealInFinder(buildPlayerOptions.locationPathName);
    }

    private static string GetBuildTargetPath(BuildTarget buildTarget, string templateName)
    {
        var timeStamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH-mm-ss", CultureInfo.InvariantCulture);
        var projectPath = new DirectoryInfo(Application.dataPath).Parent;
        var noSpaceProductName = Application.productName.Replace(" ", string.Empty) + ".exe";
        var targetFolder = Path.Join(projectPath.FullName, "buildOutput/" + timeStamp + "_" + templateName + "/");

        string toAppend = "";
        switch (buildTarget)
        {
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                toAppend = ".exe";
                break;
            case BuildTarget.StandaloneOSX:
                toAppend = ".app";
                break;
        }
        
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
