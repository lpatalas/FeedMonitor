using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using FakeItEasy;
using FeedMonitor.Services;
using FeedMonitor.UnitTests.Fakes;
using FeedMonitor.ViewModels;
using FluentAssertions;
using Xunit;

namespace FeedMonitor.UnitTests.Services
{
	public class MessageBoxServiceClass
	{
		public abstract class BaseTest
		{
			protected readonly MessageBoxService messageBoxService;
			protected readonly FakeWindowManager windowManager;

			public BaseTest()
			{
				windowManager = new FakeWindowManager();
				messageBoxService = new MessageBoxService(windowManager);
			}
		}

		public class ShowYesNoDialogMethod : BaseTest
		{
			[Fact]
			public void Should_pass_MessageBoxViewModel_instance_as_rootModel_to_window_manager()
			{
				// Arrange

				// Act
				messageBoxService.ShowYesNoDialog("A", "B");

				// Assert
				windowManager.DialogRootModel.Should().BeOfType<MessageBoxViewModel>();
			}

			[Fact]
			public void Should_return_false_when_user_selects_button_mapped_to_No_message_box_result()
			{
				// Arrange
				windowManager.OverrideDialogResult(MessageBoxResult.No);

				// Act
				var result = messageBoxService.ShowYesNoDialog("A", "B");

				// Assert
				result.Should().BeFalse();
			}

			[Fact]
			public void Should_return_true_when_user_selects_button_mapped_to_Yes_message_box_result()
			{
				// Arrange
				windowManager.OverrideDialogResult(MessageBoxResult.Yes);

				// Act
				var result = messageBoxService.ShowYesNoDialog("A", "B");

				// Assert
				result.Should().BeTrue();
			}

			[Fact]
			public void Should_set_correct_message_and_title_in_MessageBoxViewModel()
			{
				// Arrange
				var message = "Message";
				var title = "Title";

				// Act
				messageBoxService.ShowYesNoDialog(message, title);

				// Assert
				var viewModel = (MessageBoxViewModel)windowManager.DialogRootModel;

				viewModel.Message.Should().Be(message);
				viewModel.Title.Should().Be(title);
			}

			[Fact]
			public void Should_show_dialog_using_IWindowManager()
			{
				// Arrange

				// Act
				messageBoxService.ShowYesNoDialog("A", "B");

				// Assert
				windowManager.WasShowDialogCalled.Should().BeTrue();
			}

			[Fact]
			public void Should_specify_two_buttons_with_Yes_and_No_results()
			{
				// Arrange

				// Act
				messageBoxService.ShowYesNoDialog("A", "B");

				// Assert
				var viewModel = (MessageBoxViewModel)windowManager.DialogRootModel;

				viewModel.Buttons
					.Should().HaveCount(2)
					.And.Contain(mapping => mapping.Result == MessageBoxResult.Yes)
					.And.Contain(mapping => mapping.Result == MessageBoxResult.No);
			}
		}
	}
}
