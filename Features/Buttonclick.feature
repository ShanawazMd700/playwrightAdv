Feature: Buttonclick

A short summary of the feature

@tag1
Scenario: ButtonClicks of various buttons
	Given the user visits the webpage "https://webdriveruniversity.com/index.html" 
	When the user clicks on buttonclick
	When the user clicks on first button
	And get the text in alert box
	When the user clicks on second button
	And get the Text in alert box
	When the user clicks on third button
	And Get the text in alert box

