using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using TRMDesktopUI.EventModels;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.ViewModels
{
	public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
	{
		private SalesViewModel _salesVM;
		private readonly ILoggedInUserModel _user;
		private IEventAggregator _events;

		
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

		public ShellViewModel(IEventAggregator events, SalesViewModel salesVM, ILoggedInUserModel user)
		{
			_events = events;
			_salesVM = salesVM;
			_user = user;
			_events.SubscribeOnPublishedThread(this);
			
			ActivateItemAsync(IoC.Get<LoginViewModel>());
		}
		public Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
		{
			var task = ActivateItemAsync(_salesVM);

			NotifyOfPropertyChange(() => IsLoggedIn);

			return task;
		}

		public void LogOut()
		{
			_user.LoogOffUser();
			ActivateItemAsync(IoC.Get<LoginViewModel>());
			NotifyOfPropertyChange(() => IsLoggedIn);
		}
		public void ExitApplication()
		{
			TryCloseAsync();
		}

	}
}
