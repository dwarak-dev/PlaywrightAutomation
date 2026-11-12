
using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightAutomation.Tests;

public class FirstBrowserTest : PageTest
{
    [Test]
    public async Task VerifyPlaywrightWebsiteTitle()
    {
        await Page.GotoAsync("https://playwright.dev/");

        await Expect(Page).ToHaveTitleAsync(
            new Regex("Playwright")
        );
    }

    [Test]
    public async Task VerifyGetStartedNavigation()
    {
        // Step 1: Navigate to the homepage
        await Page.GotoAsync("https://playwright.dev/");

        // Step 2: Find and click the Get Started link
        await Page.GetByRole(
            AriaRole.Link,
            new() { Name = "Get started" }
        ).ClickAsync();

        // Step 3: Verify navigation to documentation
        await Expect(Page).ToHaveURLAsync(
            new Regex(".*/docs/intro")
        );
    }
}
