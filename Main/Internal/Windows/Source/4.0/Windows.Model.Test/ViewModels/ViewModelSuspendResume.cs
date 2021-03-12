﻿namespace EyeSoft.Windows.Model.Test.ViewModels
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EyeSoft.Windows.Model;
    using FluentAssertions;

	[TestClass]
	public class ViewModelSuspendResume
	{
		[TestMethod]
		public void AssignPropertyWhenPropertyChangedIsSupspendedVerifyChangesIsZero()
		{
			var viewModel = new MainViewModel { Customer = { Name = "Value1" } };

			const string LastValue = "Value2";

			viewModel.Customer.Name = LastValue;

			viewModel.Customer.ResumePropertyChanged();

			viewModel.Customer.Changed.Should().BeFalse();
			viewModel.Customer.Changes.Should().Be(0);

			viewModel.Customer.Name = LastValue;
			viewModel.Customer.Changed.Should().BeFalse();
			viewModel.Customer.Changes.Should().Be(0);

			viewModel.Customer.Name = "Value3";
			viewModel.Customer.Changed.Should().BeTrue();
			viewModel.Customer.Changes.Should().Be(1);
		}

		private class MainViewModel
		{
			public MainViewModel()
			{
				Customer = new CustomerViewModel();
			}

			public CustomerViewModel Customer { get; }
		}

		private class CustomerViewModel : ViewModel
		{
			public CustomerViewModel()
			{
				SuspendPropertyChanged();
			}

			public string Name
			{
				get => GetProperty<string>();
                set => SetProperty(value);
            }
		}
	}
}