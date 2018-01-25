@BetterTestingApproach
@Unit
Feature: Game
Verify that the tic tac toe game follows the domain specifications

Background: 
	Given I have a container
	Given I have a tictactoe factory

Scenario: New game should start in the correct state
	When I start a new game
	Then The current player should be 'X'
	Then The move history should be empty
	Then The game state should be 'InPlay'
	Then The game should not be over
	Then The total moves made should be '0'
	Then The game board should look like
		| A | B | C |
		|   |   |   |
		|   |   |   |
		|   |   |   |
	Then The available legal moves should be
		| Move      |
		| NorthWest |
		| Northern  |
		| NorthEast |
		| Western   |
		| Center    |
		| Eastern   |
		| SouthWest |
		| Southern  |
		| SouthEast |

Scenario: New game with moves should start in correct state
	When I start a new game with the following moves
		| Move     |
		| Center   |
		| Southern |
		| Eastern  |
	Then The current player should be 'O'
	Then The move history should be
		| Move     |
		| Center   |
		| Southern |
		| Eastern  |
	Then The game state should be 'InPlay'
	Then The game should not be over
	Then The total moves made should be '3'
	Then The game board should look like
		| A | B | C |
		|   |   |   |
		|   | X | X |
		|   | O |   |	
	Then The available legal moves should be
		| Move      |
		| NorthWest |
		| Northern  |
		| NorthEast |
		| Western   |
		| SouthWest |
		| SouthEast |

Scenario Outline: New game with moves should throw if 
	Given I expect an exception to be thrown
	When I start a new game with the following moves '<Moves>'
	Then I expect the following GameException to be thrown
		| Message                                                |
		| Invalid move passed in. Cannot create game from moves. |
	Examples: 
		| Test Name                                    | Moves                                                                                   |
		| the moves has duplicates                     | Center,Southern,Center                                                                  |
		| the moves are invalid due to game being over | Center,SouthWest,Southern,SouthEast,Northern,Eastern                                    |
		| the moves are too many                       | Center,SouthWest,Eastern,Western,NorthWest,SouthEast,Southern,Northern,NorthEast,Center |

Scenario: Game should detect tie
	Given I start a new game with the following moves
		| Move      |
		| Center    |
		| SouthWest |
		| NorthWest |
		| SouthEast |
		| Southern  |
		| Northern  |
		| Western   |
		| Eastern   |
	When I make the move 'NorthEast'
	Then The move history should be
		| Move      |
		| Center    |
		| SouthWest |
		| NorthWest |
		| SouthEast |
		| Southern  |
		| Northern  |
		| Western   |
		| Eastern   |
		| NorthEast |
	Then The game state should be 'Tie'
	Then The game should be over
	Then The total moves made should be '9'
	Then The game board should look like
		| A | B | C |
		| X | O | X |
		| X | X | O |
		| O | X | O |
	Then The available legal moves should be empty

Scenario: Game should detect 'X' win for western column
	Given I start a new game with the following moves
		| Move      |
		| NorthWest |
		| Center    |
		| Western   |
		| Southern  |
	When I make the move 'SouthWest'
	Then The move history should be
		| Move      |
		| NorthWest |
		| Center    |
		| Western   |
		| Southern  |
		| SouthWest |
	Then The game state should be 'XWin'
	Then The game should be over
	Then The total moves made should be '5'
	Then The game board should look like
		| A | B | C |
		| X |   |   |
		| X | O |   |
		| X | O |   |
	Then The available legal moves should be empty

Scenario: Game should detect 'X' win for center column
	Given I start a new game with the following moves
		| Move      |
		| Center    |
		| SouthWest |
		| Northern  |
		| Western   |
	When I make the move 'Southern'
	Then The move history should be
		| Move      |
		| Center    |
		| SouthWest |
		| Northern  |
		| Western   |
		| Southern  |
	Then The game state should be 'XWin'
	Then The game should be over
	Then The total moves made should be '5'
	Then The game board should look like
		| A | B | C |
		|   | X |   |
		| O | X |   |
		| O | X |   |
	Then The available legal moves should be empty

Scenario: Game should detect 'X' win for eastern column
	Given I start a new game with the following moves
		| Move      |
		| NorthEast |
		| Center    |
		| Eastern   |
		| Southern  |
	When I make the move 'SouthEast'
	Then The move history should be
		| Move      |
		| NorthEast |
		| Center    |
		| Eastern   |
		| Southern  |
		| SouthEast |
	Then The game state should be 'XWin'
	Then The game should be over
	Then The total moves made should be '5'
	Then The game board should look like
		| A | B | C |
		|   |   | X |
		|   | O | X |
		|   | O | X |
	Then The available legal moves should be empty

