using System;
using Microsoft.Playwright;
using playwrightAdv.Hooks;
using TechTalk.SpecFlow;

namespace playwrightAdv.StepDefinitions
{
    [Binding]
    public class DropStepDefinitions
    {
        private IPage _page;
        private readonly IBrowserContext _context;
        public DropStepDefinitions(Hooks1 hooks)
        {
            _page = hooks.Page;
            _context = _page.Context;
        }
        [Given(@"user goes to the website ""([^""]*)""")]
        public async Task GivenUserGoesToTheWebsite(string url)
        {
            await _page.GotoAsync(url);
        }

        [When(@"user clicks on dropdown-checkboxes-radiobuttons")]
        public async Task WhenUserClicksOnDropdown_Checkboxes_Radiobuttons()
        {
            var popupTask = _context.WaitForPageAsync();
            await _page.Locator("a#dropdown-checkboxes-radiobuttons").ClickAsync();
            var newPage = await popupTask;
            await newPage.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            _page = newPage;
        }

        [When(@"user selects the options ""([^""]*)"", ""([^""]*)"", and ""([^""]*)""")]
        public async Task WhenUserSelectsTheOptionsAnd(string option1, string option2, string option3)
        {
            await SelectDropdownOption("#dropdowm-menu-1", option1);
            await SelectDropdownOption("#dropdowm-menu-2", option2);
            await SelectDropdownOption("#dropdowm-menu-3", option3);
        }
        private async Task SelectDropdownOption(string dropdownSelector, string optionText)
        {
            var options = await _page.QuerySelectorAllAsync($"{dropdownSelector} option");

            foreach (var option in options)
            {
                var text = await option.InnerTextAsync();
                if (string.Equals(text, optionText, StringComparison.OrdinalIgnoreCase))
                {
                    var value = await option.GetAttributeAsync("value");
                    await _page.SelectOptionAsync(dropdownSelector, new[] { value });
                    return;
                }
            }

            throw new Exception($"Option '{optionText}' not found in dropdown '{dropdownSelector}'.");
        }

        [Then(@"the selected options should be ""([^""]*)"", ""([^""]*)"", and ""([^""]*)""")]
        public async Task ThenTheSelectedOptionsShouldBeAnd(string expected1, string expected2, string expected3)
        {
            await verifyOption("#dropdowm-menu-1", expected1);
            await verifyOption("#dropdowm-menu-2", expected2);
            await verifyOption("#dropdowm-menu-3", expected3);
        }

        private async Task verifyOption(string dropdownSelector, string expectedText)
        {
            var selectedValue = await _page.InputValueAsync(dropdownSelector);
            var selectedOption = await _page.QuerySelectorAsync($"{dropdownSelector} option[value='{selectedValue}']");
            var selectedText = await selectedOption.InnerTextAsync();

            if (!string.Equals(selectedText, expectedText, StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception($"Expected selected option '{expectedText}' but found '{selectedText}' in dropdown '{dropdownSelector}'.");
            }
        }
    }
}
