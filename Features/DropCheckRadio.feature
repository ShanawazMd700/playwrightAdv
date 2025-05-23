Feature: DropCheckRadio

A short summary of the feature

@tag1
Scenario: Selecting options in dropdown
	Given user goes to the website "https://webdriveruniversity.com/index.html"
	When user clicks on dropdown-checkboxes-radiobuttons
	And user selects the options "java", "maven", and "javascript"
    Then the selected options should be "java", "maven", and "javascript"


Scenario: Selecting the options in Checkbox
	Given the user goes to website "https://webdriveruniversity.com/index.html"
	When user clicks on dropdown checkboxes radiobuttons
	When the user selects options "option 1" " option 2" from the checkbox
	Then verify if checked

Scenario: Selecting the option in Radio Button
	Given user goes to website "https://webdriveruniversity.com/index.html"
	When user clicks dropdown checkboxes radiobutton
	When user selects option "green" from the radiobutton
	Then check if "green" selected


	Scenario: Select the option in Radio Button
	Given user goes to website "https://webdriveruniversity.com/index.html"
	When user clicks dropdown checkboxes radiobutton
	When user selects option "green" from the radiobutton
	Then check if "green" selected

	Scenario: Select multiple options in Radio Button and verify selection
  Given user going to website "https://webdriveruniversity.com/index.html"
  When user click dropdown checkboxes radiobutton
  And User select option "green" from radiobutton
  Then The selected radiobutton should show "green"
  When User select the option "yellow" from the radiobutton
  Then The selected radiobutton should visible "yellow"
	