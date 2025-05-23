using System;
using Microsoft.Playwright;
using Newtonsoft.Json.Linq;
using playwrightAdv.Hooks;
using TechTalk.SpecFlow;

namespace playwrightAdv.StepDefinitions
{
    [Binding]
    public class CheckStepDefinitions
    {
        private IPage _page;
        private readonly IBrowserContext _context;
        private readonly List<string> _selectedCheckboxes = new();
        public CheckStepDefinitions(Hooks1 hooks)
        {
            _page = hooks.Page;
            _context = _page.Context;
        }
        [Given(@"the user goes to website ""([^""]*)""")]
        public async Task GivenTheUserGoesToWebsite(string url)
        {
            await _page.GotoAsync(url);
        }

        [When(@"user clicks on dropdown checkboxes radiobuttons")]
        public async Task WhenUserClicksOnDropdownCheckboxesRadiobuttons()
        {
            var popupTask = _context.WaitForPageAsync();
            await _page.Locator("a#dropdown-checkboxes-radiobuttons").ClickAsync();
            var newPage = await popupTask;
            await newPage.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            _page = newPage;
        }
        
        [When(@"the user selects options ""([^""]*)"" ""([^""]*)"" from the checkbox")]
        public async Task WhenTheUserSelectsOptionsFromTheCheckbox(string value1, string value2)
        {
            await SelectCheckboxByLabelText(value1);
            await SelectCheckboxByLabelText(value2);
        }
        private async Task SelectCheckboxByLabelText(string target)    
        {
            var labels = await _page.QuerySelectorAllAsync("#checkboxes label");

            foreach (var label in labels)
            {
                var labelText = await label.InnerTextAsync();
                if (string.Equals(labelText.Trim(), target.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    var checkbox = await label.QuerySelectorAsync("input[type='checkbox']");
                    var isChecked = await checkbox.IsCheckedAsync();

                    if (!isChecked)
                        await checkbox.CheckAsync();

                    _selectedCheckboxes.Add(labelText.Trim());
                    return;
                }
            }

            throw new Exception($"Checkbox with label '{target}' not found.");
        }

        [Then(@"verify if checked")]
        public async Task ThenVerifyIfChecked()
        {
            foreach (var labelText in _selectedCheckboxes)
            {
                var labels = await _page.QuerySelectorAllAsync("#checkboxes label");

                var found = false;
                foreach (var label in labels)
                {
                    var text = await label.InnerTextAsync();
                    if (string.Equals(text.Trim(), labelText, StringComparison.OrdinalIgnoreCase))
                    {
                        var checkbox = await label.QuerySelectorAsync("input[type='checkbox']");
                        var isChecked = await checkbox.IsCheckedAsync();

                        if (!isChecked)
                            throw new Exception($"Checkbox '{labelText}' was expected to be checked but wasn't.");

                        found = true;
                        break;
                    }
                }

                if (!found)
                    throw new Exception($"Checkbox label '{labelText}' not found during verification.");
            }
        }
    }
}
