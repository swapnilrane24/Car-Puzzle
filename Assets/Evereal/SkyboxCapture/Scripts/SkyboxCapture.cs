/* Copyright (c) 2017-present Evereal. All rights reserved. */

using UnityEngine;
using System.IO;
using System.Threading;

namespace Evereal.SkyboxCapture
{
  [RequireComponent(typeof(Camera)), ExecuteInEditMode]
  public class SkyboxCapture : MonoBehaviour
  {
    #region Properties

    [SerializeField]
    public KeyCode captureKey = KeyCode.Space;

    [SerializeField]
    public string saveFolder = "";
    // The save folder full path
    private string saveFolderFullPath = "";

    [SerializeField]
    public ProjectionType projectionType = ProjectionType.Equirectangular;

    [SerializeField]
    public CubemapSizeType cubemapSize = CubemapSizeType._1024;

    [SerializeField]
    public CubemapLayoutType cubemapLayout = CubemapLayoutType.Compact;

    // The output image format.
    [SerializeField]
    public ImageFormatType imageFormat = ImageFormatType.PNG;
    [SerializeField]
    public int jpgQuality = 75;

    [SerializeField]
    public ImageSizeType imageSize = ImageSizeType._4096x2048;

    [SerializeField]
    public AntiAliasingType antiAliasing = AntiAliasingType._1;

    /// <summary>
    /// For generate equirectangular skybox.
    /// </summary>
    [SerializeField]
    public Material equirectMaterial;

    /// <summary>
    /// For generate cubemap skybox.
    /// </summary>
    [SerializeField]
    public Material cubemapMaterial;

    /// <summary>
    /// Camera for capture skybox.
    /// </summary>
    private Camera captureCamera;

    /// <summary>
    /// Include camera rotation for render to cubemap.
    /// <summary>
    protected bool includeCameraRotation = true;

    /// <summary>
    /// Capture properties.
    /// </summary>
    CubemapFace[] faces = new CubemapFace[] {
      CubemapFace.PositiveX,
      CubemapFace.NegativeX,
      CubemapFace.PositiveY,
      CubemapFace.NegativeY,
      CubemapFace.PositiveZ,
      CubemapFace.NegativeZ
    };
#if !UNITY_ANDROID
    Vector3[] faceAngles = new Vector3[] {
      new Vector3(0.0f, 90.0f, 0.0f),
      new Vector3(0.0f, -90.0f, 0.0f),
      new Vector3(-90.0f, 0.0f, 0.0f),
      new Vector3(90.0f, 0.0f, 0.0f),
      new Vector3(0.0f, 0.0f, 0.0f),
      new Vector3(0.0f, 180.0f, 0.0f)
    };
#endif

    /// <summary>
    /// The skybox image save path.
    /// </summary>
    private string imageSavePath;
    /// <summary>
    /// The skybox image data.
    /// </summary>
    private byte[] imageData;

    // Log message format template
    protected string LOG_FORMAT = "[SkyboxCapture] {0}";

    public int CubemapSize
    {
      get
      {
        if (cubemapSize == CubemapSizeType._512) return 512;
        if (cubemapSize == CubemapSizeType._1024) return 1024;
        if (cubemapSize == CubemapSizeType._2048) return 2048;
        if (cubemapSize == CubemapSizeType._4096) return 4096;
        return 1;
      }
    }

    public int ImageWidth
    {
      get
      {
        if (projectionType == ProjectionType.Cubemap)
        {
          if (cubemapLayout == CubemapLayoutType.Compact)
            return CubemapSize * 3;
          else if (cubemapLayout == CubemapLayoutType.HorizontalCross)
            return CubemapSize * 4;
          else if (cubemapLayout == CubemapLayoutType.SixSided)
            return CubemapSize;
        }
        if (imageSize == ImageSizeType._1440x720) return 1440;
        if (imageSize == ImageSizeType._2048x1024) return 2048;
        if (imageSize == ImageSizeType._4096x2048) return 4096;
        if (imageSize == ImageSizeType._8192x4096) return 8192;
        if (imageSize == ImageSizeType._12288x6144) return 12288;
        if (imageSize == ImageSizeType._16384x8192) return 16384;
        return 1;
      }
    }

