﻿using System;
using ML.Core;
using ML.Core.Mathematics;

namespace ML.DeepMethods.Models
{
  /// <summary>
  /// Represents dropout layer (see http://www.cs.toronto.edu/~rsalakhu/papers/srivastava14a.pdf)
  /// Inverted dropout is used instead of vanilla dropout (see http://cs231n.github.io/neural-networks-2)
  /// </summary>
  public class DropoutLayer : DeepLayerBase
  {
    #region Fields

    private double   m_DropRate;
    private double   m_RetainRate;
    private byte[,,] m_Mask;

    private RandomGenerator m_Generator;

    #endregion

    #region .ctor

    public DropoutLayer(double rate,
                        int seed = 0)
      : base(outputDepth: 1, // will be overridden with input depth when building the layer
             windowSize: 1,
             stride : 1,
             padding : 0,
             activation: null)
    {
      if (rate<=0 || rate>=1)
        throw new MLException("Incorrect dropout rate");

      m_DropRate = rate;
      m_RetainRate = 1-rate;
      m_Generator = RandomGenerator.Get(seed);
    }

    #endregion

    #region Properties

    public override int ParamCount { get { return 0; } }

    public double DropRate { get { return m_DropRate; } }

    public double RetainRate { get { return m_RetainRate; } }

    public byte[,,] Mask { get { return m_Mask; } }

    #endregion

    protected override double[,,] DoCalculate(double[,,] input)
    {
      if (m_IsTraining)
      {
        for (int p=0; p<m_InputDepth; p++)
        for (int i=0; i<m_InputSize;  i++)
        for (int j=0; j<m_InputSize;  j++)
        {
          var retain = m_Generator.Bernoulli(m_RetainRate);
          if (retain)
          {
            m_Mask[p, i, j] = 1;
            m_Value[p, i, j] = input[p, i, j] / m_RetainRate;
          }
          else
          {
            m_Mask[p, i, j] = 0;
            m_Value[p, i, j] = 0;
          }
        }
      }
      else
      {
        Array.Copy(input, m_Value, input.Length);
      }

      return m_Value;
    }

    public override void DoBuild()
    {
      m_OutputDepth = m_InputDepth;
      m_OutputSize = m_InputSize;
      m_Mask = new byte[m_OutputDepth, m_OutputSize, m_OutputSize];
      m_Value = new double[m_OutputDepth, m_OutputSize, m_OutputSize];

      if (m_IsTraining)
      {
        m_Mask = new byte[m_OutputDepth, m_OutputSize, m_OutputSize];
        m_Error = new double[m_OutputDepth, m_OutputSize, m_OutputSize];
      }
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
