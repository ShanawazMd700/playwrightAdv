using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using playwrightAdv.Hooks;
using TechTalk.SpecFlow;

namespace playwrightAdv.StepDefinitions
{
    [Binding]
    public class ToDoStepDefinitions
    {
        private string todo1;
        private string todo2;
        private string todo3;
        private IPage _page;
        private readonly IBrowserContext _context;
        public ToDoStepDefinitions(Hooks1 hooks)
        {
            _page = hooks.Page;
            _context = _page.Context;
        }
        [Given(@"the user goes to website""([^""]*)""")]
        public async Task GivenTheUserGoesToWebsite(string url)
        {
            await _page.GotoAsync(url);
        }

        [When(@"the user clicks on To Do")]
        public async Task WhenTheUserClicksOnToDo()
        {
            var popupTask = _context.WaitForPageAsync();
            await _page.Locator("a#to-do-list").ClickAsync();
            var newPage = await popupTask;
            await newPage.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            _page = newPage;
        }

        [When(@"the user enters a value in to do ""([^""]*)""")]
        public async Task WhenTheUserEntersAValueInToDo(string task1)
        {
            todo1 = task1;
            await _page.Locator("input[placeholder='Add new todo']").FillAsync(task1);
            await _page.Keyboard.PressAsync("Enter");
        }
        [When(@"The user enters a value in to do ""([^""]*)""")]
        public async Task WhenTheUserEntersAValueInToDo1(string task2)
        {
            todo2 = task2;
            await _page.Locator("input[placeholder='Add new todo']").FillAsync(task2);
            await _page.Keyboard.PressAsync("Enter");
        }
        [When(@"the User enters a value in to do ""([^""]*)""")]
        public async Task WhenTheUserEntersAValueInToDo2(string task3)
        {
            todo3 = task3;
            await _page.Locator("input[placeholder='Add new todo']").FillAsync(task3);
            await _page.Keyboard.PressAsync("Enter");
        }
        [Then(@"the user deletes the value in to do ""([^""]*)""")]
        public async Task ThenTheUserDeletesTheValueInToDo(string task4)
        {

            // Locate the <li> with the text (task4)
            var liLocator = _page.Locator("li", new PageLocatorOptions { HasTextString = task4 });

            //// Wait until it's visible
            await liLocator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            //Hover on the <li>
            await liLocator.HoverAsync();

            // Now locate the delete icon (assuming it's inside the <li>)
            var deleteIcon = liLocator.Locator("span"); // Adjust this selector if needed

            // Wait for it to be visible too
            await deleteIcon.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });

            // Click the delete icon
            await deleteIcon.ClickAsync();
        }
    }
}
