﻿using System;

namespace ML.Core.Metric
{
  /// <summary>
  /// Represents Euclidead metrics
  /// </summary>
  public sealed class EuclideanMetric : MetricBase
  {
    public override string ID { get { return "EUCL"; } }
    public override string Name { get { return "Euclidean"; } }

    /// <summary>
    /// Distance between two points
    /// </summary>
    public override double Dist(Point p1, Point p2)
    {
      Point.CheckDimensions(p1, p2);
      return (double)Math.Sqrt(Dist2(p1, p2));
    }
    /// <summary>
    /// Squared distance between two points
    /// </summary>
    public double Dist2(Point p1, Point p2)
    {
      Point.CheckDimensions(p1, p2);

      var dim = p1.Dimension;
      double sum2 = 0.0F;

      for (int i=0; i<dim; i++)
        sum2 += (p1[i]-p2[i])*(p1[i]-p2[i]);

      return sum2;
    }
  }
}
