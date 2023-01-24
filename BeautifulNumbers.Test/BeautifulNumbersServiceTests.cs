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
    
    public static IEnumerable<TestCaseData> GetLuckyTicketCountCases()
    {
        yield return new TestCaseData(1, 2, 2); // 00, 11
        yield return new TestCaseData(1, 10, 10); // 00 11 22 and etc
        yield return new TestCaseData(2, 3, 19); // 0000, 0101, 0211, ..., 2222
    }
    
    [TestCaseSource(nameof(GetLuckyTicketCountCases))]
    public void GetLuckyTicketCountTest(int digitsCount, int numeralSystem, int expired)
    {
        var beautifulNumbersService = new BeautifulNumbersService();
        var count = beautifulNumbersService.GetLuckyTicketCount(digitsCount, numeralSystem);
        count.Should().Be(expired);
    }
}