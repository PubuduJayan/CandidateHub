using Core.Entities;

namespace Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var templte = new Template("TestTemplte", "This is a description", Guid.NewGuid());
            templte.Title = "Test";
            Assert.Equal("Test", templte.Title);
        }
    }
}