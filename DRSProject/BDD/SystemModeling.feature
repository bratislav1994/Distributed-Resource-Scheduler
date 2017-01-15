@sysMod
Feature: SystemModeling
 I want to manage my system

@base2
Scenario Outline: Registration
 Given I have entered <user> into text box
 And I have entered <pass> into password box
 When I press register button
 Then I should be registered on system   
 Examples: 
 | user  | pass  |
 | test1 | test1 |

@base2
 Scenario Outline: Registration failed
 Given I have entered already existing <user> into text box.
 And I have entered <pass> into password box
 When I press register button
 Then I should not be registered on system   
 Examples: 
 | user  | pass  |
 | proba | proba |

 @base2
 Scenario Outline: Login
 Given I have entered registered <user1> into text box.
 And I have entered <pass1> into password box.
 When I press login button
 Then I should be login on system   
 Examples: 
 | user1  | pass1  |
 | proba  | proba  |

 @base2
 Scenario Outline: Login failed
 Given I have entered not existing <user1> into text box
 And I have entered <pass1> into password box.
 When I press login button
 Then I should not be login on system
 Examples: 
 | user1  | pass1  |
 | pera   | pera   |

 @base1
 Scenario: Add generator in new site and new group
 Given I have entered name into text box.
 And I have entered activePower into text box.
 And I have choose hasMeas from combo box
 And I have choose workingMode from combo box
 And I have entered pMin into text box.
 And I have entered pMax into text box.
 And I have entered price into text box.
 And I have choose genType from combo box
 And I have checked radioButton from input form
 And I have entered siteName into text box.
 And I have entered groupName into text box.
 When I press add button
 Then generator should be added

  @base1
 Scenario: Add generator in existing site and existing group
 Given I have entered name into text box.
 And I have entered activePower into text box.
 And I have choose hasMeas from combo box
 And I have choose workingMode from combo box
 And I have entered pMin into text box.
 And I have entered pMax into text box.
 And I have entered price into text box.
 And I have choose genType from combo box
 And I have checked radioButtonn from input form
 And I have choose groupName from text box.
 And I have choose siteName from combo box.
 When existing groups and sites
 And I press add button
 Then generator should be added

 @base1
 Scenario: Add generator in existing site and new group
 Given I have entered name into text box.
 And I have entered activePower into text box.
 And I have choose hasMeas from combo box
 And I have choose workingMode from combo box
 And I have entered pMin into text box.
 And I have entered pMax into text box.
 And I have entered price into text box.
 And I have choose genType from combo box
 And I have checked radioButtonnn from input form
 And I have choose cmbSiteName from combo box.
 And I have choose txbGroupName from text box.
 When existing sites and new group
 And I press add button
 Then generator should be added

 @base1
 Scenario: Not all input form are passed validation for Add generator
 Given I have entered activePower into text box.
 And I have choose hasMeas from combo box
 And I have choose workingMode from combo box
 And I have entered pMin into text box.
 And I have entered pMax into text box.
 And I have entered price into text box.
 And I have choose genType from combo box
 And I have checked radioButton from input form
 And I have entered siteName into text box.
 And I have entered groupName into text box.
 When I have entered empty name into text box.
 Then create button should be disabled

 @base1
 Scenario: Remove generator
 Given I have selected generator from table
 When I have pressed remove button
 Then generator should be deleted

@base1
 Scenario: Not all edit form are passed validation for Edit generator
 Given I have filled edit form
 When I have entered empty Editname into text box.
 Then edit button should be disabled

 @base1
 Scenario: Not selected generator from table
 Given I have table with at least one generator
 When generator not selected 
 Then edit button, remove button, show data button should be disabled

 @base1
 Scenario: Edit generator in new site and new group
 Given I have filled edit form.
 And I have checked radioButton from edit form
 And I have entered editSiteName into text box..
 And I have entered editGroupName into text box..
 When I pressed edit button
 Then generator should be edited