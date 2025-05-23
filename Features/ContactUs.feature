Feature: ContactUs
![Calculator](https://specflow.org/wp-content/uploads/2020/09/calculator.png)
Simple calculator for adding **two** numbers

Link to a feature: [Calculator](playwrightAdv/Features/Calculator.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

@mytag
Scenario: Filling contact us
	Given the user is on the page "https://webdriveruniversity.com/index.html"
	When the user clicks on Contact Us
	Then the user enters the first name "Shanawaz"
	Then the user enters the last name "Md"
	Then the user enters the email "shanawaz@gmail.com"
	Then the user enters the message " This is a test message"
	When the user clicks on submit
	Then verify if sumbitted