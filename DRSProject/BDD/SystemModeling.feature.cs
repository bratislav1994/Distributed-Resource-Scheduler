﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.1.0.0
//      SpecFlow Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace BDD
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.1.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("SystemModeling")]
    [NUnit.Framework.CategoryAttribute("sysMod")]
    public partial class SystemModelingFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "SystemModeling.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "SystemModeling", " I want to manage my system", ProgrammingLanguage.CSharp, new string[] {
                        "sysMod"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Registration")]
        [NUnit.Framework.CategoryAttribute("base2")]
        [NUnit.Framework.TestCaseAttribute("test1", "test1", new string[0])]
        public virtual void Registration(string user, string pass, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "base2"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Registration", @__tags);
#line 6
this.ScenarioSetup(scenarioInfo);
#line 7
 testRunner.Given(string.Format("I have entered {0} into text box", user), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 8
 testRunner.And(string.Format("I have entered {0} into password box", pass), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 9
 testRunner.When("I press register button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 10
 testRunner.Then("I should be registered on system", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Registration failed")]
        [NUnit.Framework.CategoryAttribute("base2")]
        [NUnit.Framework.TestCaseAttribute("proba", "proba", new string[0])]
        public virtual void RegistrationFailed(string user, string pass, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "base2"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Registration failed", @__tags);
#line 16
 this.ScenarioSetup(scenarioInfo);
#line 17
 testRunner.Given(string.Format("I have entered already existing {0} into text box.", user), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 18
 testRunner.And(string.Format("I have entered {0} into password box", pass), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 19
 testRunner.When("I press register button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 20
 testRunner.Then("I should not be registered on system", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Login")]
        [NUnit.Framework.CategoryAttribute("base2")]
        [NUnit.Framework.TestCaseAttribute("proba", "proba", new string[0])]
        public virtual void Login(string user1, string pass1, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "base2"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Login", @__tags);
#line 26
 this.ScenarioSetup(scenarioInfo);
#line 27
 testRunner.Given(string.Format("I have entered registered {0} into text box.", user1), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 28
 testRunner.And(string.Format("I have entered {0} into password box.", pass1), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 29
 testRunner.When("I press login button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 30
 testRunner.Then("I should be login on system", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Login failed")]
        [NUnit.Framework.CategoryAttribute("base2")]
        [NUnit.Framework.TestCaseAttribute("pera", "pera", new string[0])]
        public virtual void LoginFailed(string user1, string pass1, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "base2"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Login failed", @__tags);
#line 36
 this.ScenarioSetup(scenarioInfo);
#line 37
 testRunner.Given(string.Format("I have entered not existing {0} into text box", user1), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 38
 testRunner.And(string.Format("I have entered {0} into password box.", pass1), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 39
 testRunner.When("I press login button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 40
 testRunner.Then("I should not be login on system", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Add generator in new site and new group")]
        [NUnit.Framework.CategoryAttribute("base1")]
        public virtual void AddGeneratorInNewSiteAndNewGroup()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add generator in new site and new group", new string[] {
                        "base1"});
#line 46
 this.ScenarioSetup(scenarioInfo);
#line 47
 testRunner.Given("I have entered name into text box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 48
 testRunner.And("I have entered activePower into text box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 49
 testRunner.And("I have choose hasMeas from combo box", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 50
 testRunner.And("I have choose workingMode from combo box", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 51
 testRunner.And("I have entered pMin into text box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 52
 testRunner.And("I have entered pMax into text box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 53
 testRunner.And("I have entered price into text box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 54
 testRunner.And("I have choose genType from combo box", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 55
 testRunner.And("I have checked radioButton from input form", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 56
 testRunner.And("I have entered siteName into text box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 57
 testRunner.And("I have entered groupName into text box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 58
 testRunner.When("I press add button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 59
 testRunner.Then("generator should be added", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Add generator in existing site and existing group")]
        [NUnit.Framework.CategoryAttribute("base1")]
        public virtual void AddGeneratorInExistingSiteAndExistingGroup()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add generator in existing site and existing group", new string[] {
                        "base1"});
#line 62
 this.ScenarioSetup(scenarioInfo);
#line 63
 testRunner.Given("I have entered name into text box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 64
 testRunner.And("I have entered activePower into text box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 65
 testRunner.And("I have choose hasMeas from combo box", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 66
 testRunner.And("I have choose workingMode from combo box", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 67
 testRunner.And("I have entered pMin into text box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 68
 testRunner.And("I have entered pMax into text box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 69
 testRunner.And("I have entered price into text box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 70
 testRunner.And("I have choose genType from combo box", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 71
 testRunner.And("I have checked radioButtonn from input form", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 72
 testRunner.And("I have choose groupName from text box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 73
 testRunner.And("I have choose siteName from combo box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 74
 testRunner.When("existing groups and sites", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 75
 testRunner.And("I press add button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 76
 testRunner.Then("generator should be added", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Add generator in existing site and new group")]
        [NUnit.Framework.CategoryAttribute("base1")]
        public virtual void AddGeneratorInExistingSiteAndNewGroup()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add generator in existing site and new group", new string[] {
                        "base1"});
#line 79
 this.ScenarioSetup(scenarioInfo);
#line 80
 testRunner.Given("I have entered name into text box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 81
 testRunner.And("I have entered activePower into text box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 82
 testRunner.And("I have choose hasMeas from combo box", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 83
 testRunner.And("I have choose workingMode from combo box", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 84
 testRunner.And("I have entered pMin into text box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 85
 testRunner.And("I have entered pMax into text box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 86
 testRunner.And("I have entered price into text box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 87
 testRunner.And("I have choose genType from combo box", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 88
 testRunner.And("I have checked radioButtonnn from input form", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 89
 testRunner.And("I have choose cmbSiteName from combo box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 90
 testRunner.And("I have choose txbGroupName from text box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 91
 testRunner.When("existing sites and new group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 92
 testRunner.And("I press add button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 93
 testRunner.Then("generator should be added", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Not all input form are passed validation for Add generator")]
        [NUnit.Framework.CategoryAttribute("base1")]
        public virtual void NotAllInputFormArePassedValidationForAddGenerator()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Not all input form are passed validation for Add generator", new string[] {
                        "base1"});
#line 96
 this.ScenarioSetup(scenarioInfo);
#line 97
 testRunner.Given("I have entered activePower into text box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 98
 testRunner.And("I have choose hasMeas from combo box", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 99
 testRunner.And("I have choose workingMode from combo box", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 100
 testRunner.And("I have entered pMin into text box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 101
 testRunner.And("I have entered pMax into text box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 102
 testRunner.And("I have entered price into text box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 103
 testRunner.And("I have choose genType from combo box", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 104
 testRunner.And("I have checked radioButton from input form", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 105
 testRunner.And("I have entered siteName into text box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 106
 testRunner.And("I have entered groupName into text box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 107
 testRunner.When("I have entered empty name into text box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 108
 testRunner.Then("create button should be disabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Remove generator")]
        [NUnit.Framework.CategoryAttribute("base1")]
        public virtual void RemoveGenerator()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Remove generator", new string[] {
                        "base1"});
#line 111
 this.ScenarioSetup(scenarioInfo);
#line 112
 testRunner.Given("I have selected generator from table", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 113
 testRunner.When("I have pressed remove button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 114
 testRunner.Then("generator should be deleted", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Not all edit form are passed validation for Edit generator")]
        [NUnit.Framework.CategoryAttribute("base1")]
        public virtual void NotAllEditFormArePassedValidationForEditGenerator()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Not all edit form are passed validation for Edit generator", new string[] {
                        "base1"});
#line 117
 this.ScenarioSetup(scenarioInfo);
#line 118
 testRunner.Given("I have filled edit form", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 119
 testRunner.When("I have entered empty Editname into text box.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 120
 testRunner.Then("edit button should be disabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Not selected generator from table")]
        [NUnit.Framework.CategoryAttribute("base1")]
        public virtual void NotSelectedGeneratorFromTable()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Not selected generator from table", new string[] {
                        "base1"});
#line 123
 this.ScenarioSetup(scenarioInfo);
#line 124
 testRunner.Given("I have table with at least one generator", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 125
 testRunner.When("generator not selected", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 126
 testRunner.Then("edit button, remove button, show data button should be disabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Edit generator in new site and new group")]
        [NUnit.Framework.CategoryAttribute("base1")]
        public virtual void EditGeneratorInNewSiteAndNewGroup()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Edit generator in new site and new group", new string[] {
                        "base1"});
#line 129
 this.ScenarioSetup(scenarioInfo);
#line 130
 testRunner.Given("I have filled edit form.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 131
 testRunner.And("I have checked radioButton from edit form", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 132
 testRunner.And("I have entered editSiteName into text box..", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 133
 testRunner.And("I have entered editGroupName into text box..", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 134
 testRunner.When("I pressed edit button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 135
 testRunner.Then("generator should be edited", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
