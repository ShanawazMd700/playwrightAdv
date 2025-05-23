using System;
using TechTalk.SpecFlow;
using Microsoft.Playwright;
using NUnit.Framework;
using playwrightAdv.Hooks;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace playwrightAdv.StepDefinitions
{
    [Binding]
    public class ButtonclickStepDefinitions
    {
        private IPage _page;
        private readonly IBrowserContext _context;
        public ButtonclickStepDefinitions(Hooks1 hooks)
        {
            _page = hooks.Page;
            _context = _page.Context;
        }
        [Given(@"the user visits the webpage ""([^""]*)""")]
        public async Task GivenTheUserVisitsTheWebpage(string url)
        {
            await _page.GotoAsync(url);
        }
        [When(@"the user clicks on buttonclick")]
        public async Task WhenTheUserClicksOnButtonclick()
        {
            var popupTask = _context.WaitForPageAsync();
            await _page.Locator("a#button-clicks").ClickAsync();
            var newPage = await popupTask;
            await newPage.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            _page = newPage;
        }
        [When(@"the user clicks on first button")]
        public async Task WhenTheUserClicksOnFirstButton()
        {
            await _page.Locator("span#button1").ClickAsync();
        }
        [When(@"get the text in alert box")]
        public async Task WhenGetTheTextInAlertBox()
        {
            var message1 = await _page.Locator("div.modal-body p").Nth(0).InnerTextAsync();
            //await _page.Locator("div.modal-footer >> button", new() { HasTextString = "Close" }).ClickAsync();
            await _page.Locator("div.modal.show div.modal-footer >> text=Close").ClickAsync();
            Console.WriteLine(message1);
        }
        [When(@"the user clicks on second button")]
        public async Task WhenTheUserClicksOnSecondButton()
        {
            await _page.Locator("span#button2").ClickAsync();
        }
        [When(@"get the Text in alert box")]
        public async Task WhenGetTheTextInAlertBox2()
        {
            var message2 = await _page.Locator("div.modal-body p").Nth(1).InnerTextAsync();
            await _page.Locator("div.modal.show div.modal-footer >> text=Close").ClickAsync();
            Console.WriteLine(message2);
        }
        [When(@"the user clicks on third button")]
        public async Task WhenTheUserClicksOnThirdButton()
        {
            await _page.Locator("span#button3").ClickAsync();
        }
        [When(@"Get the text in alert box")]
        public async Task WhenGetTheTextInAlertBox3()
        {
            var message3 = await _page.Locator("div.modal-body p").Nth(2).InnerTextAsync();
            await _page.Locator("div.modal.show div.modal-footer >> text=Close").ClickAsync();
            Console.WriteLine(message3);
        }

    }
}
