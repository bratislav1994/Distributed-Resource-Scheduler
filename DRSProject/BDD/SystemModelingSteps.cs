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
            master.EditRemoveWindowVM.IsTest = true;
            master.AddWindowVM.IsTest = true;

            p1.StartInfo = new ProcessStartInfo(path + "LoadForecast\\bin\\Debug\\LoadForecast.exe");
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
            DataBase.Instance.AddGenerator(new LKRes.Data.GeneratorEntity() { Gen = generator });
            DataBase.Instance.AddGroup(new LKRes.Data.GroupEntity() { GEntity = new Group() { MRID = "test", Name = "testbaza", SiteID = "testSajt" } });
            DataBase.Instance.AddSite(new LKRes.Data.SiteEntity() { SEntity = new Site() { MRID = "testSajt", Name = "testbaza" } });
            master.EditRemoveWindowVM.SelectedItem = generator;
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
            DataBase.Instance.RemoveGenerator(new Generator() { MRID = "genTest" });
            Assert.AreEqual(0, DataBase.Instance.ReadData().Generators.Count);
            DataBase.Instance.RemoveGroup(new Group() { MRID = "test", Name = "testbaza", SiteID = "testSajt" });
            DataBase.Instance.RemoveSite(new Site() { MRID = "testSajt", Name = "testbaza" });
        }
        #endregion removeGenerator

        #region editDisabled
        [Given(@"I have filled edit form")]
        public void GivenIHaveFilledEditForm()
        {
            Generator generator = new Generator()
            {
                MRID = "genTest",
                GroupID = "test",
                HasMeasurment = true,
                ActivePower = 50,
                Name = "gen1",
                BasePoint = -1,
                Price = 200,
                GeneratorType = GeneratorType.MICROHYDRO,
                Pmax = 100,
                Pmin = 20,
                SetPoint = -1,
                WorkingMode = WorkingMode.LOCAL
            };

            master.EditRemoveWindowVM.SelectedItem = generator;

            master.EditRemoveWindowVM.EditName = generator.Name;
            master.EditRemoveWindowVM.EditActivePower = generator.ActivePower.ToString();
            master.EditRemoveWindowVM.EditCmbHasMeasSelectedItem = generator.HasMeasurment;
            master.EditRemoveWindowVM.EditCmbWorkingModeSelectedItem = generator.WorkingMode;
            master.EditRemoveWindowVM.EditPMin = generator.Pmin.ToString();
            master.EditRemoveWindowVM.EditPMax = generator.Pmax.ToString();
            master.EditRemoveWindowVM.EditPrice = generator.Price.ToString();
            master.EditRemoveWindowVM.EditCmbGeneratorTypeSelectedItem = generator.GeneratorType;

            master.EditRemoveWindowVM.EditRadioButton = false;
            master.EditRemoveWindowVM.EditRadioButton1 = true;
            master.EditRemoveWindowVM.EditRadioButton2 = false;

            DataBase.Instance.AddGenerator(new LKRes.Data.GeneratorEntity() { Gen = generator });
            DataBase.Instance.AddGroup(new LKRes.Data.GroupEntity() { GEntity = new Group() { MRID = "test", Name = "testbaza", SiteID = "testSajt" } });
            DataBase.Instance.AddSite(new LKRes.Data.SiteEntity() { SEntity = new Site() { MRID = "testSajt", Name = "testbaza" } });
        }

        [When(@"I have entered empty Editname into text box\.")]
        public void WhenIHaveEnteredEmptyEditnameIntoTextBox_()
        {
            master.EditRemoveWindowVM.EditName = string.Empty;
        }

        [Then(@"edit button should be disabled")]
        public void ThenEditButtonShouldBeDisabled()
        {
            Assert.IsFalse(master.EditRemoveWindowVM.EditCommand.CanExecute());

            UpdateInfo forRemove = DataBase.Instance.ReadData();
            foreach (Generator g in forRemove.Generators)
            {
                DataBase.Instance.RemoveGenerator(g);
            }

            foreach (Group g in forRemove.Groups)
            {
                DataBase.Instance.RemoveGroup(g);
            }

            foreach (Site s in forRemove.Sites)
            {
                DataBase.Instance.RemoveSite(s);
            }
        }

        #endregion editDisabled

        #region selectedFromTableDisabled
        [Given(@"I have table with at least one generator")]
        public void GivenIHaveTableWithAtLeastOneGenerator()
        {
            gen = new Generator() { MRID = "14142", GroupID = "test" };
            DataBase.Instance.AddGenerator(new LKRes.Data.GeneratorEntity() { Gen = gen });
            DataBase.Instance.AddGroup(new LKRes.Data.GroupEntity() { GEntity = new Group() { MRID = "test", Name = "testbaza", SiteID = "testSajt" } });
            DataBase.Instance.AddSite(new LKRes.Data.SiteEntity() { SEntity = new Site() { MRID = "testSajt", Name = "testbaza" } });
        }

        [When(@"generator not selected")]
        public void WhenGeneratorNotSelected()
        {
            master.EditRemoveWindowVM.SelectedItem = null;
        }

        [Then(@"edit button, remove button, show data button should be disabled")]
        public void ThenEditButtonRemoveButtonShowDataButtonShouldBeDisabled()
        {
            Assert.IsFalse(master.EditRemoveWindowVM.ClickEditCommand.CanExecute());
            Assert.IsFalse(master.EditRemoveWindowVM.ShowDataCommand.CanExecute());
            Assert.IsFalse(master.EditRemoveWindowVM.RemoveCommand.CanExecute());

            UpdateInfo forRemove = DataBase.Instance.ReadData();
            foreach (Generator g in forRemove.Generators)
            {
                DataBase.Instance.RemoveGenerator(g);
            }

            foreach (Group g in forRemove.Groups)
            {
                DataBase.Instance.RemoveGroup(g);
            }

            foreach (Site s in forRemove.Sites)
            {
                DataBase.Instance.RemoveSite(s);
            }
        }

        #endregion

        #region editNewSiteAndNewGroup
        [Given(@"I have filled edit form\.")]
        public void GivenIHaveFilledEditForm_()
        {
            Generator generator = new Generator()
            {
                MRID = "genTest",
                GroupID = "test",
                HasMeasurment = true,
                ActivePower = 50,
                Name = "gen1",
                BasePoint = -1,
                Price = 200,
                GeneratorType = GeneratorType.MICROHYDRO,
                Pmax = 100,
                Pmin = 20,
                SetPoint = -1,
                WorkingMode = WorkingMode.LOCAL
            };

            master.EditRemoveWindowVM.SelectedItem = generator;

            master.EditRemoveWindowVM.EditName = generator.Name;
            master.EditRemoveWindowVM.EditActivePower = generator.ActivePower.ToString();
            master.EditRemoveWindowVM.EditCmbHasMeasSelectedItem = generator.HasMeasurment;
            master.EditRemoveWindowVM.EditCmbWorkingModeSelectedItem = generator.WorkingMode;
            master.EditRemoveWindowVM.EditPMin = generator.Pmin.ToString();
            master.EditRemoveWindowVM.EditPMax = generator.Pmax.ToString();
            master.EditRemoveWindowVM.EditPrice = generator.Price.ToString();
            master.EditRemoveWindowVM.EditCmbGeneratorTypeSelectedItem = generator.GeneratorType;

            DataBase.Instance.AddGenerator(new LKRes.Data.GeneratorEntity() { Gen = generator });
            DataBase.Instance.AddGroup(new LKRes.Data.GroupEntity() { GEntity = new Group() { MRID = "test", Name = "testbaza", SiteID = "testSajt" } });
            DataBase.Instance.AddSite(new LKRes.Data.SiteEntity() { SEntity = new Site() { MRID = "testSajt", Name = "testbaza" } });
        }

        [Given(@"I have checked radioButton from edit form")]
        public void GivenIHaveCheckedRadioButtonFromEditForm()
        {
            master.EditRemoveWindowVM.EditRadioButton = true;
            master.EditRemoveWindowVM.EditRadioButton1 = false;
            master.EditRemoveWindowVM.EditRadioButton2 = false;
        }

        [Given(@"I have entered editSiteName into text box\.\.")]
        public void GivenIHaveEnteredEditSiteNameIntoTextBox_()
        {
            master.EditRemoveWindowVM.EditSiteName = "NoviSajt";
        }

        [Given(@"I have entered editGroupName into text box\.\.")]
        public void GivenIHaveEnteredEditGroupNameIntoTextBox_()
        {
            master.EditRemoveWindowVM.EditGroupName = "NovaGrupa";
        }

        [When(@"I pressed edit button")]
        public void WhenIPressedEditButton()
        {
            master.HomeVM.Username = "proba";
            master.HomeVM.Password = "proba";
            master.HomeVM.LoginCommand.Execute();
            Assert.IsTrue(master.EditRemoveWindowVM.EditCommand.CanExecute());
            master.EditRemoveWindowVM.EditCommand.Execute();
        }

        [Then(@"generator should be edited")]
        public void ThenGeneratorShouldBeEdited()
        {
            Thread.Sleep(5000);
            gen = master.Client.Generators[master.Client.Generators.Count - 1];
            Group deleteGroup = master.AddWindowVM.Client.GetGroupFromId(gen.GroupID);
            Site deleteSite = master.AddWindowVM.Client.GetSiteFromId(deleteGroup.SiteID);

            Assert.AreEqual("NoviSajt", master.Client.Sites[master.Client.Sites.Count - 1].Name);
            Assert.AreEqual("NovaGrupa", master.Client.Groups[master.Client.Groups.Count - 1].Name);

            DataBase.Instance.RemoveGenerator(gen);
            DataBase.Instance.RemoveGroup(deleteGroup);
            DataBase.Instance.RemoveGroup(new Group() { MRID = "test", Name = "testbaza", SiteID = "testSajt" });
            DataBase.Instance.RemoveSite(deleteSite);
            DataBase.Instance.RemoveSite(new Site() { MRID = "testSajt", Name = "testbaza" });
            master.AddWindowVM.Client.Sites.Remove(deleteSite);
            master.AddWindowVM.Client.Groups.Remove(deleteGroup);
        }

        #endregion
    }
}

