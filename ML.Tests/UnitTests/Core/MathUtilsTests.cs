﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ML.Core.Mathematics;

namespace ML.Tests.UnitTests.Core
{
  [TestClass]
  public class MathUtilsTests : TestBase
  {
    [ClassInitialize]
    public static void ClassInit(TestContext context)
    {
      BaseClassInit(context);
    }

    [TestMethod]
    public void EntropyH_Values()
    {
      Assert.AreEqual(0.0F,              MathUtils.EntropyH(0),    EPS);
      Assert.AreEqual(0.33219280948874F, MathUtils.EntropyH(0.1F), EPS);
      Assert.AreEqual(0.46438561897747F, MathUtils.EntropyH(0.2F), EPS);
      Assert.AreEqual(0.52108967824986F, MathUtils.EntropyH(0.3F), EPS);
      Assert.AreEqual(0.52877123795494F, MathUtils.EntropyH(0.4F), EPS);
      Assert.AreEqual(0.5F,              MathUtils.EntropyH(0.5F), EPS);
      Assert.AreEqual(0.44217935649972F, MathUtils.EntropyH(0.6F), EPS);
      Assert.AreEqual(0.36020122098083F, MathUtils.EntropyH(0.7F), EPS);
      Assert.AreEqual(0.25754247590989F, MathUtils.EntropyH(0.8F), EPS);
      Assert.AreEqual(0.13680278410054F, MathUtils.EntropyH(0.9F), EPS);
      Assert.AreEqual(0.0F,              MathUtils.EntropyH(1.0F), EPS);
    }
  }
}
