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
		private SalesViewModel _salesVM;
		private IEventAggregator _events;

		public ShellViewModel(IEventAggregator events, SalesViewModel salesVM)
		{
			_events = events;
			_salesVM = salesVM;


			_events.SubscribeOnPublishedThread(this);
			
			ActivateItemAsync(IoC.Get<LoginViewModel>());
		}

		public Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
		{
			return ActivateItemAsync(_salesVM);
		}
	}
}
