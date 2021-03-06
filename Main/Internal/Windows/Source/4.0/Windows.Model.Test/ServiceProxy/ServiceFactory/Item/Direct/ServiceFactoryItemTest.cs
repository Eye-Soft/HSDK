﻿namespace EyeSoft.Windows.Model.Test.ServiceProxy.ServiceFactory.Item.Direct
{
    using System.Threading;
    using Demo.Configuration.Helpers;
    using Demo.Contract;
    using Demo.ViewModels;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class ServiceFactoryItemTest : ServiceFactoryTest
	{
		[TestMethod]
		public void ServiceFactoryGetItemWithCompletedActionVerifyWorks()
		{
			CustomerDto customer = null;

			var manualResetEvent = new ManualResetEvent(false);

			factoryHelper.ServiceFactory
				.Fill(x => x.GetMainCustomer())
				.Completed(
					x =>
						{
							customer = x;
							manualResetEvent.Set();
						});

			manualResetEvent.WaitOne();

			customer.Should().Be(Known.Customer.Main);

			factoryHelper.ItemLoaded.Should().BeTrue();
		}

		[TestMethod]
		public void ServiceFactoryGetItemWithoutCompletedActionVerifyServiceIsNotCalled()
		{
			factoryHelper.ServiceFactory.Fill(x => x.GetMainCustomer());

			factoryHelper.ItemLoaded.Should().BeFalse();
		}

		[TestMethod]
		public void ServiceFactoryGetItemWithoutCompletedAncConversioneActionVerifyWorks()
		{
		    CustomerViewModel model = null;

			var manuelResetEvent = new ManualResetEvent(false);

			factoryHelper
				.ServiceFactory
				.Fill(x => x.GetMainCustomer())
				.Completed<CustomerViewModel>(
					x =>
					{
						model = x;
						manuelResetEvent.Set();
					});

			manuelResetEvent.WaitOne();

			model.Should().Be(Known.CustomerModel.Main);
		}
	}
}