Scenario: Game should detect 'X' win for diagonal slope
	Given I start a new game with the following moves
		| Move      |
		| NorthWest |
		| Western   |
		| Center    |
		| Eastern   |
	When I make the move 'SouthEast'
	Then The move history should be
		| Move      |
		| NorthWest |
		| Western   |
		| Center    |
		| Eastern   |
		| SouthEast |
	Then The game state should be 'XWin'
	Then The game should be over
	Then The total moves made should be '5'
	Then The game board should look like
		| A | B | C |
		| X |   |   |
		| O | X | O |
		|   |   | X |
	Then The available legal moves should be empty

#Todo: can a non C# programmer complete this test?
Scenario: Game should detect 'X' win for diagonal grade
	Given Todo
	When Todo
	Then Todo

#A more compact way to write the tests for wins for O but at the expense of readability? Should we change this...
Scenario Outline: Game should detect 'O' win for
	Given I start a new game with the following moves '<InitialMoves>'
	When I make the move '<Move>'
	Then The move history should be '<ExpectedMoveHistory>'
	Then The game state should be 'OWin'
	Then The game should be over
	Then The total moves made should be '6'
	Then The game board should be '<ExpectedGameBoard>'
	Examples: 
		| Test Name      | InitialMoves                               | Move      | ExpectedMoveHistory                                  | ExpectedGameBoard |
		| western column | Center,Western,Southern,SouthWest,Eastern  | NorthWest | Center,Western,Southern,SouthWest,Eastern,NorthWest  | O__OXXOX_         |
		| center column  | Western,Northern,SouthWest,Center,Eastern  | Southern  | Western,Northern,SouthWest,Center,Eastern,Southern   | _O_XOXXO_         |
		| eastern column | Center,Eastern,Southern,SouthEast,Western  | NorthEast | Center,Eastern,Southern,SouthEast,Western,NorthEast  | __OXXO_XO         |
		| diagonal slope | Northern,NorthWest,Southern,Center,Western | SouthEast | Northern,NorthWest,Southern,Center,Western,SouthEast | OX_XO__XO         |
		| diagonal grade | Northern,SouthWest,Southern,Center,Western | NorthEast | Northern,SouthWest,Southern,Center,Western,NorthEast | _XOXO_OX_         |

Scenario: Game should reset correctly
	Given I start a new game with the following moves
		| Move     |
		| Center   |
		| Southern |
		| Eastern  |
	When I reset the game
	Then The current player should be 'X'
	Then The move history should be empty
	Then The game state should be 'InPlay'
	Then The game should not be over
	Then The total moves made should be '0'
	Then The game board should look like
		| A | B | C |
		|   |   |   |
		|   |   |   |
		|   |   |   |
	Then The available legal moves should be
		| Move      |
		| NorthWest |
		| Northern  |
		| NorthEast |
		| Western   |
		| Center    |
		| Eastern   |
		| SouthWest |
		| Southern  |
		| SouthEast |

#Move,Over,Reset,Undomove
Scenario: Game should raise game change event for move correctly
	Given I start a new game
	Given I start listening to all game events
	When I make the move 'Center'
	Then The following game state changed events are raised
		| GameChange | GameState | CurrentPlayer |
		| Move       | InPlay    | O             |
	Then The following number of GameOver events are raised: '0'
	Then The following number of GameReset events are raised: '0'

Scenario: Game should raise game change event for win correctly
	Given I start a new game with the following moves
		| Move     |
		| Northern |
		| Western  |
		| Southern |
		| Eastern  |
	Given I start listening to all game events
	When I make the move 'Center'
	Then The following game state changed events are raised
		| GameChange | GameState | CurrentPlayer |
		| Over       | XWin      | O             |
	Then The following number of GameOver events are raised: '1'
	Then The following number of GameReset events are raised: '0'

Scenario: Game should raise game change event for reset correctly
	Given I start a new game with the following moves
		| Move     |
		| Northern |
		| Western  |
		| Southern |
		| Eastern  |
	Given I start listening to all game events
	When I reset the game
	Then The following game state changed events are raised
		| GameChange | GameState | CurrentPlayer |
		| Reset      | InPlay    | X             |
	Then The following number of GameOver events are raised: '0'
	Then The following number of GameReset events are raised: '1'

Scenario: Game should raise game change event for undomove correctly
	Given I start a new game with the following moves
		| Move     |
		| Northern |
		| Western  |
		| Southern |
		| Eastern  |
	Given I start listening to all game events
	When I undo last move
	Then The following game state changed events are raised
		| GameChange | GameState | CurrentPlayer |
		| UndoMove    | InPlay    | O             |
	Then The following number of GameOver events are raised: '0'
	Then The following number of GameReset events are raised: '0'


