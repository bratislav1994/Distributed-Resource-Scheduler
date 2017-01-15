using CommonLibrary;
using KLRESClient;
using KSRes;
using KSRes.Access;
using KSRes.Data;
using KSRESClient;
using LKRes.Access;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;

namespace BDD
{
    [Binding]
    public class MeasurementsAndManagingSteps
    {
        private double expectedPower = 0;
        private static KSRESClientViewModel model = new KSRESClientViewModel();
        private static MasterViewModel master = new MasterViewModel();
        private static Client client = null;
        private static Process p1 = new Process();
        private static Process p2 = new Process();
        private static Process p3 = new Process();
        private static Process p4 = new Process();
        private static Generator g1;
        private static Generator g2;
        private static Generator g3;
        private static Group gr;
        private static Site s;
        private SortedDictionary<DateTime, double> retVal;

        [BeforeFeature("mam")]
        public static void Start()
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.GetDirectoryName(executable);
            path = path.Substring(0, path.LastIndexOf("BDD"));
            AppDomain.CurrentDomain.SetData("DataDirectory", path + "LKRes");

            #region initialize database

            g1 = new Generator()
            {
                MRID = "test1",
                ActivePower = 5,
                Pmin = 1,
                Pmax = 20,
                Price = 4,
                GroupID = "testGroup",
                WorkingMode = WorkingMode.REMOTE
            };

            g2 = new Generator()
            {
                MRID = "test2",
                ActivePower = 12,
                Pmin = 1,
                Pmax = 20,
                Price = 8,
                GroupID = "testGroup",
                WorkingMode = WorkingMode.REMOTE
            };

            g3 = new Generator()
            {
                MRID = "test3",
                ActivePower = 6,
                Pmin = 1,
                Pmax = 20,
                Price = 3,
                GroupID = "testGroup",
                WorkingMode = WorkingMode.REMOTE
            };

            gr = new Group()
            {
                MRID = "testGroup",
                SiteID = "testSite"
            };

            s = new Site()
            {
                MRID = "testSite"
            };

            DataBase.Instance.AddGenerator(new LKRes.Data.GeneratorEntity()
            {
                Gen = g1
            });

            DataBase.Instance.AddGenerator(new LKRes.Data.GeneratorEntity()
            {
                Gen = g2
            });

            DataBase.Instance.AddGenerator(new LKRes.Data.GeneratorEntity()
            {
                Gen = g3
            });

            DataBase.Instance.AddGroup(new LKRes.Data.GroupEntity()
            {
                GEntity = gr
            });

            DataBase.Instance.AddSite(new LKRes.Data.SiteEntity()
            {
                SEntity = s
            });
            #endregion initialize database

            #region startservices
            model.IsTest = true;

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
            #endregion startservices

            master.HomeVM.Username2 = "testClient";
            master.HomeVM.Password2 = "pass111";
            master.HomeVM.RegistrateCommand.Execute();

            master.HomeVM.Username = "testClient";
            master.HomeVM.Password = "pass111";
            master.HomeVM.LoginCommand.Execute();

            client = model.Client;
        }

        [AfterFeature("mam")]
        public static void Stop()
        {
            DataBase.Instance.RemoveGenerator(g1);
            DataBase.Instance.RemoveGenerator(g2);
            DataBase.Instance.RemoveGenerator(g3);
            DataBase.Instance.RemoveGroup(gr);
            DataBase.Instance.RemoveSite(s);

            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.GetDirectoryName(executable);
            path = path.Substring(0, path.LastIndexOf("BDD"));
            AppDomain.CurrentDomain.SetData("DataDirectory", path + "KSRes");

            LocalDB.Instance.DeleteRegistrationService("testClient");

            master.HomeVM.Host.Close();
            p1.Kill();
            p2.Kill();
            p3.Kill();
            p4.Kill();
        }

