﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.2.0.0
//      SpecFlow Generator Version:2.2.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Arcesoft.TicTacToe.BetterTestingApproach.ArtificialIntelligence
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.2.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class OmniscientGodFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Microsoft.VisualStudio.TestTools.UnitTesting.TestContext _testContext;
        
#line 1 "OmniscientGod.feature"
#line hidden
        
        public virtual Microsoft.VisualStudio.TestTools.UnitTesting.TestContext TestContext
        {
            get
            {
                return this._testContext;
            }
            set
            {
                this._testContext = value;
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner(null, 0);
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "OmniscientGod", "\tVerify that the omniscientgod artificial intelligence works correctly", ProgrammingLanguage.CSharp, new string[] {
                        "BetterTestingApproach",
                        "Unit"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassCleanupAttribute()]
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute()]
        public virtual void TestInitialize()
        {
            if (((testRunner.FeatureContext != null) 
                        && (testRunner.FeatureContext.FeatureInfo.Title != "OmniscientGod")))
            {
                global::Arcesoft.TicTacToe.BetterTestingApproach.ArtificialIntelligence.OmniscientGodFeature.FeatureSetup(null);
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCleanupAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Microsoft.VisualStudio.TestTools.UnitTesting.TestContext>(TestContext);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 6
#line 7
 testRunner.Given("I have a container", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 8
 testRunner.Given("I mock the IMoveResponseRepository", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.Given("I have a tictactoe factory", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 10
 testRunner.Given("I have the artificial intelligence \'OmniscientGod\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Omniscient god AI should throw exception when making move is game is already over" +
            "")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "OmniscientGod")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("BetterTestingApproach")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Unit")]
        public virtual void OmniscientGodAIShouldThrowExceptionWhenMakingMoveIsGameIsAlreadyOver()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Omniscient god AI should throw exception when making move is game is already over" +
                    "", ((string[])(null)));
#line 12
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Move"});
            table1.AddRow(new string[] {
                        "Northern"});
            table1.AddRow(new string[] {
                        "Eastern"});
            table1.AddRow(new string[] {
                        "Center"});
            table1.AddRow(new string[] {
                        "Western"});
            table1.AddRow(new string[] {
                        "Southern"});
#line 13
 testRunner.Given("I start a new game with the following moves", ((string)(null)), table1, "Given ");
#line 20
 testRunner.Given("I expect an exception to be thrown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 21
 testRunner.When("I have the AI make the next random best move", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Message"});
            table2.AddRow(new string[] {
                        "Unable to make a move because the game is over."});
#line 22
 testRunner.Then("I expect the following GameException to be thrown", ((string)(null)), table2, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Omniscient god AI should raise error when no available moves are found")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "OmniscientGod")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("BetterTestingApproach")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Unit")]
        public virtual void OmniscientGodAIShouldRaiseErrorWhenNoAvailableMovesAreFound()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Omniscient god AI should raise error when no available moves are found", ((string[])(null)));
#line 26
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 27
 testRunner.Given("I start a new game", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 31
 testRunner.Given("I expect an exception to be thrown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 32
 testRunner.When("I have the AI make the next random best move", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Message"});
            table3.AddRow(new string[] {
                        "Unable to make a move because there are no available moves for game board _______" +
                            "__. Possible corrupt move data access or game."});
#line 33
 testRunner.Then("I expect the following Exception to be thrown", ((string)(null)), table3, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Omniscient god AI should select winning X move")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "OmniscientGod")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("BetterTestingApproach")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Unit")]
        public virtual void OmniscientGodAIShouldSelectWinningXMove()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Omniscient god AI should select winning X move", ((string[])(null)));
#line 37
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 38
 testRunner.Given("I start a new game", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Board",
                        "Player",
                        "Response",
                        "Outcome"});
            table4.AddRow(new string[] {
                        "_________",
                        "X",
                        "Eastern",
                        "XWin"});
            table4.AddRow(new string[] {
                        "_________",
                        "X",
                        "Southern",
                        "Tie"});
            table4.AddRow(new string[] {
                        "_________",
                        "X",
                        "Northern",
                        "OWin"});
#line 39
 testRunner.Given("I setup the mock IMoveResponseRepository.FindMoveResponses method to return the f" +
                    "ollowing MoveResponses for game board \"_________\" and player \"X\"", ((string)(null)), table4, "Given ");
#line 44
 testRunner.When("I have the AI make the next random best move", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 45
 testRunner.Then("The last move made should be one of the following moves \'Eastern\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Omniscient god AI should select tie X move")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "OmniscientGod")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("BetterTestingApproach")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Unit")]
        public virtual void OmniscientGodAIShouldSelectTieXMove()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Omniscient god AI should select tie X move", ((string[])(null)));
#line 47
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 48
 testRunner.Given("I start a new game", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Board",
                        "Player",
                        "Response",
                        "Outcome"});
            table5.AddRow(new string[] {
                        "_________",
                        "X",
                        "Eastern",
                        "OWin"});
            table5.AddRow(new string[] {
                        "_________",
                        "X",
                        "Southern",
                        "Tie"});
            table5.AddRow(new string[] {
                        "_________",
                        "X",
                        "Northern",
                        "OWin"});
#line 49
 testRunner.Given("I setup the mock IMoveResponseRepository.FindMoveResponses method to return the f" +
                    "ollowing MoveResponses for game board \"_________\" and player \"X\"", ((string)(null)), table5, "Given ");
#line 54
 testRunner.When("I have the AI make the next random best move", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 55
 testRunner.Then("The last move made should be one of the following moves \'Southern\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Omniscient god AI should select losing X move")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "OmniscientGod")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("BetterTestingApproach")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Unit")]
        public virtual void OmniscientGodAIShouldSelectLosingXMove()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Omniscient god AI should select losing X move", ((string[])(null)));
#line 57
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 58
 testRunner.Given("I start a new game", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Board",
                        "Player",
                        "Response",
                        "Outcome"});
            table6.AddRow(new string[] {
                        "_________",
                        "X",
                        "Eastern",
                        "OWin"});
            table6.AddRow(new string[] {
                        "_________",
                        "X",
                        "Southern",
                        "OWin"});
            table6.AddRow(new string[] {
                        "_________",
                        "X",
                        "Northern",
                        "OWin"});
#line 59
 testRunner.Given("I setup the mock IMoveResponseRepository.FindMoveResponses method to return the f" +
                    "ollowing MoveResponses for game board \"_________\" and player \"X\"", ((string)(null)), table6, "Given ");
#line 64
 testRunner.When("I have the AI make the next random best move", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 65
 testRunner.Then("The last move made should be one of the following moves \'Eastern,Southern,Norther" +
                    "n\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Omniscient god AI should select winning O move")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "OmniscientGod")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("BetterTestingApproach")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Unit")]
        public virtual void OmniscientGodAIShouldSelectWinningOMove()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Omniscient god AI should select winning O move", ((string[])(null)));
#line 67
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "Move"});
            table7.AddRow(new string[] {
                        "Center"});
#line 68
 testRunner.Given("I start a new game with the following moves", ((string)(null)), table7, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "Board",
                        "Player",
                        "Response",
                        "Outcome"});
            table8.AddRow(new string[] {
                        "____X____",
                        "O",
                        "Eastern",
                        "XWin"});
            table8.AddRow(new string[] {
                        "____X____",
                        "O",
                        "Southern",
                        "Tie"});
            table8.AddRow(new string[] {
                        "____X____",
                        "O",
                        "Northern",
                        "OWin"});
#line 71
 testRunner.Given("I setup the mock IMoveResponseRepository.FindMoveResponses method to return the f" +
                    "ollowing MoveResponses for game board \"____X____\" and player \"O\"", ((string)(null)), table8, "Given ");
#line 76
 testRunner.When("I have the AI make the next random best move", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 77
 testRunner.Then("The last move made should be one of the following moves \'Northern\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Omniscient god AI should select tie O move")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "OmniscientGod")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("BetterTestingApproach")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Unit")]
        public virtual void OmniscientGodAIShouldSelectTieOMove()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Omniscient god AI should select tie O move", ((string[])(null)));
#line 79
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                        "Move"});
            table9.AddRow(new string[] {
                        "Center"});
