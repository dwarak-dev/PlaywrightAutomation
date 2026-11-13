
using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightAutomation.Tests;

public class LoginTests : PageTest
{
    [Test]
    public async Task ValidUserCanLoginSuccessfully()
    {
        // Step 1: Open the login page
        await Page.GotoAsync("https://www.saucedemo.com/");

        // Step 2: Enter the username
        await Page.GetByPlaceholder("Username")
            .FillAsync("standard_user");

        // Step 3: Enter the password
        await Page.GetByPlaceholder("Password")
            .FillAsync("secret_sauce");

        // Step 4: Click Login
        await Page.GetByRole(
            AriaRole.Button,
            new() { Name = "Login" }
        ).ClickAsync();

        // Step 5: Verify successful navigation
        await Expect(Page).ToHaveURLAsync(
            new Regex(".*/inventory\\.html")
        );

        // Step 6: Verify the Products heading
        await Expect(
            Page.GetByText("Products", new() { Exact = true })
        ).ToBeVisibleAsync();
    }


[Test]
public async Task InvalidUserCannotLogin()
{
    // Step 1: Open the login page
    await Page.GotoAsync("https://www.saucedemo.com/");

    // Step 2: Enter invalid credentials
    await Page.GetByPlaceholder("Username")
        .FillAsync("invalid_user");

    await Page.GetByPlaceholder("Password")
        .FillAsync("wrong_password");

    // Step 3: Click Login
    await Page.GetByRole(
        AriaRole.Button,
        new() { Name = "Login" }
    ).ClickAsync();

    // Step 4: Verify the error message
    await Expect(
        Page.GetByRole(AriaRole.Alert)
    ).ToContainTextAsync(
        "Username and password do not match"
    );
}



}