    public int ImageHeight
    {
      get
      {
        if (projectionType == ProjectionType.Cubemap)
        {
          if (cubemapLayout == CubemapLayoutType.Compact)
            return CubemapSize * 2;
          else if (cubemapLayout == CubemapLayoutType.HorizontalCross)
            return CubemapSize * 3;
          else if (cubemapLayout == CubemapLayoutType.SixSided)
            return CubemapSize;
        }
        if (imageSize == ImageSizeType._1440x720) return 720;
        if (imageSize == ImageSizeType._2048x1024) return 1024;
        if (imageSize == ImageSizeType._4096x2048) return 2048;
        if (imageSize == ImageSizeType._8192x4096) return 4096;
        if (imageSize == ImageSizeType._12288x6144) return 6144;
        if (imageSize == ImageSizeType._16384x8192) return 8192;
        return 1;
      }
    }

    public int AntiAliasing
    {
      get
      {
        if (antiAliasing == AntiAliasingType._1) return 1;
        if (antiAliasing == AntiAliasingType._2) return 2;
        if (antiAliasing == AntiAliasingType._4) return 4;
        if (antiAliasing == AntiAliasingType._8) return 8;
        return 1;
      }
    }

#endregion

#region Skybox Capture

    public void StartCapture()
    {
      if (captureCamera == null)
        captureCamera = GetComponent<Camera>();

      saveFolderFullPath = Utils.CreateFolder(saveFolder);
      string ext = imageFormat == ImageFormatType.PNG ? "png" : "jpg";
      imageSavePath = string.Format("{0}skybox_{1}.{2}",
        saveFolderFullPath,
        Utils.GetTimeString(),
        ext);

      imageData = null;

      // Create render texture for final output
      RenderTexture outputTexture = Utils.CreateRenderTexture(ImageWidth, ImageHeight, 24, AntiAliasing, null);

      if (projectionType == ProjectionType.Cubemap)
      {
        int faceWidth = CubemapSize;
        int faceHeight = CubemapSize;

        // Reset capture camera rotation.
        captureCamera.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

        // Create texture for final output.
        Texture2D texture2D = Utils.CreateTexture(ImageWidth, ImageHeight, null);

        // For intermediate saving.
        Texture2D swapTexture = Utils.CreateTexture(faceWidth, faceHeight, null);

#if !UNITY_ANDROID
        // Create cubemap face render texture.
        RenderTexture faceTexture = Utils.CreateRenderTexture(faceWidth, faceHeight, 0, AntiAliasing, null);

        // Prepare for target render texture.
        captureCamera.targetTexture = faceTexture;
#else
        // Create cubemap for rendering.
        Cubemap cubemap = new Cubemap(CubemapSize, TextureFormat.RGBA32, false);
        captureCamera.RenderToCubemap(cubemap);
#endif

        if (cubemapLayout == CubemapLayoutType.Compact)
        {
          for (int i = 0; i < faces.Length; i++)
          {
#if !UNITY_ANDROID
            captureCamera.transform.eulerAngles = faceAngles[i];
            captureCamera.Render();
            RenderTexture.active = faceTexture;
            swapTexture.ReadPixels(new Rect(0, 0, faceWidth, faceHeight), 0, 0, false);
            Color[] pixels = swapTexture.GetPixels();
#else
            // read screen contents into the texture
            Color[] pixels = cubemap.GetPixels(faces[i]);
            // rotate texture
            System.Array.Reverse(pixels, 0, pixels.Length);
            for (int row = 0; row < faceHeight; ++row)
              System.Array.Reverse(pixels, row * faceWidth, faceWidth);
            swapTexture.SetPixels(pixels);
#endif
            switch ((CubemapFace)i)
            {
              case CubemapFace.PositiveX:
                texture2D.SetPixels(0, faceHeight, faceWidth, faceHeight, pixels);
                break;
              case CubemapFace.NegativeX:
                texture2D.SetPixels(faceWidth, faceHeight, faceWidth, faceHeight, pixels);
                break;
              case CubemapFace.PositiveY:
                texture2D.SetPixels(faceWidth * 2, faceHeight, faceWidth, faceHeight, pixels);
                break;
              case CubemapFace.NegativeY:
                texture2D.SetPixels(0, 0, faceWidth, faceHeight, pixels);
                break;
              case CubemapFace.PositiveZ:
                texture2D.SetPixels(faceWidth, 0, faceWidth, faceHeight, pixels);
                break;
              case CubemapFace.NegativeZ:
                texture2D.SetPixels(faceWidth * 2, 0, faceWidth, faceHeight, pixels);
                break;
            }
          }
          texture2D.Apply();

          // Save image file.
          WriteTexture2DImage(texture2D);
        }
        else if (cubemapLayout == CubemapLayoutType.HorizontalCross)
        {
          for (int i = 0; i < faces.Length; i++)
          {
#if !UNITY_ANDROID
            captureCamera.transform.eulerAngles = faceAngles[i];
            captureCamera.Render();
            RenderTexture.active = faceTexture;
            swapTexture.ReadPixels(new Rect(0, 0, faceWidth, faceHeight), 0, 0, false);
            Color[] pixels = swapTexture.GetPixels();
#else
            // read screen contents into the texture
            Color[] pixels = cubemap.GetPixels(faces[i]);
            // rotate texture
            System.Array.Reverse(pixels, 0, pixels.Length);
            for (int row = 0; row < faceHeight; ++row)
              System.Array.Reverse(pixels, row * faceWidth, faceWidth);
            swapTexture.SetPixels(pixels);
#endif
            switch ((CubemapFace)i)
            {
              case CubemapFace.PositiveX:
                texture2D.SetPixels(faceWidth * 2, faceHeight, faceWidth, faceHeight, pixels);
                break;
              case CubemapFace.NegativeX:
                texture2D.SetPixels(0, faceHeight, faceWidth, faceHeight, pixels);
                break;
              case CubemapFace.PositiveY:
                //texture.SetPixels(width, 0, width, height, pixels);
                texture2D.SetPixels(faceWidth, faceHeight * 2, faceWidth, faceHeight, pixels);
                break;
              case CubemapFace.NegativeY:
                //texture.SetPixels(width, height * 2, width, height, pixels);
                texture2D.SetPixels(faceWidth, 0, faceWidth, faceHeight, pixels);
                break;
              case CubemapFace.PositiveZ:
                texture2D.SetPixels(faceWidth, faceHeight, faceWidth, faceHeight, pixels);
                break;
              case CubemapFace.NegativeZ:
                texture2D.SetPixels(faceWidth * 3, faceHeight, faceWidth, faceHeight, pixels);
                break;
            }
          }
          texture2D.Apply();

          // Save image file.
          WriteTexture2DImage(texture2D);
        }
        else if (cubemapLayout == CubemapLayoutType.SixSided)
        {
          for (int i = 0; i < faces.Length; i++)
          {
#if !UNITY_ANDROID
            captureCamera.transform.eulerAngles = faceAngles[i];
            captureCamera.Render();
            RenderTexture.active = faceTexture;
            swapTexture.ReadPixels(new Rect(0, 0, faceWidth, faceHeight), 0, 0, false);
            Color[] pixels = swapTexture.GetPixels();
#else
            // read screen contents into the texture
            Color[] pixels = cubemap.GetPixels(faces[i]);
            // rotate texture
            System.Array.Reverse(pixels, 0, pixels.Length);
            for (int row = 0; row < faceHeight; ++row)
              System.Array.Reverse(pixels, row * faceWidth, faceWidth);
            swapTexture.SetPixels(pixels);
#endif
            string timeStamp = Utils.GetTimeString();
            switch ((CubemapFace)i)
            {
              case CubemapFace.PositiveX:
                imageSavePath = string.Format("{0}skybox_{1}_right.{2}",
                  saveFolderFullPath,
                  timeStamp,
                  ext);
                break;
              case CubemapFace.NegativeX:
                imageSavePath = string.Format("{0}skybox_{1}_left.{2}",
                  saveFolderFullPath,
                  timeStamp,
                  ext);
                break;
              case CubemapFace.PositiveY:
                imageSavePath = string.Format("{0}skybox_{1}_top.{2}",
                  saveFolderFullPath,
                  timeStamp,
                  ext);
                break;
              case CubemapFace.NegativeY:
                imageSavePath = string.Format("{0}skybox_{1}_bottom.{2}",
                  saveFolderFullPath,
                  timeStamp,
                  ext);
                break;
              case CubemapFace.PositiveZ:
                imageSavePath = string.Format("{0}skybox_{1}_front.{2}",
                  saveFolderFullPath,
                  timeStamp,
                  ext);
                break;
              case CubemapFace.NegativeZ:
                imageSavePath = string.Format("{0}skybox_{1}_back.{2}",
                  saveFolderFullPath,
                  timeStamp,
                  ext);
                break;
            }

            texture2D.SetPixels(0, 0, faceWidth, faceHeight, pixels);
            texture2D.Apply();

            // Save image file.
            WriteTexture2DImage(texture2D);
          }
        }

        RenderTexture.active = null;

        // Clean texture resources
        if (Application.isEditor)
        {
          DestroyImmediate(texture2D);
          DestroyImmediate(swapTexture);
#if !UNITY_ANDROID
          DestroyImmediate(faceTexture);
#endif
        }
        else
        {
          Destroy(texture2D);
          Destroy(swapTexture);
#if !UNITY_ANDROID
          Destroy(faceTexture);
#endif
        }
      }
      else if (projectionType == ProjectionType.Equirectangular)
      {
        // Create equirectangular render texture.
        RenderTexture equirectTexture = Utils.CreateRenderTexture(CubemapSize, CubemapSize, 24, AntiAliasing, null, false);
        equirectTexture.dimension = UnityEngine.Rendering.TextureDimension.Cube;

        // Render to cubemap.
        captureCamera.RenderToCubemap(equirectTexture);
        captureCamera.Render();

        // Create material for convert cubemap to equirectangular.
        // equirectMaterial = Utils.CreateMaterial("SkyboxCapture/CubemapToEquirect", equirectMaterial);

        if (includeCameraRotation)
        {
          equirectMaterial.SetMatrix("_CubeTransform", Matrix4x4.TRS(Vector3.zero, transform.rotation, Vector3.one));
        }
        else
        {
          equirectMaterial.SetMatrix("_CubeTransform", Matrix4x4.identity);
        }

        // Convert to equirectangular projection.
        Graphics.Blit(equirectTexture, outputTexture, equirectMaterial);

        // From RenderTexture to Texture, save image file.
        WriteTextureImage(outputTexture);

        if (Application.isEditor)
        {
          DestroyImmediate(equirectTexture);
        }
        else
        {
          Destroy(equirectTexture);
        }
      }

      if (Application.isEditor)
      {
        DestroyImmediate(outputTexture);
      }
      else
      {
        Destroy(outputTexture);
      }
    }

#endregion

#region Internal

