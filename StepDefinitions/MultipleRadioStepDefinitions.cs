using System;
using System.Security.Policy;
using Microsoft.Playwright;
using playwrightAdv.Hooks;
using TechTalk.SpecFlow;

namespace playwrightAdv.StepDefinitions
{
    [Binding]
    public class MultipleRadioStepDefinitions
    {
        private IPage _page;
        private readonly IBrowserContext _context;
        private string _selectedOption;
        public MultipleRadioStepDefinitions(Hooks1 hooks)
        {
            _page = hooks.Page;
            _context = _page.Context;
        }
        [Given(@"user going to website ""([^""]*)""")]
        public async Task GivenUserGoingToWebsite(string url)
        {
            await _page.GotoAsync(url);
        }

        [When(@"user click dropdown checkboxes radiobutton")]
        public async Task WhenUserClickDropdownCheckboxesRadiobutton()
        {
            var popupTask = _context.WaitForPageAsync();
            await _page.Locator("a#dropdown-checkboxes-radiobuttons").ClickAsync();
            var newPage = await popupTask;
            await newPage.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            _page = newPage;
        }

        [When(@"User select option ""([^""]*)"" from radiobutton")]
        public async Task WhenUserSelectOptionFromTheRadiobutton(string green)
        {
            await selectRadiobutton(green);
        }
        private async Task selectRadiobutton(string target)
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

        [Then(@"The selected radiobutton should show ""([^""]*)""")]
        public async Task ThenTheSelectedRadiobuttonShouldBe(string green)
        {
            await CheckIfSelected(green);
        }
        private async Task CheckIfSelected(string expected)
        {
            var checkedRadio = await _page.QuerySelectorAsync("#radio-buttons input[type='radio']:checked");
            var selectedValue = await checkedRadio.GetAttributeAsync("value");

            if (!string.Equals(selectedValue, expected.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception($"Expected radio selection: '{expected}', but found '{selectedValue}'");
            }
        }

        [When(@"User select the option ""([^""]*)"" from the radiobutton")]
        public async Task WhenUserSelectOptionFromTheRadiobutton1(string yellow)
        {
            await selectRadiobutton(yellow);
        }

        [Then(@"The selected radiobutton should visible ""([^""]*)""")]
        public async Task ThenTheSelectedRadiobuttonShouldBe1(string yellow)
        {
            await CheckIfSelected(yellow);
        }
    }
}
