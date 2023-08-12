/* Copyright (c) 2017-present Evereal. All rights reserved. */

using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Evereal.SkyboxCapture
{
  [CustomEditor(typeof(SkyboxCapture))]
  public class SkyboxCaptureEditor : UnityEditor.Editor
  {
    SkyboxCapture skyboxCapture;
    SerializedProperty equirectMaterial;
    SerializedProperty cubemapMaterial;

    public void OnEnable()
    {
      skyboxCapture = (SkyboxCapture)target;

      equirectMaterial = serializedObject.FindProperty("equirectMaterial");
      cubemapMaterial = serializedObject.FindProperty("cubemapMaterial");
    }

    public override void OnInspectorGUI()
    {
      GUILayout.Label("Capture Options", EditorStyles.boldLabel);

      skyboxCapture.captureKey = (KeyCode)EditorGUILayout.EnumPopup("Capture Key", skyboxCapture.captureKey);
      skyboxCapture.saveFolder = EditorGUILayout.TextField("Save Folder", skyboxCapture.saveFolder);
      skyboxCapture.projectionType = (ProjectionType)EditorGUILayout.EnumPopup("Projection", skyboxCapture.projectionType);
      skyboxCapture.cubemapSize = (CubemapSizeType)EditorGUILayout.EnumPopup("Cubemap Size", skyboxCapture.cubemapSize);
      if (skyboxCapture.projectionType == ProjectionType.Cubemap)
      {
        skyboxCapture.cubemapLayout = (CubemapLayoutType)EditorGUILayout.EnumPopup("Cubemap Layout", skyboxCapture.cubemapLayout);
      }
      else
      {
        skyboxCapture.imageSize = (ImageSizeType)EditorGUILayout.EnumPopup("Image Size", skyboxCapture.imageSize);
      }
      skyboxCapture.antiAliasing = (AntiAliasingType)EditorGUILayout.EnumPopup("Anti Aliasing", skyboxCapture.antiAliasing);

      skyboxCapture.imageFormat = (ImageFormatType)EditorGUILayout.EnumPopup("Image Format", skyboxCapture.imageFormat);
      if (skyboxCapture.imageFormat == ImageFormatType.JPEG)
      {
        skyboxCapture.jpgQuality = EditorGUILayout.IntField("Encode Quality", skyboxCapture.jpgQuality);
      }

      GUILayout.Label("Material Settings", EditorStyles.boldLabel);

      EditorGUILayout.PropertyField(equirectMaterial, new GUIContent("Equirect Material"), true);
      EditorGUILayout.PropertyField(cubemapMaterial, new GUIContent("Cubemap Material"), true);

      GUILayout.Space(10);

      if (GUILayout.Button("Capture"))
      {
        // Call skybox start capture
        skyboxCapture.StartCapture();
      }

      if (GUILayout.Button("Browse"))
      {
        // Open video save directory
        Utils.BrowseFolder(skyboxCapture.saveFolder);
      }

      GUILayout.Space(10);

      if (GUI.changed)
      {
        EditorUtility.SetDirty(target);
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
      }

      // Apply changes to the serializedProperty - always do this at the end of OnInspectorGUI.
      serializedObject.ApplyModifiedProperties();
    }
  }
}