#line 80
 testRunner.Given("I start a new game with the following moves", ((string)(null)), table9, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                        "Board",
                        "Player",
                        "Response",
                        "Outcome"});
            table10.AddRow(new string[] {
                        "____X____",
                        "O",
                        "Eastern",
                        "XWin"});
            table10.AddRow(new string[] {
                        "____X____",
                        "O",
                        "Southern",
                        "Tie"});
            table10.AddRow(new string[] {
                        "____X____",
                        "O",
                        "Northern",
                        "XWin"});
#line 83
 testRunner.Given("I setup the mock IMoveResponseRepository.FindMoveResponses method to return the f" +
                    "ollowing MoveResponses for game board \"____X____\" and player \"O\"", ((string)(null)), table10, "Given ");
#line 88
 testRunner.When("I have the AI make the next random best move", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 89
 testRunner.Then("The last move made should be one of the following moves \'Southern\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Omniscient god AI should select losing O move")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "OmniscientGod")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("BetterTestingApproach")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Unit")]
        public virtual void OmniscientGodAIShouldSelectLosingOMove()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Omniscient god AI should select losing O move", ((string[])(null)));
#line 91
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                        "Move"});
            table11.AddRow(new string[] {
                        "Center"});
#line 92
 testRunner.Given("I start a new game with the following moves", ((string)(null)), table11, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
                        "Board",
                        "Player",
                        "Response",
                        "Outcome"});
            table12.AddRow(new string[] {
                        "____X____",
                        "O",
                        "Eastern",
                        "XWin"});
            table12.AddRow(new string[] {
                        "____X____",
                        "O",
                        "Southern",
                        "XWin"});
            table12.AddRow(new string[] {
                        "____X____",
                        "O",
                        "Northern",
                        "XWin"});
