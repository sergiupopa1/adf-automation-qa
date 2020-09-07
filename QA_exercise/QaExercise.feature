Feature: QaExercise
	
Scenario: Log in and create new folder
	Given I am on home page
	And I set Provider to 'ECM'
	When I click Apply
	Then I navigate to login page
	When I insert Username and Password
	And I click Login
	Then I navigate to files page
	When I click on Create New Folder
	Then New folder dialog is displayed
	When I introduce my username
	Then Name has been added
	When I click on create button
	Then The dialog is closed
	And Folder with username is created in current folder
	When I click on Create New Folder
	Then New folder dialog is displayed
	When I introduce my username
	Then Name has been added
	When I click on create button	
	Then The dialog is not closed
	And Message 'There's already a folder with this name. Try a different name.' is displayed	
	Then Select the folder with username
	Then Open Options window
	And Click Delete