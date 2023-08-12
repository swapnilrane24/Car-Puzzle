/* Copyright (c) 2020-present Evereal. All rights reserved. */

using UnityEngine;

namespace Evereal.SkyboxCapture
{
  public class HintUI : MonoBehaviour
  {
    public SkyboxCapture skyboxCapture;

    void OnGUI()
    {
      GUI.Label(new Rect(10, 10, 200, 20), "Press \"" + skyboxCapture.captureKey.ToString() + "\" to Capture Skybox Image");
    }
  }
}