        [Given(@"I have entered (.*) into text box\.")]
        public void GivenIHaveEnteredIntoTextBox_(double requiredAP)
        {
            model.NeededPower = requiredAP.ToString();

            double activePower = 0;
            double diff = 0;

            foreach (Generator generator in model.Client.Generators)
            {
                activePower += generator.ActivePower;
            }

            diff = requiredAP - activePower;

            if (diff > 0)
            {
                List<Generator> sortedList = model.Client.Generators.OrderBy(o => o.Price).ToList();
                foreach (Generator remoteGenerator in sortedList)
                {
                    double diff1 = 0;
                    if ((diff1 = remoteGenerator.Pmax - remoteGenerator.ActivePower) > 0)
                    {
                        if (diff > diff1)
                        {
                            diff -= diff1;
                        }
                        else if (diff <= diff1)
                        {
                            diff = 0;
                            break;
                        }
                    }
                }
                if (diff == 0)
                {
                    expectedPower = requiredAP;
                }
                else if (diff > 0)
                {
                    expectedPower = requiredAP - diff;
                }
            }
            else
            {
                diff = Math.Abs(diff);
                List<Generator> sortedList = model.Client.Generators.OrderByDescending(o => o.Price).ToList();
                foreach (Generator remoteGenerator in sortedList)
                {
                    if (remoteGenerator.ActivePower > 0)
                    {
                        if (diff != 0)
                        {
                            if (remoteGenerator.ActivePower <= diff)
                            {
                                diff -= remoteGenerator.ActivePower;
                            }
                            else
                            {
                                if ((remoteGenerator.ActivePower - remoteGenerator.Pmin) >= diff)
                                {
                                    diff = 0;
                                    break;
                                }
                                else
                                {
                                    diff -= remoteGenerator.ActivePower - remoteGenerator.Pmin;
                                }
                            }
                        }
                    }
                }
                if(diff == 0)
                {
                    expectedPower = requiredAP;
                }
                else if (diff > 0)
                {
                    expectedPower = diff;
                }
            }


            Assert.IsTrue(model.IssueCommand.CanExecute());
        }

        [When(@"I press issueCommand button")]
        public void WhenIPressIssueCommandButton()
        {
            model.IssueCommand.Execute();
        }
        
        [Then(@"Generator's active power should change")]
        public void ThenGeneratorSActivePowerShouldChange()
        {
            Thread.Sleep(3000);
            double activePower = 0;
            foreach (Generator g in DataBase.Instance.ReadData().Generators)
            {
                activePower += g.ActivePower;
            }
            Assert.AreEqual(expectedPower, activePower);
        }

        [Given(@"I have two consuptions in database")]
        public void GivenIHaveTwoConsuptionsInDatabase()
        {
            ConsuptionHistory h1 = new ConsuptionHistory()
            {
                Consuption = 10,
                TimeStamp = DateTime.Now
            };

            ConsuptionHistory h2 = new ConsuptionHistory()
            {
                Consuption = 11,
                TimeStamp = DateTime.Now.AddMinutes(3)
            };

            LocalDB.Instance.AddConsuption(h1);
            LocalDB.Instance.AddConsuption(h2);
        }

        [When(@"I press DrawHistory button")]
        public void WhenIPressDrawHistoryButton()
        {
            model.Client.Proxy.LoadForecastOnDemand();
            
            retVal = model.Client.Proxy.GetLoadForecast();
        }

        [Then(@"Binding list for showing should not be empty")]
        public void ThenBindingListForShowingShouldNotBeEmpty()
        {
            Assert.AreEqual(180, retVal.Count);
        }

        [Given(@"I have entered (.*) into text box predefined for that\.")]
        public void GivenIHaveEnteredIntoTextBoxPredefinedForThat_(int p0)
        {
            model.NumberOfDays = p0.ToString();
        }

        [When(@"I press drawProductionHistory button")]
        public void WhenIPressDrawProductionHistoryButton()
        {
            Assert.IsTrue(model.DrawHistoryCommand.CanExecute());
            model.DrawHistoryCommand.Execute();
        }

        [Then(@"Binding lsit for showing should not be empty")]
        public void ThenBindingLsitForShowingShouldNotBeEmpty()
        {
            SortedDictionary<DateTime, double> test = model.Client.GetProductionHistory(double.Parse(model.NumberOfDays));
            Assert.IsTrue(test.SequenceEqual(model.ProductionHistory));
        }
    }
}
