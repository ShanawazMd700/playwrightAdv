Feature: PageObjectModel

A short summary of the feature

@tag1
Scenario: Reading Text in Page Object Model page
	Given the user goes to the website "https://webdriveruniversity.com/index.html"
	When the user clicks on page object model
	Then read the text in the first textbox
	Then read the text in second textbox
	Then read the text in third textbox
	Then read the text in fourth textbox

Scenario: Performing operations in Accordions
	Given The user visits the website"https://webdriveruniversity.com/index.html"
	When the user clicks accordions
	Then click and read the text in manual accordion
	Then click and read the text in cucumber accordion
	Then click and read the text in automation accordion
	Then click and read the text in last accordion
