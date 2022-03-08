using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using TRMDesktopUI.EventModels;

namespace TRMDesktopUI.ViewModels
{
	public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
	{
		private LoginViewModel _loginVM;
		private IEventAggregator _events;
		private SalesViewModel _salesVM;
		private SimpleContainer _container;

		public ShellViewModel(IEventAggregator events, SalesViewModel salesVM, SimpleContainer container)
		{
			_events = events;
			_salesVM = salesVM;
			_container = container;

			_events.SubscribeOnPublishedThread(this);
			
			ActivateItemAsync(_container.GetInstance<LoginViewModel>());
		}

		public Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
		{
			return ActivateItemAsync(_salesVM);
		}
	}
}
