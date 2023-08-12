/* Copyright (c) 2017-present Evereal. All rights reserved. */

using UnityEngine;

namespace Evereal.SkyboxCapture
{
  public class AutoRotate : MonoBehaviour
  {
    public int angleSpeed = 30;

    // Update is called once per frame
    void Update()
    {
      // Rotate the object around its local X axis at 30 degree per second
      transform.Rotate(Vector3.up * Time.deltaTime * angleSpeed);
    }
  }
}