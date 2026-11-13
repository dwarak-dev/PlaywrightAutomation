
using Microsoft.Playwright;

namespace PlaywrightAutomation.Tests.Pages;

public class LoginPage
{
    private readonly IPage _page;

    public LoginPage(IPage page)
    {
        _page = page;
    }

    public async Task NavigateAsync()
    {
        await _page.GotoAsync("https://www.saucedemo.com/");
    }

    public async Task LoginAsync(string username, string password)
    {
        await _page.GetByPlaceholder("Username")
            .FillAsync(username);

        await _page.GetByPlaceholder("Password")
            .FillAsync(password);

        await _page.GetByRole(
            AriaRole.Button,
            new() { Name = "Login" }
        ).ClickAsync();
    }

    public ILocator ErrorMessage =>
        _page.GetByRole(AriaRole.Alert);
}
