using CommonLibrary;
using CommonLibrary.Interfaces;
using KLRESClient;
using NSubstitute;
using NUnit.Framework;
using Prism.Commands;
using System;
using System.ServiceModel;

namespace LKResClientTest
{
    [TestFixture]
    public class MasterViewModelTest
    {
        #region Declarations

        private MasterViewModel masterVM;
        /// <summary>
        /// Represent view model for add actions
        /// </summary>
        /// 
        private AddWindowViewModel addWindowVM = null;

        /// <summary>
        /// Represent view model for edit and remove actions
        /// </summary>
        private EditRemoveViewModel editRemoveWindowVM = null;

        /// <summary>
        /// Represent instance of client
        /// </summary>
        private LKClientService client = null;

        /// <summary>
        /// exit command
        /// </summary>
        private DelegateCommand clickExitCommand;

        /// <summary>
        /// Use for closing MainWindow
        /// </summary>
        private MainWindow mainWindow = null;

        private ILKForClient mockService = null;

        #endregion

        [OneTimeSetUp]
        public void SetupTest()
        {
            //Assert.Throws<EndpointNotFoundException>(() => this.masterVM = new MasterViewModel(this.mainWindow));
            //this.mainWindow = new MainWindow();

            this.masterVM = new MasterViewModel(this.mainWindow);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.DoesNotThrow(() => this.masterVM = new MasterViewModel(this.mainWindow));
            Assert.AreNotEqual(null, this.masterVM.AddWindowVM);
            Assert.AreNotEqual(null, this.masterVM.EditRemoveWindowVM);
            Assert.AreNotEqual(null, this.masterVM.Client);
            //Assert.Throws<ArgumentNullException>(() => new MasterViewModel(null));
        }

        [Test]
        public void ClientTest()
        {
            Assert.AreNotEqual(null, this.masterVM.Client);
            Assert.Throws<ArgumentNullException>(() => this.masterVM.Client = null);
            LKClientService client2 = new LKClientService();
            this.masterVM.Client = client2;
            Assert.AreEqual(client2, this.masterVM.Client);
        }

        [Test]
        public void AddWindowVMTest()
        {
            Assert.AreNotEqual(null, this.masterVM.AddWindowVM);
            Assert.Throws<ArgumentNullException>(() => this.masterVM.AddWindowVM = null);
            AddWindowViewModel add = new AddWindowViewModel(this.client);
            this.masterVM.AddWindowVM = add;
            Assert.AreEqual(add, this.masterVM.AddWindowVM);
        }

        [Test]
        public void EditRemoveWindowVMTest()
        {
            Assert.AreNotEqual(null, this.masterVM.EditRemoveWindowVM);
            Assert.Throws<ArgumentNullException>(() => this.masterVM.EditRemoveWindowVM = null);
            EditRemoveViewModel viewModel = new EditRemoveViewModel(this.client);
            this.masterVM.EditRemoveWindowVM = viewModel;
            Assert.AreEqual(viewModel, this.masterVM.EditRemoveWindowVM);
        }

        [Test]
        public void ExitCommandTest()
        {
            Assert.AreNotEqual(null, this.masterVM.ExitCommand);
        }
    }
}
