using NUnit.Framework;

[TestFixture]
public class Tests
{
    [TestCase("1,2,3", 6)]
    [TestCase("4,5,6", 15)]
    [TestCase("", 0)]
    public void AddTest(string numbers, int expectedResult)
    {
        Assert.AreEqual(expectedResult, Program.Add(numbers));
    }
}