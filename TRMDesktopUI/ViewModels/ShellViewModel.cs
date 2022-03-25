using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using TRMDesktopUI.EventModels;
using TRMDesktopUI.Library.Models;
using TRMDesktopUI.Library.Api;

namespace TRMDesktopUI.ViewModels
{
	public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
	{
		private readonly SalesViewModel _salesVM;
		private readonly ILoggedInUserModel _user;
		private readonly IAPIHelper _apiHelper;
		private readonly IEventAggregator _events;

		
		public bool IsLoggedIn
		{
			get
			{
				bool output = false;

				if (string.IsNullOrEmpty(_user.Token) == false)
				{
					output = true;
				}

				return output;
			}

		}

		public ShellViewModel(IEventAggregator events, SalesViewModel salesVM, ILoggedInUserModel user, IAPIHelper apiHelper)
		{
			_events = events;
			_salesVM = salesVM;
			_user = user;
			_apiHelper = apiHelper;
			_events.SubscribeOnPublishedThread(this);
			
			ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());
		}
		public async Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
		{
			await ActivateItemAsync(_salesVM, cancellationToken);
			NotifyOfPropertyChange(() => IsLoggedIn);
		}

		public async Task UserManagement()
		{
			await ActivateItemAsync(IoC.Get<UserDisplayViewModel>(), new CancellationToken());
		}

		public async Task LogOut()
		{
			_user.ResetUserModel();
			_apiHelper.LogOffUser();
			await ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());
			NotifyOfPropertyChange(() => IsLoggedIn);
		}
		public void ExitApplication()
		{
			TryCloseAsync();
		}

	}
}
