using System;
using Microsoft.Playwright;
using playwrightAdv.Hooks;
using TechTalk.SpecFlow;

namespace playwrightAdv.StepDefinitions
{
    [Binding]
    public class PageObjectModelStepDefinitions
    {
        private IPage _page;
        private readonly IBrowserContext _context;
        public PageObjectModelStepDefinitions(Hooks1 hooks)
        {
            _page = hooks.Page;
            _context = _page.Context;
        }
        [Given(@"the user goes to the website ""([^""]*)""")]
        public async Task GivenTheUserGoesToTheWebsite(string url)
        {
            await _page.GotoAsync(url);
        }

        [When(@"the user clicks on page object model")]
        public async Task WhenTheUserClicksOnPageObjectModel()
        {
            var popupTask = _context.WaitForPageAsync();
            await _page.Locator("a#page-object-model[href='Page-Object-Model/index.html']").ClickAsync();
            var newPage = await popupTask;
            await newPage.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            _page = newPage;
        }

        [Then(@"read the text in the first textbox")]
        public async Task ThenReadTheTextInTheFirstTextbox()
        {
            var message1 = await _page.Locator("div.caption p").Nth(0).InnerTextAsync();
            Console.WriteLine("The Text is :");
            Console.WriteLine(message1);
        }

        [Then(@"read the text in second textbox")]
        public async Task ThenReadTheTextInSecondTextbox()
        {
            var message2 = await _page.Locator("div.caption p").Nth(1).InnerTextAsync();
            Console.WriteLine("The Text is :");
            Console.WriteLine(message2);
        }

        [Then(@"read the text in third textbox")]
        public async Task ThenReadTheTextInThirdTextbox()
        {
            var message3 = await _page.Locator("div.caption p").Nth(2).InnerTextAsync();
            Console.WriteLine("The Text is :");
            Console.WriteLine(message3);
        }

        [Then(@"read the text in fourth textbox")]
        public async Task ThenReadTheTextInFourthTextbox()
        {
            var message4 = await _page.Locator("div.caption p").Nth(3).InnerTextAsync();
            Console.WriteLine("The Text is :");
            Console.WriteLine(message4);
        }
    }
}
