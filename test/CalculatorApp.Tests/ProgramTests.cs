using NUnit.Framework;

[TestFixture]
public class Tests
{
    [TestCase("1,2,3", 6)]
    [TestCase("4,5,6", 15)]
    [TestCase("", 0)]
    [TestCase("1000", 1000)]
    [TestCase("1001", 0)]
    [TestCase("1000,2000", 1000)]
    [TestCase("1,2,3\n4,5", 15)]
    [TestCase("//;\n1;2;3", 6)]
    [TestCase("//[***]\n1***2***3", 6)]
    [TestCase("//[;][|]\n1;2|3", 6)]
    public void AddTest(string numbers, int expectedResult)
    {
        Assert.AreEqual(expectedResult, Program.Add(numbers));
    }

    [Test]
    public void AddTest_NegativeNumbers_ThrowsArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(() => Program.Add("1,-2,3"));
        Assert.That(ex.Message, Is.EqualTo("Negative numbers are not allowed: -2"));
    }

    [Test]
    public void AddTest_MultipleNegativeNumbers_ThrowsArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(() => Program.Add("1,-2,-3"));
        Assert.That(ex.Message, Is.EqualTo("Negative numbers are not allowed: -2, -3"));
    }

    [Test]
    public void AddTest_CustomDelimiterWithMultipleCharacters()
    {
        Assert.AreEqual(6, Program.Add("//[***]\n1***2***3"));
    }

    [Test]
    public void AddTest_CustomDelimiterWithMultipleDelimiters()
    {
        Assert.AreEqual(6, Program.Add("//[;][|]\n1;2|3"));
    }

    [Test]
    public void AddTest_CustomDelimiterNewlineWithValidNumbers()
    {
        Assert.AreEqual(6, Program.Add("//;\n1;2;3\n"));
    }

    [Test]
    public void AddTest_EmptyStringAfterDelimiter()
    {
        Assert.AreEqual(0, Program.Add("//;\n"));
    }

    [Test]
    public void AddTest_SingleDelimiter()
    {
        Assert.AreEqual(3, Program.Add("//;\n1;2"));
    }
}
