@BetterTestingApproach
@Unit
Feature: ArtificialIntelligence
	Verify that the artificial intelligence strategies work correctly

Background: 
	Given I have a container
	Given I mock the move database
	Given I have a tictactoe factory

Scenario: OmniscientGod AI should win the following games
	Given I start a new game with the following moves
		| Move |
		|      |
	Given I setup the mock move database to contain the following
		| Board | Player | Response | Outcome |
		|       |        |          |         |


	
