﻿using System;
using ML.Contracts;

namespace ML.Core.ActivationFunctions
{
  /// <summary>
  /// Identity Activation Function
  /// </summary>
  public sealed class IdentityActivation : IActivationFunction
  {
    public string ID { get { return "IDT"; } }
    public string Name { get { return "Identity"; } }

    public double Value(double r)
    {
      return r;
    }

    public double Derivative(double r)
    {
      return 1.0D;
    }

    public double DerivativeFromValue(double y)
    {
      return 1.0D;
    }
  }
}
