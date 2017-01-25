﻿using System;
using System.Linq;
using System.Collections.Generic;
using ML.Contracts;
using ML.Core;

namespace ML.MetricalMethods.Algorithms
{
  public sealed class PotentialFunctionAlgorithm : MetricAlgorithmBase
  {
    #region Inner

    public struct KernelEquipment
    {
      public KernelEquipment(float gamma, float h)
      {
        Gamma = gamma;
        H = h;
      }

      public float Gamma;
      public float H;
    }

    #endregion

    private readonly IKernel m_Kernel;
    private KernelEquipment[] m_Eqps;

    public PotentialFunctionAlgorithm(ClassifiedSample classifiedSample,
                                      IMetric metric,
                                      IKernel kernel,
                                      KernelEquipment[] eqps)
      : base(classifiedSample, metric)
    {
      if (kernel == null)
        throw new MLException("PotentialAlgorithm.ctor(kernel=null)");

      m_Kernel = kernel;
      Eqps = eqps;
    }

    public override string ID { get { return "PF"; } }

    public override string Name { get { return "Potential Functions"; } }

    public IKernel Kernel { get { return m_Kernel; } }

    public KernelEquipment[] Eqps
    {
      get { return m_Eqps; }
      set
      {
        KernelEquipment[] eqps;
        if (value==null)
        {
          var cnt = TrainingSample.Count;
          eqps = new KernelEquipment[cnt];
          for (int i=0; i<cnt; i++)
            eqps[i] = new KernelEquipment(1.0F, 1.0F);
        }
        else
        {
          eqps = value.ToArray();
        }

        m_Eqps = eqps;
      }
    }

    public override float EstimateClose(Point x, Class cls)
    {
      var closeness = 0.0F;
      int idx = -1;

      foreach (var sData in TrainingSample)
      {
        idx++;
        if (sData.Value != cls) continue;

        var r = Metric.Dist(x, sData.Key) / m_Eqps[idx].H;
        closeness += m_Eqps[idx].Gamma * m_Kernel.Calculate(r);
      }

      return closeness;
    }
  }
}