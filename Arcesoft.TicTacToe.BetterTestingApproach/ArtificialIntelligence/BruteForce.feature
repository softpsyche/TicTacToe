@BetterTestingApproach
@Unit
Feature: BruteForce
	Verify that the brute force artificial intelligence works correctly

Background: 
	Given I have a container
	Given I have a tictactoe factory
	Given I have the artificial intelligence 'BruteForce'

#This is a pretty slow test...Left here as an example on how this sort of 
#testing can create a tax on performance even though its still a test that does not cross any
#process boundaries.
#it is perhaps a bad test if we consider we have some other options to get similar
#coverage that might be faster.
#however, this also proves that moving first in tic tac toe is preferable...
Scenario: Should find tie move results for game as player X
	Given I start a new game
	When I have the AI find move results for the current game
	Then The move results should contain the following
		| MoveMade  | GameStateAfterMove |
		| NorthWest | Tie                |
		| Northern  | Tie                |
		| NorthEast | Tie                |
		| Western   | Tie                |
		| Center    | Tie                |
		| Eastern   | Tie                |
		| SouthWest | Tie                |
		| Southern  | Tie                |
		| SouthEast | Tie                |

Scenario: Should find winning move results for game as player X when Os first move is western
	Given I start a new game with the following moves
		| Move      |
		| Center    |
		| Western   |
		| NorthWest |
		| SouthEast |
	When I have the AI find move results for the current game
	Then The move results should contain the following
		| MoveMade  | GameStateAfterMove |
		| Northern  | XWin               |
		| NorthEast | XWin               |
		| Eastern   | Tie                |
		| SouthWest | Tie                |
		| Southern  | Tie                |

Scenario: Should find winning move results for game as player X when Os first move is eastern
	Given I start a new game with the following moves
		| Move      |
		| Center    |
		| Eastern   |
		| SouthEast |
		| NorthWest |
	When I have the AI find move results for the current game
	Then The move results should contain the following
		| MoveMade  | GameStateAfterMove |
		| Northern  | Tie                |
		| NorthEast | Tie                |
		| Western   | Tie                |
		| SouthWest | XWin               |
		| Southern  | XWin               |

Scenario: Should find winning move results for game as player X when Os first move is northern
	Given I start a new game with the following moves
		| Move      |
		| Center    |
		| Northern  |
		| NorthWest |
		| SouthEast |
	When I have the AI find move results for the current game
	Then The move results should contain the following
		| MoveMade  | GameStateAfterMove |
		| Eastern   | Tie                |
		| Western   | XWin               |
		| NorthEast | Tie                |
		| SouthWest | XWin               |
		| Southern  | Tie                |

Scenario: Should find winning move results for game as player X when Os first move is southern
	Given I start a new game with the following moves
		| Move      |
		| Center    |
		| Southern  |
		| SouthWest |
		| NorthEast |
	When I have the AI find move results for the current game
	Then The move results should contain the following
		| MoveMade  | GameStateAfterMove |
		| NorthWest | XWin               |
		| Northern  | Tie                |
		| Eastern   | Tie                |
		| Western   | XWin               |
		| SouthEast | Tie                |

Scenario: Should find winning move results for game as player O when Xs two moves are eastern and western
	Given I start a new game with the following moves
		| Move    |
		| Western |
		| Center  |
		| Eastern |
	When I have the AI find move results for the current game
	Then The move results should contain the following
		| MoveMade  | GameStateAfterMove |
		| NorthWest | OWin               |
		| Northern  | OWin               |
		| NorthEast | OWin               |
		| SouthWest | OWin               |
		| Southern  | OWin               |
		| SouthEast | OWin               |

Scenario: Should find winning move results for game as player O when Xs two moves are northern and southern
	Given I start a new game with the following moves
		| Move     |
		| Northern |
		| Center   |
		| Southern |
	When I have the AI find move results for the current game
	Then The move results should contain the following
		| MoveMade  | GameStateAfterMove |
		| NorthWest | OWin               |
		| NorthEast | OWin               |
		| Western   | OWin               |
		| Eastern   | OWin               |
		| SouthWest | OWin               |
		| SouthEast | OWin               |

Scenario Outline: Should make next random best move available
	Given I start a new game with the following moves '<InitialMoves>'
	When I have the AI make the next random best move
	Then The last move made should be one of the following moves '<ExpectedMoves>'
	Examples: 
		| Test Name                         | InitialMoves                       | ExpectedMoves                                             |
		| for X when best option is winning | Center,Western,NorthWest,SouthEast | Northern,NorthEast                                        |
		| for X when best option is tieing  | Center,NorthWest                   | Northern,NorthEast,Eastern,Western,SouthEast,Southern     |
		| for X when best option is losing  | Western,Center,Eastern,Northern    | NorthWest,NorthEast,SouthEast,Southern,SouthWest          |
		| for O when best option is winning | Eastern,Center,Western             | NorthEast,Northern,NorthWest,SouthEast,Southern,SouthWest |
		| for O when best option is tieing  | Center                             | NorthEast,NorthWest,SouthEast,SouthWest                   |
		| for O when best option is losing  | Center,Eastern,NorthEast           | Northern,NorthWest,Western,SouthEast,Southern,SouthWest   |                                                      
