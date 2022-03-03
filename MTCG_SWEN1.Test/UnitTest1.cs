using NUnit.Framework;
using MTCG_SWEN1.Services;

namespace MTCG_SWEN1.Test
{
    public class Tests
    {
        //HttpServer socket = HttpServer.GetServerStatic;

        Users user = new Users();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.IsTrue(user.Demo == 2);
        }
    }
}