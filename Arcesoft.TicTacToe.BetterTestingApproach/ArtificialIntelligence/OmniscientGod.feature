@BetterTestingApproach
@Unit
Feature: OmniscientGod
	Verify that the omniscientgod artificial intelligence works correctly

Background: 
	Given I have a container
	Given I mock IMoveDatabase
	Given I have a tictactoe factory
	Given I have the artificial intelligence 'OmniscientGod'

Scenario: Omniscient god AI should throw exception when making move is game is already over
	Given I start a new game with the following moves
		| Move     |
		| Northern |
		| Eastern  |
		| Center   |
		| Western  |
		| Southern |
	Given I expect an exception to be thrown
	When I have the AI make the next random best move
	Then I expect the following GameException to be thrown
         | Message                                         |
         | Unable to make a move because the game is over. |

Scenario: Omniscient god AI should raise error when no available moves are found
	Given I start a new game
	Given I setup the IMoveDatabase.MovesDataTable to return the following
		| Board     | Player | Response | Outcome |
		| ____X____ | O      | Eastern  | XWin    |
	Given I expect an exception to be thrown
	When I have the AI make the next random best move
	Then I expect the following Exception to be thrown
         | Message                                         |
         | Unable to make a move because there are no available moves for game board _________ |

Scenario: Omniscient god AI should select winning X move
	Given I start a new game
	Given I setup the IMoveDatabase.MovesDataTable to return the following
		| Board     | Player | Response | Outcome |
		| _________ | X      | Eastern  | XWin    |
		| _________ | X      | Southern | Tie     |
		| _________ | X      | Northern | OWin    |
	When I have the AI make the next random best move
	Then The last move made should be one of the following moves 'Eastern'

Scenario: Omniscient god AI should select tie X move
	Given I start a new game
	Given I setup the IMoveDatabase.MovesDataTable to return the following
		| Board     | Player | Response | Outcome |
		| _________ | X      | Eastern  | OWin    |
		| _________ | X      | Southern | Tie     |
		| _________ | X      | Northern | OWin    |
	When I have the AI make the next random best move
	Then The last move made should be one of the following moves 'Southern'

Scenario: Omniscient god AI should select losing X move
	Given I start a new game
	Given I setup the IMoveDatabase.MovesDataTable to return the following
		| Board     | Player | Response | Outcome |
		| _________ | X      | Eastern  | OWin    |
		| _________ | X      | Southern | OWin    |
		| _________ | X      | Northern | OWin    |
	When I have the AI make the next random best move
	Then The last move made should be one of the following moves 'Eastern,Southern,Northern'

Scenario: Omniscient god AI should select winning O move
	Given I start a new game with the following moves
		| Move   |
		| Center |
	Given I setup the IMoveDatabase.MovesDataTable to return the following
		| Board     | Player | Response | Outcome |
		| ____X____ | O      | Eastern  | XWin    |
		| ____X____ | O      | Southern | Tie     |
		| ____X____ | O      | Northern | OWin    |
	When I have the AI make the next random best move
	Then The last move made should be one of the following moves 'Northern'

Scenario: Omniscient god AI should select tie O move
	Given I start a new game with the following moves
		| Move   |
		| Center |
	Given I setup the IMoveDatabase.MovesDataTable to return the following
		| Board     | Player | Response | Outcome |
		| ____X____ | O      | Eastern  | XWin    |
		| ____X____ | O      | Southern | Tie     |
		| ____X____ | O      | Northern | XWin    |
	When I have the AI make the next random best move
	Then The last move made should be one of the following moves 'Southern'

Scenario: Omniscient god AI should select losing O move
	Given I start a new game with the following moves
		| Move   |
		| Center |
	Given I setup the IMoveDatabase.MovesDataTable to return the following
		| Board     | Player | Response | Outcome |
		| ____X____ | O      | Eastern  | XWin    |
		| ____X____ | O      | Southern | XWin    |
		| ____X____ | O      | Northern | XWin    |
	When I have the AI make the next random best move
	Then The last move made should be one of the following moves 'Eastern,Southern,Northern'


Scenario: Omniscient god AI should find move results
	Given I start a new game with the following moves
		| Move   |
		| Center |
	Given I setup the IMoveDatabase.MovesDataTable to return the following
		| Board     | Player | Response | Outcome |
		| _________ | O      | Eastern  |         |
		| ____XO___ | O      | Eastern  |         |
		| ____X____ | O      | Southern | Tie     |
		| ____X____ | O      | Western  | XWin    |
		| ____X____ | X      | Eastern  |         |
		| X___O___X | O      | Eastern  |         |
	When I have the AI find move results for the current game
	Then The move results should contain the following
         | MoveMade | GameStateAfterMove |
         | Southern | Tie                |
         | Western  | XWin               |


