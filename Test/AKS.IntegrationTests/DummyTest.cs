namespace AKS.IntegrationTests;

public class DummyTest
{
    [Test]
    public async Task RunTest()
    {
        var a = 5;
        var b = 10;
        var sum = a + b;
        await Assert.That(sum).IsEqualTo(15);
    }
}