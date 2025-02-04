using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System.Text.RegularExpressions;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class ExampleTest : PageTest
{
    [SetUp]
    public async Task Setup()
    {
        await Page.GotoAsync("https://playwright.dev");
    }

    [Test]
    public async Task HasTitle()
    {
        // Expect a title "to contain" a substring.
        await Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));
    }

    [Test]
    public async Task GetStartedLink()
    {
        await Page.GetByRole(AriaRole.Link, new() { Name = "Get started" }).ClickAsync();

        // Expects page to have a heading with the name of Installation.
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Installation" })).ToBeVisibleAsync();
    }

    [Test]
    public async Task ScreenshotTest()
    {
        await Page.ScreenshotAsync(new()
        {
            Path = $"screenshots/{nameof(ScreenshotTest)}-{Path.GetRandomFileName()}.png",
            FullPage = true,
        });
    }
}