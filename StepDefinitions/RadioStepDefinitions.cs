using System;
using Microsoft.Playwright;
using playwrightAdv.Hooks;
using TechTalk.SpecFlow;

namespace playwrightAdv.StepDefinitions
{
    [Binding]
    public class RadioStepDefinitions
    {
        private IPage _page;
        private readonly IBrowserContext _context;
        private string _selectedOption;

        public RadioStepDefinitions(Hooks1 hooks)
        {
            _page = hooks.Page;
            _context = _page.Context;
        }
        [Given(@"user goes to website ""([^""]*)""")]
        public async Task GivenUserGoesToWebsite(string url)
        {
            await _page.GotoAsync(url);
        }

        [When(@"user clicks dropdown checkboxes radiobutton")]
        public async Task WhenUserClicksDropdownCheckboxesRadiobutton()
        {
            var popupTask = _context.WaitForPageAsync();
            await _page.Locator("a#dropdown-checkboxes-radiobuttons").ClickAsync();
            var newPage = await popupTask;
            await newPage.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            _page = newPage;
        }

        [When(@"user selects option ""([^""]*)"" from the radiobutton")]
        public async Task WhenUserSelectsOptionFromTheRadiobutton(string target)
        {
            var radioButtons = await _page.QuerySelectorAllAsync("#radio-buttons input[type='radio']");

            foreach (var radio in radioButtons)
            {
                var value = await radio.GetAttributeAsync("value");

                if (string.Equals(value, target.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    await radio.CheckAsync(); // select the radio button
                    _selectedOption = value; // store it for verification
                    return;
                }
            }

            throw new Exception($"Radio button with value '{target}' not found.");
        }

        [Then(@"check if ""([^""]*)"" selected")]
        public async Task ThenCheckIfSelected(string expected)
        {
            var checkedRadio = await _page.QuerySelectorAsync("#radio-buttons input[type='radio']:checked");
            var selectedValue = await checkedRadio.GetAttributeAsync("value");

            if (!string.Equals(selectedValue, expected.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception($"Expected radio selection: '{expected}', but found '{selectedValue}'");
            }
        }

    }
}
