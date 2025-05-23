using System;
using Microsoft.Playwright;
using TechTalk.SpecFlow;
using NUnit.Framework;
using playwrightAdv.Hooks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace playwrightAdv.StepDefinitions
{
    [Binding]
    public class ContactUsStepDefinitions
    {
        private IPage _page;
        private readonly IBrowserContext _context;


        public ContactUsStepDefinitions(Hooks1 hooks)
        {
            _page = hooks.Page;
            _context = _page.Context;
        }
        [Given(@"the user is on the page ""([^""]*)""")]
        public async Task GivenTheUserIsOnThePage(string url)
        {
            await _page.GotoAsync(url);
        }

        [When(@"the user clicks on Contact Us")]
        public async Task WhenTheUserClicksOnContactUs()
        {
            //var popupTask = _context.WaitForPageAsync();
            //await _page.Locator("#contact-us").ClickAsync();
            //var newPage = await popupTask;
           // await newPage.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
           // _page = newPage;
            var popupTask = _context.WaitForPageAsync();

            var contactUsLocator = _page.Locator("#contact-us");
            var boundingBox = await contactUsLocator.BoundingBoxAsync();

            if (boundingBox != null)
            {
                // Move mouse to the center of the element and click
                var centerX = boundingBox.X + boundingBox.Width / 2;
                var centerY = boundingBox.Y + boundingBox.Height / 2;       
                await _page.Mouse.MoveAsync(centerX, centerY);
                await _page.Mouse.ClickAsync(centerX, centerY);
            }
            else
            {
                throw new Exception("Could not find the bounding box for the Contact Us element.");
            }
            var newPage = await popupTask;
            await newPage.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            _page = newPage;
        }

        [Then(@"the user enters the first name ""([^""]*)""")]
        public async Task ThenTheUserEntersTheFirstName(string name)
        {
            Thread.Sleep(2000);
            //await page.Locator("//input[@name='first_name']").FillAsync(name);
            await _page.Locator("input[name='first_name']").FillAsync(name);
        }

        [Then(@"the user enters the last name ""([^""]*)""")]
        public async Task ThenTheUserEntersTheLastName(string lname)
        {
            await _page.Locator("input[name='last_name']").FillAsync(lname);
            // await page.FillAsync("input[name='last_name']", lname);
        }

        [Then(@"the user enters the email ""([^""]*)""")]
        public async Task ThenTheUserEntersTheEmail(string mail)
        {
            await _page.Locator("input[name='email']").FillAsync(mail);
            //await page.FillAsync("input[name='email']", mail);
        }
        [Then(@"the user enters the message ""([^""]*)""")]
        public async Task ThenTheUserEntersTheMessage(string message)
        {
            await _page.Locator("textarea[name='message']").FillAsync(message);
        }

        [When(@"the user clicks on submit")]
        public async Task WhenTheUserClicksOnSubmit()
        {
            await _page.Locator("input[type='submit']").ClickAsync();
        }

        [Then(@"verify if sumbitted")]
        public async Task ThenVerifyIfSumbitted()
        {
            await _page.WaitForSelectorAsync("h1");
            var message = await _page.Locator("h1").TextContentAsync();
            Assert.AreEqual("Thank You for your Message!", message);
        }
    }
}
