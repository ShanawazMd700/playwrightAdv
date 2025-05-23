using System;
using System.Security.Policy;
using Microsoft.Playwright;
using playwrightAdv.Hooks;
using TechTalk.SpecFlow;

namespace playwrightAdv.StepDefinitions
{
    [Binding]
    public class AccordionsStepDefinitions
    {
        private IPage _page;
        private readonly IBrowserContext _context;
        public AccordionsStepDefinitions(Hooks1 hooks)
        {
            _page = hooks.Page;
            _context = _page.Context;
        }
        [Given(@"The user visits the website""([^""]*)""")]
        public async Task GivenTheUserVisitsTheWebsite(string url)
        {
            await _page.GotoAsync(url);
        }

        [When(@"the user clicks accordions")]
        public async Task WhenTheUserClicksAccordions()
        {
            var popupTask = _context.WaitForPageAsync();
            await _page.Locator("a#page-object-model[href='Accordion/index.html']").ClickAsync();
            var newPage = await popupTask;
            await newPage.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            _page = newPage;
        }

        [Then(@"click and read the text in manual accordion")]
        public async Task ThenClickAndReadTheTextInManualAccordion()
        {
            await _page.Locator("button#manual-testing-accordion").ClickAsync();
            var paragraphText = await _page.Locator("#manual-testing-description > p").InnerTextAsync();
            Console.WriteLine("Manual Testing Description:");
            Console.WriteLine(paragraphText);
        }

        [Then(@"click and read the text in cucumber accordion")]
        public async Task ThenClickAndReadTheTextInCucumberAccordion()
        {
            await _page.Locator("button#cucumber-accordion").ClickAsync();
            var paragraphText = await _page.Locator("#cucumber-testing-description > p").InnerTextAsync();
            Console.WriteLine("Cucumber Testing Description:");
            Console.WriteLine(paragraphText);
        }

        [Then(@"click and read the text in automation accordion")]
        public async Task ThenClickAndReadTheTextInAutomationAccordion()
        {
            await _page.Locator("button#automation-accordion").ClickAsync();
            var paragraphText = await _page.Locator("#automation-testing-description > p").InnerTextAsync();
            Console.WriteLine("Automation Testing Description:");
            Console.WriteLine(paragraphText);
        }

        [Then(@"click and read the text in last accordion")]
        public async Task ThenClickAndReadTheTextInLastAccordion()
        {
            await _page.Locator("#hidden-text").WaitForAsync(new()
            {
                State = WaitForSelectorState.Visible
            });

            await _page.WaitForFunctionAsync(
                @"() => document.querySelector('#hidden-text').innerText === 'LOADING COMPLETE.'"
            );

            // Click the accordion button
            await _page.Locator("button#click-accordion").ClickAsync();
            var timeoutText = await _page.Locator("#timeout").InnerTextAsync();
            Console.WriteLine("Timeout Text:");
            Console.WriteLine(timeoutText);
        }
    }
}
