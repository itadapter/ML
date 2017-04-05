﻿using System;
using ML.Contracts;

namespace ML.DeepMethods.Models
{
  /// <summary>
  /// Flattening layer that transform multidimensional array data into flat one-dimensional fully-connected layer
  /// </summary>
  public class FlattenLayer : ConvolutionalLayer
  {
    public FlattenLayer(int outputDim, IActivationFunction activation = null)
      : base(outputDim,
             windowSize: 1, // will be overridden with input size when building the layer
             stride: 1,
             padding: 0,
             activation: activation)
    {
    }

    public override void DoBuild()
    {
      m_WindowSize = m_InputSize;

      base.DoBuild();
    }
  }
}
