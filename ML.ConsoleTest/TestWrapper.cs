﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using ML.Core;
using ML.Core.Metric;
using ML.Core.Kernels;
using ML.Contracts;
using ML.LogicalMethods.Algorithms;
using ML.MetricalMethods.Algorithms;
using ML.Core.Logical;

namespace ML.ConsoleTest
{
  public class TestWrapper
  {
    public TestWrapper(DataWrapper data)
    {
      if (data == null)
        throw new ArgumentException("TestWrapper.ctor(data=null)");

      Data = data;
      Visualizer = new Visualizer(data);
    }

    public readonly DataWrapper Data;
    public readonly Visualizer  Visualizer;


    public void Run()
    {
      //doNearestNeighbourAlgorithmTest();
      //doNearestKNeighboursAlgorithmTest();
      //doParzenFixedAlgorithmTest();
      //doPotentialFixedAlgorithmTest();

      doDecisionTreeAlgorithmTest();
    }

    #region .pvt

    private void doNearestNeighbourAlgorithmTest()
    {
      var metric = new EuclideanMetric();
      var alg = new NearestNeighbourAlgorithm(Data.TrainingSample, metric);

      Console.WriteLine("Margin:");
      calculateMargin(alg);

      Console.WriteLine("Errors:");
      var errors = alg.GetErrors(Data.Data);
      var ec = errors.Count();
      var dc = Data.Data.Count;
      var pct = Math.Round(100.0F * ec / dc, 2);
      Console.WriteLine("{0} of {1} ({2}%)", ec, dc, pct);

      Visualizer.Run(alg);
    }

    private void doNearestKNeighboursAlgorithmTest()
    {
      var metric = new EuclideanMetric();
      var alg = new NearestKNeighboursAlgorithm(Data.TrainingSample, metric, 1);

      // LOO
      alg.Train_LOO();
      var optK = alg.K;
      Console.WriteLine("Nearest K Neigbour: optimal k is {0}", optK);
      Console.WriteLine();

      // Margins
      Console.WriteLine("Margins:");
      calculateMargin(alg);
      Console.WriteLine();

      //Error distribution
      Console.WriteLine("Errors:");
      for (int k = 1; k < 5; k++)
      {
        alg.K = k;
        var errors = alg.GetErrors(Data.Data);
        var ec = errors.Count();
        var dc = Data.Data.Count;
        var pct = Math.Round(100.0F * ec / dc, 2);
        Console.WriteLine("{0}:\t{1} of {2}\t({3}%) {4}", k, ec, dc, pct, k == optK ? "<-LOO optimal" : string.Empty);
      }
      Console.WriteLine();

      Visualizer.Run(alg);
    }

    private void doParzenFixedAlgorithmTest()
    {
      var timer = new System.Diagnostics.Stopwatch();
      timer.Start();

      var metric = new EuclideanMetric();
      var kernel = new GaussianKernel();
      var alg = new ParzenFixedAlgorithm(Data.TrainingSample, metric, kernel, 1.0F);

      // LOO
      alg.Train_LOO(0.1F, 20.0F, 0.2F);
      var optH = alg.H;
      Console.WriteLine("Parzen Fixed: optimal h is {0}", optH);
      Console.WriteLine();

      // Margins
      Console.WriteLine("Margins:");
      calculateMargin(alg);
      Console.WriteLine();

      //var x = algorithm.Classify(new Point(new float[] { -3, 0 }));

      //Error distribution
      Console.WriteLine("Errors:");
      var step = 0.1F;
      for (float h1 = step; h1 < 5; h1 += step)
      {
        var h = h1;
        if (h <= optH && h + step > optH) h = optH;

        alg.H = h;
        var errors = alg.GetErrors(Data.Data);
        var ec = errors.Count();
        var dc = Data.Data.Count;
        var pct = Math.Round(100.0F * ec / dc, 2);
        Console.WriteLine("{0}:\t{1} of {2}\t({3}%) {4}", Math.Round(h, 2), ec, dc, pct, h == optH ? "<-LOO optimal" : string.Empty);
      }
      Console.WriteLine();

      Visualizer.Run(alg);

      timer.Stop();
      Console.WriteLine(timer.ElapsedMilliseconds/1000.0F);
    }

    private void doPotentialFixedAlgorithmTest()
    {
      var metric = new EuclideanMetric();
      var kernel = new GaussianKernel();

      var eqps = new PotentialFunctionAlgorithm.KernelEquipment[Data.TrainingSample.Count];
      for (int i=0; i<Data.TrainingSample.Count; i++)
        eqps[i] = new PotentialFunctionAlgorithm.KernelEquipment(1.0F, 1.5F);
      var alg = new PotentialFunctionAlgorithm(Data.TrainingSample, metric, kernel, eqps);

      Console.WriteLine("Margin:");
      calculateMargin(alg);

      Console.WriteLine("Errors:");
      var errors = alg.GetErrors(Data.Data);
      var ec = errors.Count();
      var dc = Data.Data.Count;
      var pct = Math.Round(100.0F * ec / dc, 2);
      Console.WriteLine("{0} of {1} ({2}%)", ec, dc, pct);

      Visualizer.Run(alg);
    }

    private void doDecisionTreeAlgorithmTest()
    {
      var dim = Data.TrainingSample.First().Key.Dimension;
      var patterns = getSimpleLogicPatterns(dim);

      var alg = new DecisionTreeAlgorithm(Data.TrainingSample);
      var informativity = new GiniInformativity();
      alg.Train_ID3(patterns, informativity);

      Console.WriteLine("Errors:");
      var errors = alg.GetErrors(Data.Data);
      var ec = errors.Count();
      var dc = Data.Data.Count;
      var pct = Math.Round(100.0F * ec / dc, 2);
      Console.WriteLine("{0} of {1} ({2}%)", ec, dc, pct);

      Visualizer.Run(alg);
    }



    private void calculateMargin(IMetricAlgorithm algorithm)
    {
      var res = algorithm.CalculateMargins();
      foreach (var r in res)
      {
        Console.WriteLine("{0}\t{1}", r.Key, r.Value);
      }
    }

    private IEnumerable<Predicate<Point>> getSimpleLogicPatterns(int dim)
    {
      float step = 0.5F;
      float min = 0;
      float max = 1;

      for (int i=0; i<dim; i++)
      {
        var idx = i;
        for (float l=min; l<=max; l += step)
          yield return (p => p[idx]<l);
      }
    }

    #endregion
  }
}