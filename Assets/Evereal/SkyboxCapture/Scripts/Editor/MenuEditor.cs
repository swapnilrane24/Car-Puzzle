/* Copyright (c) 2017-present Evereal. All rights reserved. */

using UnityEngine;
using UnityEditor;

namespace Evereal.SkyboxCapture
{
  public class MenuEditor
  {
    [MenuItem("Tools/Evereal/SkyboxCapture/GameObject/SkyboxCapture")]
    private static void CreateSkyboxCaptureObject(MenuCommand menuCommand)
    {
      GameObject skyboxCapturePrefab = PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/SkyboxCapture")) as GameObject;
      skyboxCapturePrefab.name = "SkyboxCapture";
      //PrefabUtility.DisconnectPrefabInstance(skyboxCapturePrefab);
      GameObjectUtility.SetParentAndAlign(skyboxCapturePrefab, menuCommand.context as GameObject);
      Undo.RegisterCreatedObjectUndo(skyboxCapturePrefab, "Create " + skyboxCapturePrefab.name);
      Selection.activeObject = skyboxCapturePrefab;
    }
  }
}