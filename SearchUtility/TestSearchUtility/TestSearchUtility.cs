using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TechieNotes;

namespace TestTechieNotes
{
    [TestFixture]
    public class TestSearchUtility
    {
        [Test]
        public void TestConstructor()
        {
            var searchUtility = new SearchUtility();
            Assert.NotNull(searchUtility);
        }
    }
}
