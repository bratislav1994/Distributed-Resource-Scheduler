using CommonLibrary;
using KLRESClient;
using KSRes.Access;
using LKRes.Access;
using NUnit.Framework;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;

namespace BDDTest
{
    [Binding]
    public class SystemModelingSteps
    {
        private static MasterViewModel master = new MasterViewModel();
        private string registrationServie = String.Empty;
        private int numOfGenerators = 0;
        private Generator gen = new Generator();
        private static Process p1 = new Process();
        private static Process p2 = new Process();
        private static Process p3 = new Process();
        private static Process p4 = new Process();

        [BeforeFeature("sysMod")]
        public static void Start()
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.GetDirectoryName(executable);
            path = path.Substring(0, path.LastIndexOf("BDD"));

            master.HomeVM.IsTest = true;

            p1.StartInfo = new ProcessStartInfo( path + "LoadForecast\\bin\\Debug\\LoadForecast.exe");
            p1.Start();
            Thread.Sleep(100);
            p2.StartInfo = new ProcessStartInfo(path + "ActivePowerGenerator\\bin\\Debug\\ActivePowerGenerator.exe");
            p2.Start();
            Thread.Sleep(100);
            p3.StartInfo = new ProcessStartInfo(path + "KSRes\\bin\\Debug\\KSRes.exe");
            p3.Start();
            Thread.Sleep(100);
            p4.StartInfo = new ProcessStartInfo(path + "LKRes\\bin\\Debug\\LKRes.exe");
            p4.Start();

        }

        [AfterFeature("sysMod")]
        public static void Stop()
        {
            master.HomeVM.Host.Close();
            p1.Kill();
            p2.Kill();
            p3.Kill();
            p4.Kill();
        }