#line 95
 testRunner.Given("I setup the mock IMoveResponseRepository.FindMoveResponses method to return the f" +
                    "ollowing MoveResponses for game board \"____X____\" and player \"O\"", ((string)(null)), table12, "Given ");
#line 100
 testRunner.When("I have the AI make the next random best move", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 101
 testRunner.Then("The last move made should be one of the following moves \'Eastern,Southern,Norther" +
                    "n\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Omniscient god AI should find move results")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "OmniscientGod")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("BetterTestingApproach")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Unit")]
        public virtual void OmniscientGodAIShouldFindMoveResults()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Omniscient god AI should find move results", ((string[])(null)));
#line 103
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table13 = new TechTalk.SpecFlow.Table(new string[] {
                        "Move"});
            table13.AddRow(new string[] {
                        "Center"});
#line 104
 testRunner.Given("I start a new game with the following moves", ((string)(null)), table13, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table14 = new TechTalk.SpecFlow.Table(new string[] {
                        "Board",
                        "Player",
                        "Response",
                        "Outcome"});
            table14.AddRow(new string[] {
                        "____X____",
                        "O",
                        "Southern",
                        "Tie"});
            table14.AddRow(new string[] {
                        "____X____",
                        "O",
                        "Western",
                        "XWin"});
            table14.AddRow(new string[] {
                        "____X____",
                        "O",
                        "Eastern",
                        "Tie"});
#line 107
 testRunner.Given("I setup the mock IMoveResponseRepository.FindMoveResponses method to return the f" +
                    "ollowing MoveResponses for game board \"____X____\" and player \"O\"", ((string)(null)), table14, "Given ");
#line 112
 testRunner.When("I have the AI find move results for the current game", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table15 = new TechTalk.SpecFlow.Table(new string[] {
                        "MoveMade",
                        "GameStateAfterMove"});
            table15.AddRow(new string[] {
                        "Southern",
                        "Tie"});
            table15.AddRow(new string[] {
                        "Western",
                        "XWin"});
            table15.AddRow(new string[] {
                        "Eastern",
                        "Tie"});
#line 113
 testRunner.Then("The move results should contain the following", ((string)(null)), table15, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
