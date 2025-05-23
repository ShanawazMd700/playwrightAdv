Feature: login

A short summary of the feature

@tag1
Scenario: performing login
	Given the user is on the webpage "https://webdriveruniversity.com/index.html"
	When user clicks on login portal
	When user enters the username "random 1"
	When user enters the password "password"
	Then verify if failed
