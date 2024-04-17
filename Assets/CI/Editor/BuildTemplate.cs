
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Data", menuName = "CI/BuildTemplate", order = 1)]
public class BuildTemplate : ScriptableObject
{

    
    [FormerlySerializedAs("_sceneList")]
    [Header("Scenes to include in Build")]
    [SerializeField] public List<SceneAsset> sceneList;
    
    [FormerlySerializedAs("_developmentBuild")]
    [Header("Options")]
    [SerializeField] public bool developmentBuild = false;
    [FormerlySerializedAs("_scriptDebugging")] [SerializeField] public bool scriptDebugging = false;

    [FormerlySerializedAs("_defines")] [SerializeField] public string[] defines; 
    

}