        [BeforeScenario("base2")]
        public static void Initialize()
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.GetDirectoryName(executable);
            path = path.Substring(0, path.LastIndexOf("BDD"));
            AppDomain.CurrentDomain.SetData("DataDirectory", path + "KSRes");
        }

        [BeforeScenario("base1")]
        public static void Initialize1()
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.GetDirectoryName(executable);
            path = path.Substring(0, path.LastIndexOf("BDD"));
            AppDomain.CurrentDomain.SetData("DataDirectory", path + "LKRes");
        }

        #region login and register

        [Given(@"I have entered (.*) into text box")]
        public void GivenIHaveEnteredTestIntoTextBox(string username)
        {
            master.HomeVM.Username2 = username;
            registrationServie = username;
        }

        [Given(@"I have entered already existing (.*) into text box\.")]
        public void GivenIHaveEnteredAlreadyExistingTestIntoTextBox_(string username)
        {
            master.HomeVM.Username2 = username;
        }

        [Given(@"I have entered (.*) into password box")]
        public void GivenIHaveEnteredTestIntoPasswordBox(string password)
        {
            master.HomeVM.Password2 = password;
        }

        [When(@"I press register button")]
        public void WhenIPressRegisterButton()
        {
            master.HomeVM.RegistrateCommand.Execute();
        }

        [Then(@"I should be registered on system")]
        public void ThenIShouldBeRegisteredOnSystem()
        {
            Assert.IsTrue(master.HomeVM.IsRegistered);
            LocalDB.Instance.DeleteRegistrationService(registrationServie);
            registrationServie = String.Empty;
        }

        [Then(@"I should not be registered on system")]
        public void ThenIShouldNotBeRegisteredOnSystem()
        {
            Assert.IsFalse(master.HomeVM.IsRegistered);
        }

        [Given(@"I have entered registered (.*) into text box\.")]
        public void GivenIHaveEnteredRegisteredTestIntoTextBox_(string username)
        {
            master.HomeVM.Username = username;
        }

        [Given(@"I have entered (.*) into password box\.")]
        public void GivenIHaveEnteredTestIntoPasswordBox_(string password)
        {
            master.HomeVM.Password = password;
        }

        [When(@"I press login button")]
        public void WhenIPressLoginButton()
        {
            master.HomeVM.LoginCommand.Execute();
        }

        [Then(@"I should be login on system")]
        public void ThenIShouldBeLoginOnSystem()
        {
            Assert.IsTrue(master.HomeVM.IsLogin);
        }

        [Then(@"I should not be login on system")]
        public void ThenIShouldNotBeLoginOnSystem()
        {
            Assert.IsFalse(master.HomeVM.IsLogin);
        }

        #endregion

        #region Add generator
        // add
        [Given(@"I have entered name into text box\.")]
        public void GivenIHaveEnteredNameIntoTextBox_()
        {
            master.AddWindowVM.Name = "gen2";
        }

        [Given(@"I have entered activePower into text box\.")]
        public void GivenIHaveEnteredActivePowerIntoTextBox_()
        {
            master.AddWindowVM.ActivePower = "50";
        }

        [Given(@"I have choose hasMeas from combo box")]
        public void GivenIHaveChooseHasMeasFromComboBox()
        {
            master.AddWindowVM.CmbHasMeasSelectedItem = true;
        }

        [Given(@"I have choose workingMode from combo box")]
        public void GivenIHaveChooseWorkingModeFromComboBox()
        {
            master.AddWindowVM.CmbWorkingModeSelectedItem = WorkingMode.LOCAL;
        }

        [Given(@"I have entered pMin into text box\.")]
        public void GivenIHaveEnteredPMinIntoTextBox_()
        {
            master.AddWindowVM.PMin = "20";
        }

        [Given(@"I have entered pMax into text box\.")]
        public void GivenIHaveEnteredPMaxIntoTextBox_()
        {
            master.AddWindowVM.PMax = "100";
        }

        [Given(@"I have entered price into text box\.")]
        public void GivenIHaveEnteredPriceIntoTextBox_()
        {
            master.AddWindowVM.Price = "200";
        }

        [Given(@"I have choose genType from combo box")]
        public void GivenIHaveChooseGenTypeFromComboBox()
        {
            master.AddWindowVM.CmbGeneratorTypeSelectedItem = GeneratorType.MICROHYDRO;
        }

        [Given(@"I have checked radioButton from input form")]
        public void GivenIHaveCheckedRadioButtonFromInputForm()
        {
            master.AddWindowVM.RadioButton = true;
        }

        [Given(@"I have entered siteName into text box\.")]
        public void GivenIHaveEnteredSiteNameIntoTextBox_()
        {
            master.AddWindowVM.SiteName = "Sajt1";
        }

        [Given(@"I have entered groupName into text box\.")]
        public void GivenIHaveEnteredGroupNameIntoTextBox_()
        {
            master.AddWindowVM.GroupName = "Grupa1";
        }

        [When(@"I press add button")]
        public void WhenIPressAddButton()
        {
            master.HomeVM.Username = "proba";
            master.HomeVM.Password = "proba";
            master.HomeVM.LoginCommand.Execute();
            numOfGenerators = master.Client.Generators.Count;
            Assert.IsTrue(master.AddWindowVM.CreateCommand.CanExecute());
            master.AddWindowVM.CreateCommand.Execute();
        }

        [Then(@"generator should be added")]
        public void ThenGeneratorShouldBeAdded()
        {
            Thread.Sleep(1000);
            Assert.AreEqual(numOfGenerators + 1, master.Client.Generators.Count);
            gen = master.Client.Generators[master.Client.Generators.Count - 1];
            Assert.AreEqual("gen2", master.Client.Generators[master.Client.Generators.Count - 1].Name);
            Group deleteGroup = master.AddWindowVM.Client.GetGroupFromId(gen.GroupID);
            Site deleteSite = master.AddWindowVM.Client.GetSiteFromId(deleteGroup.SiteID);
            DataBase.Instance.RemoveGenerator(gen);
            DataBase.Instance.RemoveGroup(deleteGroup);
            DataBase.Instance.RemoveSite(deleteSite);
            master.AddWindowVM.Client.Sites.Remove(deleteSite);
            master.AddWindowVM.Client.Groups.Remove(deleteGroup);
         
        }

        // next 3 given for radio button1

        [Given(@"I have checked radioButtonn from input form")]
        public void GivenIHaveCheckedRadioButtonnFromInputForm()
        {
            master.AddWindowVM.RadioButton1 = true;
            master.AddWindowVM.RadioButton = false;
            master.AddWindowVM.RadioButton2 = false;
        }

        [Given(@"I have choose groupName from text box\.")]
        public void GivenIHaveChooseGroupNameFromTextBox_()
        {
            master.AddWindowVM.Cmb2GroupNameSelectedItem = new Group() { MRID = "test", Name = "test", SiteID = "testSajt" };
        }

        [Given(@"I have choose siteName from combo box\.")]
        public void GivenIHaveChooseSiteNameFromComboBox_()
        {
            master.AddWindowVM.CmbSiteNameSelectedItem = new Site() { MRID = "testSajt", Name = "test" };
        }

        [When(@"existing groups and sites")]
        public void WhenExistingGroupsAndSites()
        {
            DataBase.Instance.AddGroup(new LKRes.Data.GroupEntity() { GEntity = new Group() { MRID = "test", Name = "testbaza", SiteID = "testSajt" } });
            DataBase.Instance.AddSite(new LKRes.Data.SiteEntity() { SEntity = new Site() { MRID = "testSajt", Name = "testbaza" } });
        }

        // next 3 given for radio button2

        [Given(@"I have checked radioButtonnn from input form")]
        public void GivenIHaveCheckedRadioButtonnnFromInputForm()
        {
            master.AddWindowVM.RadioButton2 = true;
            master.AddWindowVM.RadioButton = false;
            master.AddWindowVM.RadioButton1 = false;
        }

        [Given(@"I have choose cmbSiteName from combo box\.")]
        public void GivenIHaveChooseCmbSiteNameFromComboBox_()
        {
            master.AddWindowVM.Cmb3SiteNameSelectedItem = new Site() { MRID = "testSajt", Name = "test" };
        }

        [Given(@"I have choose txbGroupName from text box\.")]
        public void GivenIHaveChooseTxbGroupNameFromTextBox_()
        {
            master.AddWindowVM.TxbGroupName = "Grupa1";
        }

        [When(@"existing sites and new group")]
        public void WhenExistingSitesAndNewGroup()
        {
            DataBase.Instance.AddSite(new LKRes.Data.SiteEntity() { SEntity = new Site() { MRID = "testSajt", Name = "test" } });
        }

        [When(@"I have entered empty name into text box\.")]
        public void WhenIHaveEnteredEmptyNameIntoTextBox_()
        {
            master.HomeVM.Username = "proba";
            master.HomeVM.Password = "proba";
            master.HomeVM.LoginCommand.Execute();
            master.AddWindowVM.Name = string.Empty;
        }

        [Then(@"create button should be disabled")]
        public void ThenCreateButtonShouldBeDisabled()
        {
            Assert.IsFalse(master.AddWindowVM.CreateCommand.CanExecute());
        }
        #endregion

        #region removeGenerator
        [Given(@"I have selected generator from table")]
        public void GivenIHaveSelectedGeneratorFromTable()
        {
            Generator generator = new Generator() { MRID = "genTest", GroupID = "test" };
            master.EditRemoveWindowVM.SelectedItem = generator;
            DataBase.Instance.AddGenerator(new LKRes.Data.GeneratorEntity() { Gen = generator });
            DataBase.Instance.AddGroup(new LKRes.Data.GroupEntity() { GEntity = new Group() { MRID = "test", Name = "testbaza", SiteID = "testSajt" } });
            DataBase.Instance.AddSite(new LKRes.Data.SiteEntity() { SEntity = new Site() { MRID = "testSajt", Name = "testbaza" } });
        }

        [When(@"I have pressed remove button")]
        public void WhenIHavePressedRemoveButton()
        {
            master.HomeVM.Username = "proba";
            master.HomeVM.Password = "proba";
            master.HomeVM.LoginCommand.Execute();
            Assert.IsTrue(master.EditRemoveWindowVM.RemoveCommand.CanExecute());
            master.EditRemoveWindowVM.RemoveCommand.Execute();
        }

        [Then(@"generator should be deleted")]
        public void ThenGeneratorShouldBeDeleted()
        {
            Assert.AreEqual(null, DataBase.Instance.ReadData().Generators.Where(o => o.MRID.Equals("genTest")).FirstOrDefault());
            DataBase.Instance.RemoveGroup(new Group() { MRID = "test", Name = "testbaza", SiteID = "testSajt" } );
            DataBase.Instance.RemoveSite(new Site() { MRID = "testSajt", Name = "testbaza" });
        }
        #endregion removeGenerator

    }
}

