using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PocketGoogle;

namespace PocketGoogleTests
{
    [TestFixture]
    public class Indexer_Tests
    {
        private Dictionary<int, string> dictionary = new Dictionary<int, string>()
            {
                { 0, "A B C" },
                { 1, "B C" },
                { 2, "A C A C" },
                { 3, "F, f ff" }
            };
        private readonly Indexer i = new Indexer();

        [Test]
        [Order(00)]
        public void Add()
        {
            var actual = true;
            foreach (var d in dictionary)
                i.Add(d.Key, d.Value);
            Assert.AreEqual(true, actual);
        }
       
        [TestCase("C", new int[3] { 0, 1, 2 })]
        [TestCase("X", new int[0])]
        [Order(01)]
        public void GetIds(string word, int[] expected)
        {
            var actual = i.GetIds(word);
            Assert.AreEqual(expected.ToList(), actual);
        }

        [TestCase("A", 0, new int[1] { 0 })]
        [TestCase("A", 2, new int[2] { 0, 4})]
        [Order(02)]
        public void GetPositions(string word, int id, int[] expected)
        {
            var actual = i.GetPositions(id, word);
            Assert.AreEqual(expected.ToList(), actual);
        }

        [TestCase("A", 1, new int[2] { 0, 2 })]
        [TestCase("A", 0, new int[1] { 2 })]
        [TestCase("A", 2, new int[0])]
        [Order(03)]
        public void Remove(string word, int id, int[] expected)
        {
            i.Remove(id);
            var actual = i.GetIds(word);
            Assert.AreEqual(expected.ToList(), actual);
        }

        [TestCase("B", 1, new int[1] { 0 })]
        public void Add_Remove_Add_GetPosition(string word, int id, int[] expected)
        {
            i.Add(id, word);
            i.Remove(id);
            i.Add(id, word);
            var actual = i.GetPositions(id, word);
            Assert.AreEqual(expected.ToList(), actual);
        }
    }
}
