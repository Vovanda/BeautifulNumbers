using System.Numerics;
using BeautifulNumbers.Service;
using FluentAssertions;
using NUnit.Framework;

namespace BeautifulNumbers.Test;

public class BeautifulNumbersServiceTests
{
    [SetUp]
    public void Setup()
    { }
    
    public static IEnumerable<TestCaseData> GetCompositionCountCases()
    {
        yield return new TestCaseData(1, 3, 2, 3);
        yield return new TestCaseData(5, 3, 3, 3);
        yield return new TestCaseData(3, 3, 3, 7);
        yield return new TestCaseData(4, 4, 3, 19);
        yield return new TestCaseData(1, 6, 13, 6);
        yield return new TestCaseData(72, 6, 13, 1);
        yield return new TestCaseData(71, 6, 13, 6);
    }
    
    [TestCaseSource(nameof(GetCompositionCountCases))]
    public void GetCompositionCountTest(int number, int digitsCount, int numeralSystem, int expired)
    {
        var beautifulNumbersService = new BeautifulNumbersService();
        var count = beautifulNumbersService.GetCompositionCount(number, digitsCount, numeralSystem);
        count.Should().Be(expired);
    }
    
    public static IEnumerable<TestCaseData> GetBeautifulNumbersCountCases()
    {
        yield return new TestCaseData(1, 2, 4); //000, 010, 101, 111
        yield return new TestCaseData(2, 3, 57); //01X01, 01X10 and etc; 12X12, 12X21 and etc; 11X02, 11X02 and etc; 00X00 and etc; 11X11 and etc; 22X22 and etc
    }
    
    [TestCaseSource(nameof(GetBeautifulNumbersCountCases))]
    public void GetBeautifulNumbersCountTest(int digitsCount, int numeralSystem, int expired)
    {
        var beautifulNumbersService = new BeautifulNumbersService();
        var count = beautifulNumbersService.GetBeautifulNumbersCount(digitsCount, numeralSystem);
        count.Should().Be(expired);
    }
}