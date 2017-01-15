@mam
Feature: MeasurementsAndManaging
	I want to manage my system

@issueCommand
Scenario Outline: Issue command
 Given I have entered <neededPower> into text box.
 When I press issueCommand button
 Then Generator's active power should change    
 Examples: 
 | neededPower |
 | 100         |
 | 50          |
 | 20          |
 | 5           |
 | 2           |

 @drawHistory
Scenario: Draw LoadForecast
 Given I have two consuptions in database
 When I press DrawHistory button
 Then Binding list for showing should not be empty

 @drawProduction
Scenario Outline: Draw ProductionHistory
 Given I have entered <numberOfDays> into text box predefined for that.
 When I press drawProductionHistory button
 Then Binding lsit for showing should not be empty   
 Examples: 
 | numberOfDays |
 | 1            |
 | 2            |
 | 3            |