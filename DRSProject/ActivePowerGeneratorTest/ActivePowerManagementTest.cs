using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CommonLibrary;
using CommonLibrary.Interfaces;
using CommonLibrary.Exceptions;
using LKRes.Services;
using System.ServiceModel;
using NSubstitute;
using KSRes;
using System.Threading;
using ActivePowerGenerator;

namespace LKResTest.ServicesTest
{
    [TestFixture]
    public class ActivePowerGeneratorTest
    {
        private ActivePowerManagement management = null;

        [OneTimeSetUp]
        public void SetUpTest()
        {
            this.management = new ActivePowerManagement();
        }

        [Test]
        public void ChangeActivePower01()
        {
            UpdateInfo update = new UpdateInfo();

            Group group = new Group() { MRID = "2" };

            Generator generator = new Generator()
            {
                MRID = "1",
                GroupID = group.MRID,
                SetPoint = -1,
                HasMeasurment = true,
                Pmax = 20,
                Pmin = 1,
                ActivePower = 8
            };
            update.Generators.Add(generator);
            update.Groups.Add(group);

            management.ChangeActivePower(ref update, 0);

            update.Generators[0].HasMeasurment = false;
            update.Generators[0].SetPoint = 5;
            management.ChangeActivePower(ref update, 1);
        }

        [Test]
        public void ChangeActivePower02()
        {
            UpdateInfo update = new UpdateInfo();

            Group group = new Group() { MRID = "2" };

            Generator generator = new Generator()
            {
                MRID = "1",
                GroupID = group.MRID,
                SetPoint = -1,
                HasMeasurment = true,
                Pmax = 20,
                Pmin = 1,
                ActivePower = 8
            };
            update.Generators.Add(generator);
            update.Groups.Add(group);

            management.ChangeActivePower(ref update, 1);
        }

        [Test]
        public void ChangeActivePower03()
        {
            UpdateInfo update = new UpdateInfo();

            Group group = new Group() { MRID = "2" };

            Generator generator = new Generator()
            {
                MRID = "1",
                GroupID = group.MRID,
                SetPoint = -1,
                HasMeasurment = false,
                Pmax = 20,
                Pmin = 1,
                ActivePower = 8
            };
            update.Generators.Add(generator);
            update.Groups.Add(group);

            management.ChangeActivePower(ref update, 1);
        }

        [Test]
        public void ChangeActivePower04()
        {
            UpdateInfo update = new UpdateInfo();

            Group group = new Group() { MRID = "2" };

            Generator generator = new Generator()
            {
                MRID = "1",
                GroupID = group.MRID,
                SetPoint = -1,
                HasMeasurment = true,
                Pmax = 20,
                Pmin = 1,
                ActivePower = 30
            };
            update.Generators.Add(generator);
            update.Groups.Add(group);

            management.ChangeActivePower(ref update, 1);
            management.ChangeActivePower(ref update, 0);
        }

        [Test]
        public void ChangeActivePower05()
        {
            UpdateInfo update = new UpdateInfo();

            Group group = new Group() { MRID = "2" };

            Generator generator = new Generator()
            {
                MRID = "1",
                GroupID = group.MRID,
                SetPoint = -1,
                HasMeasurment = false,
                Pmax = 20,
                Pmin = 0,
                ActivePower = 8
            };

            Generator generator2 = new Generator()
            {
                MRID = "2",
                GroupID = group.MRID,
                SetPoint = -1,
                HasMeasurment = true,
                Pmax = 20,
                Pmin = 0,
                ActivePower = 8
            };
            update.Generators.Add(generator);
            update.Generators.Add(generator2);
            update.Groups.Add(group);

            management.ChangeActivePower(ref update, 1);
        }
    }
}
