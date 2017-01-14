using CommonLibrary;
using KLRESClient;
using NUnit.Framework;
using System;
using System.Threading;
using TechTalk.SpecFlow;

namespace BDDTest
{
    [Binding]
    public class SystemModelingSteps
    {
        private MasterViewModel master = new MasterViewModel();
        private int numOfGenerators = 0;

        [Given(@"I have entered (.*) into text box")]
        public void GivenIHaveEnteredTestIntoTextBox(string username)
        {
            master.HomeVM.Username2 = username;
        }

        [Given(@"I have entered already existing (.*) into text box\.")]
        public void GivenIHaveEnteredAlreadyExistingTestIntoTextBox_(string username)
        {
            master.HomeVM.Username2 = username;
        }

        [Given(@"I have entered (.*) into password box")]
        public void GivenIHaveEnteredTestIntoPasswordBox(string password)
        {
            this.master.HomeVM.Password2 = password;
            // ScenarioContext.Current.Pending();
        }

        [When(@"I press register button")]
        public void WhenIPressRegisterButton()
        {
            this.master.HomeVM.RegistrateCommand.Execute();
        }

        [Then(@"I should be registered on system")]
        public void ThenIShouldBeRegisteredOnSystem()
        {
            Assert.IsTrue(this.master.HomeVM.IsRegistered);
        }

        [Then(@"I should not be registered on system")]
        public void ThenIShouldNotBeRegisteredOnSystem()
        {
            Assert.IsFalse(this.master.HomeVM.IsRegistered);
        }

        [Given(@"I have entered registered (.*) into text box\.")]
        public void GivenIHaveEnteredRegisteredTestIntoTextBox_(string p0)
        {
            this.master.HomeVM.Username = p0;
        }

        [Given(@"I have entered (.*) into password box\.")]
        public void GivenIHaveEnteredTestIntoPasswordBox_(string p0)
        {
            this.master.HomeVM.Password = p0;
        }

        [When(@"I press login button")]
        public void WhenIPressLoginButton()
        {
            this.master.HomeVM.LoginCommand.Execute();
        }

        [Then(@"I should be login on system")]
        public void ThenIShouldBeLoginOnSystem()
        {
            Assert.IsTrue(this.master.HomeVM.IsLogin);
        }

        [Then(@"I should not be login on system")]
        public void ThenIShouldNotBeLoginOnSystem()
        {
            Assert.IsFalse(this.master.HomeVM.IsLogin);
        }

        // add
        [Given(@"I have entered name into text box\.")]
        public void GivenIHaveEnteredNameIntoTextBox_()
        {
            this.master.AddWindowVM.Name = "gen2";
        }

        [Given(@"I have entered activePower into text box\.")]
        public void GivenIHaveEnteredActivePowerIntoTextBox_()
        {
            this.master.AddWindowVM.ActivePower = "50";
        }

        [Given(@"I have choose hasMeas from combo box")]
        public void GivenIHaveChooseHasMeasFromComboBox()
        {
            this.master.AddWindowVM.CmbHasMeasSelectedItem = true;
        }

        [Given(@"I have choose workingMode from combo box")]
        public void GivenIHaveChooseWorkingModeFromComboBox()
        {
            this.master.AddWindowVM.CmbWorkingModeSelectedItem = WorkingMode.LOCAL;
        }

        [Given(@"I have entered pMin into text box\.")]
        public void GivenIHaveEnteredPMinIntoTextBox_()
        {
            this.master.AddWindowVM.PMin = "20";
        }

        [Given(@"I have entered pMax into text box\.")]
        public void GivenIHaveEnteredPMaxIntoTextBox_()
        {
            this.master.AddWindowVM.PMax = "100";
        }

        [Given(@"I have entered price into text box\.")]
        public void GivenIHaveEnteredPriceIntoTextBox_()
        {
            this.master.AddWindowVM.Price = "200";
        }

        [Given(@"I have choose genType from combo box")]
        public void GivenIHaveChooseGenTypeFromComboBox()
        {
            this.master.AddWindowVM.CmbGeneratorTypeSelectedItem = GeneratorType.MICROHYDRO;
        }

        [Given(@"I have checked radioButton from input form")]
        public void GivenIHaveCheckedRadioButtonFromInputForm()
        {
            this.master.AddWindowVM.RadioButton = true;
        }

        [Given(@"I have entered siteName into text box\.")]
        public void GivenIHaveEnteredSiteNameIntoTextBox_()
        {
            this.master.AddWindowVM.SiteName = "Sajt1";
        }

        [Given(@"I have entered groupName into text box\.")]
        public void GivenIHaveEnteredGroupNameIntoTextBox_()
        {
            this.master.AddWindowVM.GroupName = "Grupa1";
        }

        [When(@"I press add button")]
        public void WhenIPressAddButton()
        {
            this.master.HomeVM.Username = "proba";
            this.master.HomeVM.Password = "proba";
            this.master.HomeVM.LoginCommand.Execute();
            numOfGenerators = this.master.Client.Generators.Count;
            Assert.IsTrue(this.master.AddWindowVM.CreateCommand.CanExecute());
            this.master.AddWindowVM.CreateCommand.Execute();
        }

        [Then(@"generator should be added")]
        public void ThenGeneratorShouldBeAdded()
        {
            Thread.Sleep(1000);
            Assert.AreEqual(numOfGenerators + 1, this.master.Client.Generators.Count);
            Assert.AreEqual("gen2", this.master.Client.Generators[this.master.Client.Generators.Count - 1].Name);
        }

        [BeforeScenario("SystemModeling")]
        private void Before()
        {

        }
    }
}

