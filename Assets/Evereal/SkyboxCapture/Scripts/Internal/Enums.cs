/* Copyright (c) 2017-present Evereal. All rights reserved. */

using System;

namespace Evereal.SkyboxCapture
{
  [Serializable]
  public enum ProjectionType
  {
    /// <summary>
    /// The equirectangular projection type.
    /// See: https://en.wikipedia.org/wiki/Equirectangular_projection
    /// </summary>
    Equirectangular,
    /// <summary>
    /// The cubemap projection type.
    /// See: https://docs.unity3d.com/Manual/class-Cubemap.html
    /// </summary>
    Cubemap
  }

  [Serializable]
  public enum CubemapSizeType
  {
    _512,
    _1024,
    _2048,
    _4096
  }

  [Serializable]
  public enum CubemapLayoutType
  {
    /// <summary>
    ///
    /// The horizontal cross layout:
    ///
    /// +------------+------------+------------+------------+
    /// |            |            |            |            |
    /// |            |            |            |            |
    /// |            |  +Y (Top)  |            |            |
    /// |            |            |            |            |
    /// |            |            |            |            |
    /// +------------+------------+------------+------------+
    /// |            |            |            |            |
    /// |            |            |            |            |
    /// | -X (Left)  | +Z (Front) | +X (Right) | -Z (Back)  |
    /// |            |            |            |            |
    /// |            |            |            |            |
    /// +------------+------------+------------+------------+
    /// |            |            |            |            |
    /// |            |            |            |            |
    /// |            | -Y (Bottom)|            |            |
    /// |            |            |            |            |
    /// |            |            |            |            |
    /// +------------+------------+------------+------------+
    ///
    /// </summary>
    HorizontalCross,
    /// <summary>
    ///
    /// The 6 sided layout:
    ///
    /// +------------+ +------------+ +------------+
    /// |            | |            | |            |
    /// |            | |            | |            |
    /// | +X (Right) | | -X (Left)  | |  +Y (Top)  |
    /// |            | |            | |            |
    /// |            | |            | |            |
    /// +------------+ +------------+ +------------+
    /// |            | |            | |            |
    /// |            | |            | |            |
    /// | -Y (Bottom)| | +Z (Front) | | -Z (Back)  |
    /// |            | |            | |            |
    /// |            | |            | |            |
    /// +------------+ +------------+ +------------+
    ///
    /// </summary>
    SixSided,
    /// <summary>
    ///
    /// The compact layout:
    ///
    /// +------------+------------+------------+
    /// |            |            |            |
    /// |            |            |            |
    /// | +X (Right) | -X (Left)  |  +Y (Top)  |
    /// |            |            |            |
    /// |            |            |            |
    /// +------------+------------+------------+
    /// |            |            |            |
    /// |            |            |            |
    /// | -Y (Bottom)| +Z (Front) | -Z (Back)  |
    /// |            |            |            |
    /// |            |            |            |
    /// +------------+------------+------------+
    ///
    /// </summary>
    Compact
  }

  [Serializable]
  public enum AntiAliasingType
  {
    _1,
    _2,
    _4,
    _8
  }

  [Serializable]
  public enum ImageFormatType
  {
    PNG,
    JPEG,
  }

  [Serializable]
  public enum ImageSizeType
  {
    /// <summary>
    /// 720p (1440 x 720) High Definition (HD).
    /// </summary>
    _1440x720,
    /// <summary>
    /// 2K (2160 x 1080).
    /// </summary>
    _2048x1024,
    /// <summary>
    /// 4K (4096 x 2048) Ultra High Definition (UHD).
    /// </summary>
    _4096x2048,
    /// <summary>
    /// 8K (8192 x 4096) Ultra High Definition (UHD).
    /// </summary>
    _8192x4096,
    /// <summary>
    /// 12K (12288 x 6144).
    /// </summary>
    _12288x6144,
    /// <summary>
    /// 16K (16384 x 8192).
    /// </summary>
    _16384x8192,
  }
}