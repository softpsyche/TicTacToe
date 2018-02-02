@BetterTestingApproach
@Unit
Feature: DatabaseBuilder
	Verify building game database works correctly

Background: 
	Given I have a container
	Given I mock the ILiteDatabase
	Given I have a tictactoe factory

Scenario: Database builder should populate move responses
	Given I start a new game in the following state
		| A | B | C |
		| X |   | O |
		| O | X |   |
		| X |   | O |
	Given I have a database builder
	When I populate move responses for the current game
	Then I expect the ILiteDatabase.DeleteCollection to have been called '1' times
	Then I expect the ILiteDatabase.EnsureIndex to have been called with the following 'Id'
	Then I expect the ILiteDatabase.InsertBulk to have been called with the following move response records
		| Board     | Player | Response | Outcome | Id                 |
		| XXOOX_X_O | O      | Eastern  | OWin    | XXOOX_X_OOEastern  |
		| XXOOX_XOO | X      | Eastern  | Tie     | XXOOX_XOOXEastern  |
		| XXOOX_X_O | O      | Southern | Tie     | XXOOX_X_OOSouthern |
		| X_OOX_X_O | X      | Northern | OWin    | X_OOX_X_OXNorthern |
		| XOOOXXX_O | X      | Southern | Tie     | XOOOXXX_OXSouthern |
		| X_OOXXX_O | O      | Northern | Tie     | X_OOXXX_OONorthern |
		| X_OOXXXOO | X      | Northern | Tie     | X_OOXXXOOXNorthern |
		| X_OOXXX_O | O      | Southern | Tie     | X_OOXXX_OOSouthern |
		| X_OOX_X_O | X      | Eastern  | Tie     | X_OOX_X_OXEastern  |
		| XOOOX_XXO | X      | Eastern  | Tie     | XOOOX_XXOXEastern  |
		| X_OOX_XXO | O      | Northern | Tie     | X_OOX_XXOONorthern |
		| X_OOX_XXO | O      | Eastern  | OWin    | X_OOX_XXOOEastern  |
		| X_OOX_X_O | X      | Southern | OWin    | X_OOX_X_OXSouthern |

Scenario: Database builder should populate move responses 2
	Given I start a new game in the following state
		| A | B | C |
		| X |   | O |
		|   | X |   |
		| X | O | O |
	Given I have a database builder
	When I populate move responses for the current game
	Then I expect the ILiteDatabase.DeleteCollection to have been called '1' times
	Then I expect the ILiteDatabase.EnsureIndex to have been called with the following 'Id'
	Then I expect the ILiteDatabase.InsertBulk to have been called with the following move response records
		| Board     | Player | Response | Outcome | Id                 |
		| XXOOX_XOO | X      | Eastern  | Tie     | XXOOX_XOOXEastern  |
		| XXO_X_XOO | O      | Western  | Tie     | XXO_X_XOOOWestern  |
		| XXO_X_XOO | O      | Eastern  | OWin    | XXO_X_XOOOEastern  |
		| X_O_X_XOO | X      | Northern | OWin    | X_O_X_XOOXNorthern |
		| X_O_X_XOO | X      | Western  | XWin    | X_O_X_XOOXWestern  |
		| XOO_XXXOO | X      | Western  | XWin    | XOO_XXXOOXWestern  |
		| X_O_XXXOO | O      | Northern | XWin    | X_O_XXXOOONorthern |
		| X_OOXXXOO | X      | Northern | Tie     | X_OOXXXOOXNorthern |
		| X_O_XXXOO | O      | Western  | Tie     | X_O_XXXOOOWestern  |
		| X_O_X_XOO | X      | Eastern  | Tie     | X_O_X_XOOXEastern  |
