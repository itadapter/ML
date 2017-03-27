﻿using System.Collections.Generic;
using ML.Core.Kernels;
using ML.Core.Metric;
using ML.Core.Logical;
using ML.Core.ActivationFunctions;
using ML.Contracts;
using ML.Core.LossFunctions;

namespace ML.Core
{
  public static class Registry
  {
    public static class Kernels
    {
      public static readonly GaussianKernel    Gaussian    = new GaussianKernel();
      public static readonly QuadraticKernel   Quadratic   = new QuadraticKernel();
      public static readonly QuarticKernel     Quartic     = new QuarticKernel();
      public static readonly RectangularKernel Rectangular = new RectangularKernel();
      public static readonly TriangularKernel  Triangular  = new TriangularKernel();

      public static readonly Dictionary<string, IFunction> ByID = new Dictionary<string, IFunction>
      {
        { Gaussian.ID,    Gaussian },
        { Quadratic.ID,   Quadratic },
        { Quartic.ID,     Quartic },
        { Rectangular.ID, Rectangular },
        { Triangular.ID,  Triangular }
      };
    }

    public static class Metrics
    {
      private static readonly Dictionary<double, LpMetric> m_Lp = new Dictionary<double, LpMetric>();

      public static readonly EuclideanMetric Euclidean = new EuclideanMetric();
      public static readonly LInftyMetric    LInfty    = new LInftyMetric();

      public static readonly Dictionary<string, IMetric> ByID = new Dictionary<string, IMetric>
      {
        { Euclidean.ID, Euclidean },
        { LInfty.ID,    LInfty }
      };

      public static LpMetric Lp(double p)
      {
        LpMetric result;
        if (!m_Lp.TryGetValue(p, out result))
        {
          result = new LpMetric(p);
          m_Lp[p] = result;
        }

        return result;
      }
    }

    public static class Informativities<TObj>
    {
      public static readonly GiniIndex<TObj>    Gini    = new GiniIndex<TObj>();
      public static readonly DonskoyIndex<TObj> Donskoy = new DonskoyIndex<TObj>();
      public static readonly EntropyIndex<TObj> Entropy = new EntropyIndex<TObj>();

      public static readonly Dictionary<string, IInformativityIndex<TObj>> ByID = new Dictionary<string, IInformativityIndex<TObj>>
      {
        { Gini.ID,    Gini },
        { Donskoy.ID, Donskoy },
        { Entropy.ID, Entropy }
      };
    }

    public static class ActivationFunctions
    {
      private static readonly Dictionary<double, RationalActivation>    m_Rationals    = new Dictionary<double, RationalActivation>();
      private static readonly Dictionary<double, ShiftedStepActivation> m_ShiftedSteps = new Dictionary<double, ShiftedStepActivation>();
      private static readonly Dictionary<double, LogisticActivation>    m_Logistics    = new Dictionary<double, LogisticActivation>();

      public static readonly ArctanActivation   Atan       = new ArctanActivation();
      public static readonly StepActivation     Step       = new StepActivation();
      public static readonly IdentityActivation Identity   = new IdentityActivation();
      public static readonly ReLUActivation     ReLU       = new ReLUActivation();
      public static readonly TanhActivation     Tanh       = new TanhActivation();
      public static readonly ExpActivation      Exp        = new ExpActivation();
      public static readonly SignActivation     Sign       = new SignActivation();

      public static readonly Dictionary<string, IFunction> ByID = new Dictionary<string, IFunction>
      {
        { Atan.ID,     Atan },
        { Step.ID,     Step },
        { Identity.ID, Identity },
        { ReLU.ID,     ReLU },
        { Tanh.ID,     Tanh },
        { Exp.ID,      Exp },
        { Sign.ID,     Sign }
      };

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

    public static class LossFunctions
    {
      private static readonly Dictionary<double, LpLoss> m_Lp = new Dictionary<double, LpLoss>();

      public static readonly EuclideanLoss           Euclidean           = new EuclideanLoss();
      public static readonly CrossEntropyLoss        CrossEntropy        = new CrossEntropyLoss();
      public static readonly CrossEntropySoftMaxLoss CrossEntropySoftMax = new CrossEntropySoftMaxLoss();

      public static LpLoss Lp(double p)
      {
        LpLoss result;
        if (!m_Lp.TryGetValue(p, out result))
        {
          result = new LpLoss(p);
          m_Lp[p] = result;
        }

        return result;
      }
    }
  }
}
