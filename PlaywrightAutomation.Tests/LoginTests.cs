
using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightAutomation.Tests.Pages;

namespace PlaywrightAutomation.Tests;

public class LoginTests : PageTest
{
    [Test]
    public async Task ValidUserCanLoginSuccessfully()
    {
        var loginPage = new LoginPage(Page);

        await loginPage.NavigateAsync();
        await loginPage.LoginAsync("standard_user", "secret_sauce");

        await Expect(Page).ToHaveURLAsync(
            new Regex(".*/inventory\\.html")
        );

        await Expect(Page.GetByText("Products", new() { Exact = true }))
            .ToBeVisibleAsync();
    }

    [Test]
    public async Task InvalidUserCannotLogin()
    {
        var loginPage = new LoginPage(Page);

        await loginPage.NavigateAsync();
        await loginPage.LoginAsync("invalid_user", "wrong_password");

        await Expect(loginPage.ErrorMessage)
            .ToContainTextAsync("Username and password do not match");
    }
}
