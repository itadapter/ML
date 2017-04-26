﻿using System;
using ML.Core;
using ML.Contracts;

namespace ML.DeepMethods.Models
{
  /// <summary>
  /// Applies activation function to input
  /// </summary>
  public class ActivationLayer : DeepLayerBase
  {
    #region .ctor

    public ActivationLayer(IActivationFunction activation)
      : base(outputDepth: 1, // will be overridden with input depth when building the layer
             windowSize: 1,
             stride: 1,
             padding: 0,
             activation: activation)
    {
      if (m_ActivationFunction==null)
        throw new MLException("Activation function is null");
    }

    #endregion

    public override int ParamCount { get { return 0; } }


    protected override double[][,] DoCalculate(double[][,] input)
    {
      for (int p=0; p<m_InputDepth;  p++)
      for (int i=0; i<m_InputHeight; i++)
      for (int j=0; j<m_InputWidth;  j++)
      {
        m_Value[p][i, j] = m_ActivationFunction.Value(input[p][i, j]);
      }

      return m_Value;
    }

    public override void _Build()
    {
      m_OutputDepth = m_InputDepth;

      base._Build();
    }

    protected override void DoBackprop(DeepLayerBase prevLayer, double[][,] error, double[][,] prevError)
    {
      for (int p=0; p<m_OutputDepth;  p++)
      for (int i=0; i<m_OutputHeight; i++)
      for (int j=0; j<m_OutputWidth;  j++)
      {
        prevError[p][i, j] = error[p][i, j] * prevLayer.Derivative(p, i, j);
      }
    }

    protected override void DoSetLayerGradient(DeepLayerBase prevLayer, double[][,] errors, double[] updates)
    {
    }

    protected override double DoGetParam(int idx)
    {
      return 0;
    }

    protected override void DoSetParam(int idx, double value, bool isDelta)
    {
    }

    protected override void DoUpdateParams(double[] pars, bool isDelta, int cursor)
    {
    }
  }
}