﻿using System.Collections.Generic;
using ML.Core.Kernels;
using ML.Core.Metric;
using ML.Core.Logical;
using ML.Core.ActivationFunctions;
using ML.Contracts;

namespace ML.Core
{
  public static class Registry
  {
    public static class Kernels
    {
      private static readonly GaussianKernel    m_GaussianKernel    = new GaussianKernel();
      private static readonly QuadraticKernel   m_QuadraticKernel   = new QuadraticKernel();
      private static readonly QuarticKernel     m_QuarticKernel     = new QuarticKernel();
      private static readonly RectangularKernel m_RectangularKernel = new RectangularKernel();
      private static readonly TriangularKernel  m_TriangularKernel  = new TriangularKernel();

      public static readonly Dictionary<string, IFunction> ByID = new Dictionary<string, IFunction>
      {
        { m_GaussianKernel.ID,    m_GaussianKernel },
        { m_QuadraticKernel.ID,   m_QuadraticKernel },
        { m_QuarticKernel.ID,     m_QuarticKernel },
        { m_RectangularKernel.ID, m_RectangularKernel },
        { m_TriangularKernel.ID,  m_TriangularKernel }
      };

      public static GaussianKernel    GaussianKernel    { get { return m_GaussianKernel; } }
      public static QuadraticKernel   QuadraticKernel   { get { return m_QuadraticKernel; } }
      public static QuarticKernel     QuarticKernel     { get { return m_QuarticKernel; } }
      public static RectangularKernel RectangularKernel { get { return m_RectangularKernel; } }
      public static TriangularKernel  TriangularKernel  { get { return m_TriangularKernel; } }
    }

    public static class Metrics
    {
      private static readonly EuclideanMetric m_EuclideanMetric = new EuclideanMetric();
      private static readonly LInftyMetric    m_LInftyMetric    = new LInftyMetric();
      private static readonly Dictionary<double, LpMetric> m_LpMetrics= new Dictionary<double, LpMetric>();

      public static readonly Dictionary<string, IMetric> ByID = new Dictionary<string, IMetric>
      {
        { m_EuclideanMetric.ID, m_EuclideanMetric },
        { m_LInftyMetric.ID,    m_LInftyMetric }
      };

      public static EuclideanMetric EuclideanMetric { get { return m_EuclideanMetric; } }
      public static LInftyMetric    LInftyMetric { get { return m_LInftyMetric; } }
      public static LpMetric        LpMetric(double p)
      {
        LpMetric result;
        if (!m_LpMetrics.TryGetValue(p, out result))
        {
          result = new LpMetric(p);
          m_LpMetrics[p] = result;
        }

        return result;
      }
    }

    public static class Informativities<TObj>
    {
      private static readonly GiniIndex<TObj>    m_GiniInformativity    = new GiniIndex<TObj>();
      private static readonly DonskoyIndex<TObj> m_DonskoyInformativity = new DonskoyIndex<TObj>();
      private static readonly EntropyIndex<TObj> m_EntropyInformativity = new EntropyIndex<TObj>();

      public static readonly Dictionary<string, IInformIndex<TObj>> ByID = new Dictionary<string, IInformIndex<TObj>>
      {
        { m_GiniInformativity.ID,    m_GiniInformativity },
        { m_DonskoyInformativity.ID, m_DonskoyInformativity },
        { m_EntropyInformativity.ID, m_EntropyInformativity }
      };

      public static GiniIndex<TObj>    GiniInfomativity     { get { return m_GiniInformativity; } }
      public static DonskoyIndex<TObj> DonskoyInformativity { get { return m_DonskoyInformativity; } }
      public static EntropyIndex<TObj> EntropyInformativity { get { return m_EntropyInformativity; } }
    }

    public static class ActivationFunctions
    {
      private static readonly ArctanActivation   m_Atan       = new ArctanActivation();
      private static readonly StepActivation     m_Step = new StepActivation();
      private static readonly IdentityActivation m_Identity   = new IdentityActivation();
      private static readonly ReLUActivation     m_ReLU       = new ReLUActivation();
      private static readonly TanhActivation     m_Tanh       = new TanhActivation();
      private static readonly ExpActivation      m_Exp        = new ExpActivation();
      private static readonly SignActivation     m_Sign       = new SignActivation();
      private static readonly Dictionary<double, RationalActivation> m_Rationals = new Dictionary<double, RationalActivation>();
      private static readonly Dictionary<double, ShiftedStepActivation> m_ShiftedSteps = new Dictionary<double, ShiftedStepActivation>();
      private static readonly Dictionary<double, LogisticActivation> m_Logistics = new Dictionary<double, LogisticActivation>();

      public static readonly Dictionary<string, IFunction> ByID = new Dictionary<string, IFunction>
      {
        { m_Atan.ID,       m_Atan },
        { m_Step.ID, m_Step },
        { m_Identity.ID,   m_Identity },
        { m_ReLU.ID,       m_ReLU },
        { m_Tanh.ID,       m_Tanh },
        { m_Exp.ID,        m_Exp },
        { m_Sign.ID,       m_Sign }
      };

      public static ArctanActivation      Atan     { get { return m_Atan; } }
      public static StepActivation        Step     { get { return m_Step; } }
      public static IdentityActivation    Identity { get { return m_Identity; } }
      public static ReLUActivation        ReLU     { get { return m_ReLU; } }
      public static TanhActivation        Tanh     { get { return m_Tanh; } }
      public static ExpActivation         Exp      { get { return m_Exp; } }
      public static SignActivation        Sign     { get { return m_Sign; } }
      public static ShiftedStepActivation ShiftedStep(double p)
      {
        ShiftedStepActivation result;
        if (!m_ShiftedSteps.TryGetValue(p, out result))
        {
          result = new ShiftedStepActivation(p);
          m_ShiftedSteps[p] = result;
        }

        return result;
      }
      public static RationalActivation Rational(double p)
      {
        RationalActivation result;
        if (!m_Rationals.TryGetValue(p, out result))
        {
          result = new RationalActivation(p);
          m_Rationals[p] = result;
        }

        return result;
      }
      public static LogisticActivation Logistic(double a)
      {
        LogisticActivation result;
        if (!m_Logistics.TryGetValue(a, out result))
        {
          result = new LogisticActivation(a);
          m_Logistics[a] = result;
        }

        return result;
      }
    }
  }
}
