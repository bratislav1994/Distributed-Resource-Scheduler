// <copyright file="LKClientServiceTest.cs" company="company">
// product
// Copyright (c) 2016
// by company ( http://www.example.com )
// </copyright>

namespace LKResClientTest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using CommonLibrary;
    using CommonLibrary.Interfaces;
    using KLRESClient;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Used for testing LKClient
    /// </summary>
    [TestFixture]
    public class LKClientServiceTest
    {
        #region Declarations

        /// <summary>
        /// instance of client
        /// </summary>
        private LKClientService client = null;

        /// <summary>
        /// mock for calling service methods
        /// </summary>
        private ILKForClient mockService = null;

        /// <summary>
        /// mock for calling service methods
        /// </summary>
        private ILKForClient mockService2 = null;

        #endregion

        /// <summary>
        /// initialize fields
        /// </summary>
        [OneTimeSetUp]
        public void SetupTest()
        {
            this.client = new LKClientService();
            this.mockService = Substitute.For<ILKForClient>();
            this.mockService.Registration("proba", "proba");
            UpdateInfo update = new UpdateInfo();
            update.UpdateType = UpdateType.ADD;
            update.Generators.Add(new Generator());
            update.Groups.Add(new Group());
            update.Sites.Add(new Site());
            this.mockService.GetMySystem().Returns(update);

            this.mockService2 = Substitute.For<ILKForClient>();
            this.mockService2.Login("proba", "proba");
            this.mockService2.GetMySystem().Returns(update);
            this.mockService2.Update(update);

            this.client.Proxy = this.mockService;
            this.client.Registration("proba", "proba");
            this.client.Proxy = this.mockService2;
            this.client.LogIn("proba", "proba");
            this.client.Command(update);
        }

        /// <summary>
        /// test for constructor
        /// </summary>
        [Test]
        public void ConstructorTest()
        {
            Assert.DoesNotThrow(() => this.client = new LKClientService());
            Assert.AreNotEqual(null, this.client.Generators);
            Assert.AreNotEqual(null, this.client.Sites);
            Assert.AreNotEqual(null, this.client.Groups);
            Assert.AreNotEqual(null, this.client.HasMeasurments);
            Assert.AreNotEqual(null, this.client.GeneratorTypes);
            Assert.AreNotEqual(null, this.client.WorkingModes);
            Assert.AreNotEqual(null, this.client.GroupNames);
            Assert.AreNotEqual(null, this.client.SiteNames);
            Assert.AreNotEqual(null, this.client.EditGroupNames);
            Assert.AreEqual(null, this.client.DataContext);
        }

        /// <summary>
        /// test for list of generators
        /// </summary>
        [Test]
        public void GeneratorsTest()
        {
            Assert.AreNotEqual(null, this.client.Generators);
            Assert.Throws<ArgumentNullException>(() => this.client.Generators = null);
            BindingList<Generator> generators = new BindingList<Generator>();
            this.client.Generators = generators;
            Assert.AreEqual(generators, this.client.Generators);
        }

        /// <summary>
        /// test for list of sites
        /// </summary>
        [Test]
        public void SitesTest()
        {
            Assert.AreNotEqual(null, this.client.Sites);
            Assert.Throws<ArgumentNullException>(() => this.client.Sites = null);
            BindingList<Site> sites = new BindingList<Site>();
            this.client.Sites = sites;
            Assert.AreEqual(sites, this.client.Sites);
        }

        /// <summary>
        /// test for list of groups
        /// </summary>
        [Test]
        public void GroupsTest()
        {
            Assert.AreNotEqual(null, this.client.Groups);
            Assert.Throws<ArgumentNullException>(() => this.client.Groups = null);
            BindingList<Group> groups = new BindingList<Group>();
            this.client.Groups = groups;
            Assert.AreEqual(groups, this.client.Groups);
        }

        /// <summary>
        /// test for list of value in has measurement list
        /// </summary>
        [Test]
        public void HasMeasurementsTest()
        {
            Assert.AreNotEqual(null, this.client.HasMeasurments);
            Assert.Throws<ArgumentNullException>(() => this.client.HasMeasurments = null);
            BindingList<bool> hasM = new BindingList<bool>();
            this.client.HasMeasurments = hasM;
            Assert.AreEqual(hasM, this.client.HasMeasurments);
        }

        /// <summary>
        /// test for list of generator types
        /// </summary>
        [Test]
        public void GeneratorTypesTest()
        {
            Assert.AreNotEqual(null, this.client.GeneratorTypes);
            Assert.Throws<ArgumentNullException>(() => this.client.GeneratorTypes = null);
            BindingList<GeneratorType> genTypes = new BindingList<GeneratorType>();
            this.client.GeneratorTypes = genTypes;
            Assert.AreEqual(genTypes, this.client.GeneratorTypes);
        }

        /// <summary>
        /// test for list of working modes
        /// </summary>
        [Test]
        public void WorkingModeTest()
        {
            Assert.AreNotEqual(null, this.client.WorkingModes);
            Assert.Throws<ArgumentNullException>(() => this.client.WorkingModes = null);
            BindingList<WorkingMode> workModes = new BindingList<WorkingMode>();
            this.client.WorkingModes = workModes;
            Assert.AreEqual(workModes, this.client.WorkingModes);
        }

        /// <summary>
        /// test for list of group names
        /// </summary>
        [Test]
        public void GroupNamesTest()
        {
            Assert.AreNotEqual(null, this.client.GroupNames);
            Assert.Throws<ArgumentNullException>(() => this.client.GroupNames = null);
            BindingList<Group> groupNames = new BindingList<Group>();
            this.client.GroupNames = groupNames;
            Assert.AreEqual(groupNames, this.client.GroupNames);
        }

        /// <summary>
        /// test for list of site names
        /// </summary>
        [Test]
        public void SiteNamesTest()
        {
            Assert.AreNotEqual(null, this.client.SiteNames);
            Assert.Throws<ArgumentNullException>(() => this.client.SiteNames = null);
            BindingList<Site> siteNames = new BindingList<Site>();
            this.client.SiteNames = siteNames;
            Assert.AreEqual(siteNames, this.client.SiteNames);
        }

        /// <summary>
        /// test for list of group names
        /// </summary>
        [Test]
        public void EditGroupNamesTest()
        {
            Assert.AreNotEqual(null, this.client.EditGroupNames);
            Assert.Throws<ArgumentNullException>(() => this.client.EditGroupNames = null);
            BindingList<Group> editGroupName = new BindingList<Group>();
            this.client.EditGroupNames = editGroupName;
            Assert.AreEqual(editGroupName, this.client.EditGroupNames);
        }

        /// <summary>
        /// test for get and set data context
        /// </summary>
        [Test]
        public void DataContextTest()
        {
            Assert.AreEqual(null, this.client.DataContext);
            Assert.Throws<ArgumentNullException>(() => this.client.DataContext = null);
            object data = new object();
            this.client.DataContext = data;
            Assert.AreEqual(data, this.client.DataContext);
        }

        /// <summary>
        /// test method for getting group from generator id
        /// </summary>
        [Test]
        public void GetGroupFromIdTest()
        {
            Generator gen = new Generator() { GroupID = "1" };
            Generator gen2 = new Generator() { GroupID = "2" };
            Group group = new Group() { MRID = "1" };

            this.client.Generators.Add(gen);
            this.client.Generators.Add(gen2);
            this.client.Groups.Add(group);

            Assert.AreEqual(null, this.client.GetGroupFromId("2"));
            Assert.AreEqual(group.MRID, this.client.GetGroupFromId("1").MRID);
        }

        /// <summary>
        /// test method for getting site from group id
        /// </summary>
        [Test]
        public void GetSiteFromIdTest()
        {
            Group g = new Group() { MRID = "1" };
            Group g2 = new Group() { MRID = "2" };
            Site site = new Site() { MRID = "1" };

            this.client.Groups.Add(g);
            this.client.Groups.Add(g2);
            this.client.Sites.Add(site);

            Assert.AreEqual(null, this.client.GetSiteFromId("2"));
            Assert.AreEqual(site.MRID, this.client.GetSiteFromId("1").MRID);
        }

        /// <summary>
        /// test method for validation of input fields
        /// </summary>
        [Test]
        public void CheckStringInputFieldTest()
        {
            Assert.AreEqual(false, this.client.CheckStringInputField(null));
            Assert.AreEqual(false, this.client.CheckStringInputField(string.Empty));
            Assert.AreEqual(true, this.client.CheckStringInputField("string"));
        }

        /// <summary>
        /// test method for validation of input fields
        /// </summary>
        [Test]
        public void CheckDoubleInputFieldTest()
        {
            Assert.AreEqual(false, this.client.CheckDoubleInputField(null));
            Assert.AreEqual(false, this.client.CheckDoubleInputField(string.Empty));
            this.client.CheckDoubleInputField("string");
            Assert.AreEqual(false, this.client.CheckDoubleInputField("-2"));
            Assert.AreEqual(true, this.client.CheckDoubleInputField("2.0"));
        }

        /// <summary>
        /// test method for invalid data which is sent to service
        /// </summary>
        [Test]
        public void UpdateTest()
        {
            Assert.Throws<InvalidDataException>(() => this.client.Update(null));
        }

        /// <summary>
        /// test method for adding data to service
        /// </summary>
        [Test]
        public void UpdateAddTest()
        {
            this.client.Generators.Clear();
            this.client.Groups.Clear();
            this.client.Sites.Clear();
            UpdateInfo update = new UpdateInfo();
            update.UpdateType = UpdateType.ADD;
            update.Generators.Add(new Generator());
            update.Groups.Add(new Group());
            update.Sites.Add(new Site());
            this.client.Update(update);
            Assert.AreEqual(update.Generators, this.client.Generators);
            Assert.AreEqual(update.Groups, this.client.Groups);
            Assert.AreEqual(update.Sites, this.client.Sites);
        }

        /// <summary>
        /// test method for removing data to service
        /// </summary>
        [Test]
        public void UpdateDeleteTest()
        {
            this.client.Generators.Clear();
            this.client.Groups.Clear();
            this.client.Sites.Clear();

            Generator g = new Generator() { MRID = "1", GroupID = "2" };
            Group group = new Group() { MRID = "2", SiteID = "3" };
            Site site = new Site() { MRID = "3" };

            this.client.Generators.Add(g);
            this.client.Groups.Add(group);
            this.client.Sites.Add(site);

            UpdateInfo update = new UpdateInfo();
            update.UpdateType = UpdateType.REMOVE;
            update.Generators.Add(g);
            update.Groups.Add(group);
            update.Sites.Add(site);
            this.client.Update(update);
            Assert.AreEqual(0, this.client.Generators.Count);
            Assert.AreEqual(0, this.client.Groups.Count);
            Assert.AreEqual(0, this.client.Sites.Count);
        }

        /// <summary>
        /// test method for editing data to service
        /// </summary>
        [Test]
        public void UpdateEditTest()
        {
            this.client.Generators.Clear();
            this.client.Groups.Clear();
            this.client.Sites.Clear();

            Generator g = new Generator() { MRID = "1", GroupID = "2" };
            Group group = new Group() { MRID = "2", SiteID = "3" };
            Site site = new Site() { MRID = "3" };

            this.client.Generators.Add(g);
            this.client.Groups.Add(group);
            this.client.Sites.Add(site);

            UpdateInfo update = new UpdateInfo();
            update.UpdateType = UpdateType.UPDATE;

            g.Pmax = 50;

            update.Generators.Add(g);
            update.Groups.Add(new Group());
            update.Sites.Add(new Site());
            this.client.Update(update);
            Assert.AreEqual(1, this.client.Generators.Count);
            Assert.AreEqual(2, this.client.Groups.Count);
            Assert.AreEqual(2, this.client.Sites.Count);
        }

        /// <summary>
        /// test method for property changed
        /// </summary>
        [Test]
        public void RaisePropertyChangedTest()
        {
            string receivedEvents = null;

            this.client.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                receivedEvents = e.PropertyName;
            };

            this.client.Generators = new BindingList<Generator>();
            Assert.IsNotNull(receivedEvents);
            Assert.AreEqual("Generators", receivedEvents);
        }

        /// <summary>
        /// test method for get measurement which will be displayed
        /// </summary>
        [Test]
        public void GetMeasurementTest()
        {
            this.client.Generators.Clear();
            this.client.Groups.Clear();
            this.client.Sites.Clear();

            Generator g = new Generator() { MRID = "1", GroupID = "2" };
            Group group = new Group() { MRID = "2", SiteID = "3" };
            Site site = new Site() { MRID = "3" };

            this.client.Generators.Add(g);
            this.client.Groups.Add(group);
            this.client.Sites.Add(site);

            this.mockService = Substitute.For<ILKForClient>();
            List<KeyValuePair<DateTime, double>> temp = new List<KeyValuePair<DateTime, double>>();

            this.mockService.GetMeasurements("1").Returns(temp);
            this.client.Proxy = this.mockService;
            this.client.GetMeasurements("1");
        }
    }
}
