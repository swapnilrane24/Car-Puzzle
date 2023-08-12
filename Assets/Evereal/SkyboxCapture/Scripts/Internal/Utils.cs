/* Copyright (c) 2017-present Evereal. All rights reserved. */

using System;
using System.IO;
using System.Diagnostics;
using UnityEngine;

namespace Evereal.SkyboxCapture
{
  public static class Utils
  {
    /// <summary>
    /// Get timestamp string.
    /// </summary>
    /// <returns></returns>
    public static string GetTimeString()
    {
      return DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
    }

    /// <summary>
    /// Create materials which will be used for equirect and cubemap generation.
    /// </summary>
    /// <param name="sName"> shader name </param>
    /// <param name="m2Create"> material </param>
    /// <returns></returns>
    public static Material CreateMaterial(string sName, Material m2Create)
    {
      if (m2Create && (m2Create.shader.name == sName))
        return m2Create;
      Shader s = Shader.Find(sName);
      return CreateMaterial(s, m2Create);
    }

    /// <summary>
    /// Create materials which will be used for equirect and cubemap generation.
    /// </summary>
    /// <param name="s"> shader code </param>
    /// <param name="m2Create"> material </param>
    /// <returns></returns>
    public static Material CreateMaterial(Shader s, Material m2Create)
    {
      if (!s)
      {
        UnityEngine.Debug.Log("Create material missing shader!");
        return null;
      }

      if (m2Create && (m2Create.shader == s) && (s.isSupported))
        return m2Create;

      if (!s.isSupported)
      {
        return null;
      }

      if (m2Create != null)
      {
        UnityEngine.Object.Destroy(m2Create);
      }

      m2Create = new Material(s);
      m2Create.hideFlags = HideFlags.DontSave;

      return m2Create;
    }

    /// <summary>
    /// Create Texture2D.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="t2Create"></param>
    /// <returns></returns>
    public static Texture2D CreateTexture(int width, int height, Texture2D t2Create)
    {
      if (t2Create && (t2Create.width == width) && (t2Create.height == height))
        return t2Create;

      if (t2Create != null)
      {
        UnityEngine.Object.Destroy(t2Create);
      }

      t2Create = new Texture2D(width, height, TextureFormat.RGBA32, false);
      t2Create.hideFlags = HideFlags.HideAndDontSave;

      return t2Create;
    }

    /// <summary>
    /// Create RenderTexture.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="depth"></param>
    /// <param name="antiAliasing"></param>
    /// <param name="t2Create"></param>
    /// <param name="create"></param>
    /// <returns></returns>
    public static RenderTexture CreateRenderTexture(int width, int height, int depth, int antiAliasing, RenderTexture t2Create, bool create = true)
    {
      if (t2Create &&
        (t2Create.width == width) && (t2Create.height == height) && (t2Create.depth == depth) &&
        (t2Create.antiAliasing == antiAliasing) && (t2Create.IsCreated() == create))
        return t2Create;

      if (t2Create != null)
      {
        UnityEngine.Object.Destroy(t2Create);
      }

      t2Create = new RenderTexture(width, height, depth, RenderTextureFormat.ARGB32);
      //t2Create = new RenderTexture(width, height, depth, RenderTextureFormat.Default);
      t2Create.antiAliasing = antiAliasing;
      t2Create.hideFlags = HideFlags.HideAndDontSave;

      // Make sure render texture is created.
      if (create)
        t2Create.Create();

      return t2Create;
    }

    /// <summary>
    /// Create folder.
    /// </summary>
    /// <param name="f2Create"></param>
    /// <returns></returns>
    public static string CreateFolder(string f2Create)
    {
      string folder = f2Create;
      if (string.IsNullOrEmpty(folder))
      {
        folder = "Captures/";
      }
      if (!folder.EndsWith("/") && !folder.EndsWith("\\"))
      {
        folder += "/";
      }
      if (!Directory.Exists(folder))
      {
        Directory.CreateDirectory(folder);
      }
      return Path.GetFullPath(folder);
    }

    public static void BrowseFolder(string folder)
    {
      if (string.IsNullOrEmpty(folder))
      {
        folder = "Captures/";
      }
      string fullPath = Path.GetFullPath(folder);
      if (Directory.Exists(fullPath))
      {
        Process.Start(Path.GetFullPath(folder));
      }
      else
      {
        UnityEngine.Debug.LogWarning("Folder " + fullPath + " not existed!");
      }
    }
  }
}