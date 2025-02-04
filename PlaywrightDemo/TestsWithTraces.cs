using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class TestsWithTraces : PageTest
{
    [SetUp]
    public async Task Setup()
    {
        await Context.Tracing.StartAsync(new()
        {
            Title = $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}",
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

        await Page.GotoAsync("https://playwright.dev");
    }

    [TearDown]
    public async Task TearDown()
    {
        await Context.Tracing.StopAsync(new()
        {
            Path = Path.Combine(
                TestContext.CurrentContext.WorkDirectory,
                "playwright-traces",
                $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip"
            )
        });
    }

    [Test]
    public async Task GetStartedLink()
    {
        // Click the get started link.
        await Page.GetByRole(AriaRole.Link, new() { Name = "Get started" }).ClickAsync();

        // Expects page to have a heading with the name of Installation.
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Installation" })).ToBeVisibleAsync();
    }

    [Test]
    public async Task LearnVideosLink()
    {
        await Page.Locator("footer").ScrollIntoViewIfNeededAsync();

        await Page.GetByRole(AriaRole.Link, new() { Name = "Learn Videos" }).ClickAsync();

        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Learn Videos" })).ToBeVisibleAsync();
    }

    [Test]
    public async Task FeatureVideosCorrectLink()
    {
        await Page.Locator("footer").ScrollIntoViewIfNeededAsync();

        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Feature Videos" })).ToHaveAttributeAsync("href", "/community/feature-videos");
    }
}