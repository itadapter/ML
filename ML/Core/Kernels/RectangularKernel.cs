﻿using System;
using ML.Contracts;

namespace ML.Core.Kernels
{
  /// <summary>
  /// Rectangular kernel r -> 1, [-1, 1]
  /// </summary>
  public sealed class RectangularKernel : IKernel
  {
    public string ID { get { return "RECT"; } }
    public string Name { get { return "Rectangular"; } }

    public float Calculate(float r)
    {
      return (r > -1 && r < 1) ? 1 : 0;
    }
  }
}
