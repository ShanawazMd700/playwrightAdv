using System;
using TechTalk.SpecFlow;
using Microsoft.Playwright;
using NUnit.Framework;
using playwrightAdv.Hooks;
using static System.Net.Mime.MediaTypeNames;

namespace playwrightAdv.StepDefinitions
{
    [Binding]
    public class LoginStepDefinitions
    {
        private IPage _page;
        private readonly IBrowserContext _context;
        public LoginStepDefinitions(Hooks1 hooks)
        {
            _page = hooks.Page;
            _context = _page.Context;
        }
        [Given(@"the user is on the webpage ""([^""]*)""")]
        public async Task GivenTheUserIsOnTheWebpage(string url)
        {
            await _page.GotoAsync(url);
        }

        [When(@"user clicks on login portal")]
        public async Task WhenUserClicksOnLoginPortal()
        {
            var popupTask = _context.WaitForPageAsync();
            await _page.Locator("a#login-portal").ClickAsync();
            var newPage = await popupTask;
            await newPage.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            _page = newPage;
        }

        [When(@"user enters the username ""([^""]*)""")]
        public async Task WhenUserEntersTheUsername(string uname)
        {
            await _page.Locator("#text").FillAsync(uname);
        }

        [When(@"user enters the password ""([^""]*)""")]
        public async Task WhenUserEntersThePassword(string pword)
        {
            await _page.Locator("#password").FillAsync(pword);
        }

        [Then(@"verify if failed")]
        public async Task ThenVerifyIfFailed()
        {
            string alertMessage = null;

            // Attach BEFORE triggering the alert
            _page.Dialog += async (_, dialog) =>
            {
                alertMessage = dialog.Message;
                await dialog.AcceptAsync();
            };

            // Trigger the alert (clicking the login button again, or whichever step causes it)
            await _page.ClickAsync("#login-button");

            // Optional: wait a bit to ensure alert gets caught
            await Task.Delay(1000);

            Assert.AreEqual("validation failed", alertMessage);
        }
    }
}
