namespace $safeprojectname$
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            string actual = "ABCDEFGHI";
            actual.Should().StartWith("AB").And.EndWith("HI").And.Contain("EF").And.HaveLength(9);
        }
    }
}
