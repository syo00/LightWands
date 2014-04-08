using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Kirinji.LightWands;
using System.Collections.Generic;

namespace LightWandsTest
{
    [TestClass]
    public class EnumerableExtensionsTest
    {
        [TestMethod]
        public async Task ForEachAsyncTest1()
        {
            var result = new List<int>();
            await new[] { 3, 1, 2, 0 }.ForEachAsync(async i => result.Add(await PseudoSlowWork(i)));
            result.Is(3, 1, 2, 0);
        }

        [TestMethod]
        public async Task ForEachAsyncTest2()
        {
            var result = new List<int>();
            await new[] { 3, 1, 2, 0 }.ForEachAsync(async (i, index) =>
                {
                    result.Add(index);
                    result.Add(await PseudoSlowWork(i));
                });
            result.Is(0, 3, 1, 1, 2, 2, 3, 0);
        }
        private static async Task<int> PseudoSlowWork(int i)
        {
            await Task.Delay(i * 100);
            return i;
        }
    }
}
