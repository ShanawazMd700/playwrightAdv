Feature: ToDo

A short summary of the feature

@tag1
Scenario: Performing To Do list operations
	Given the user goes to website"https://webdriveruniversity.com/index.html"
	When the user clicks on To Do
	When the user enters a value in to do "GO Shopping"
	When The user enters a value in to do "Go Swimming"
	When the User enters a value in to do "Performing dance"
	Then the user deletes the value in to do "Go Swimming"
