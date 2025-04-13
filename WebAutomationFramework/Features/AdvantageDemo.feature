@Retry
Feature: AdvantageDemo

Scenario: Check error messages for Username & Email when both are empty
	Given I navigate to the landing page of the app
	When I see the page is loaded
    And I click the user button to create new user
    And I dont enter anything to username and email fields
    Then I see the 'Username field is required' error message is displayed
    And I see the 'Email field is required' error message is displayed

Scenario: Check error message not displayed for Username & Email when they are filled
    Given I navigate to the landing page of the app
	When I see the page is loaded
    And I click the user button to create new user
    And I enter 'admin' to username field
    And I enter 'admin@gmail.com' to email field
    Then I dont see any error message for 'username' field
    And I dont see any error message for 'email' field