    private void WriteTextureImage(RenderTexture outputTexture)
    {
      // Bind camera render texture.
      RenderTexture.active = outputTexture;

      Texture2D texture2D = Utils.CreateTexture(ImageWidth, ImageHeight, null);
      // Read screen contents into the texture
      texture2D.ReadPixels(new Rect(0, 0, ImageWidth, ImageHeight), 0, 0);
      texture2D.Apply();

      // Restore RenderTexture states.
      RenderTexture.active = null;

      // Save texture 2d data to image.
      WriteTexture2DImage(texture2D);

      // Clean texture resources
      if (Application.isEditor)
      {
        DestroyImmediate(texture2D);
      }
      else
      {
        Destroy(texture2D);
      }
    }

    private void WriteTexture2DImage(Texture2D texture2D)
    {
      // Encode image for write
      if (imageFormat == ImageFormatType.PNG)
      {
        // Encode texture into PNG
        imageData = texture2D.EncodeToPNG();
      }
      else
      {
        // Encode texture into JPG
        imageData = texture2D.EncodeToJPG(jpgQuality);
      }

      // Start write image thread.
      Thread writeImageThread = new Thread(WriteImageProcess);
      writeImageThread.Priority = System.Threading.ThreadPriority.Lowest;
      writeImageThread.IsBackground = true;
      writeImageThread.Start();
    }

    private void WriteImageProcess()
    {
      while (imageData == null)
      {
        Thread.Sleep(100);
      }
      File.WriteAllBytes(imageSavePath, imageData);

      Debug.LogFormat(LOG_FORMAT, "Save file to: " + imageSavePath);

      imageData = null;
    }

#endregion

#region Unity Lifecycle

    private void Update()
    {
      if (Input.GetKeyDown(captureKey))
      {
        StartCapture();
      }
    }

#endregion
  }
}