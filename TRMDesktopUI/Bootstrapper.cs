using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TRMDesktopUI.Library.Api;
using TRMDesktopUI.Library.Models;
using TRMDesktopUI.ViewModels;

namespace TRMDesktopUI
{
	public class Bootstrapper : BootstrapperBase
	{
		private SimpleContainer _container = new SimpleContainer();
		public Bootstrapper()
		{
			Initialize();

			ConventionManager.AddElementConvention<PasswordBox>(
				PasswordBoxHelper.BoundPasswordProperty,
				"Password",
				"PasswordChanged");
		}


		protected override void Configure()
		{
			// Whenever we ask for container instance this instance will be returned
			// Object kinda holds the instance of itself
			_container.Instance(_container)
				.PerRequest<IProductEndpoint, ProductEndpoint>();

			// Singleton is instance of an object who's lifetime is same as the lifetime of application
			// Note on D.I.: If someone asks for the instance of a IWindowManager he will receive an instance of WindowMahager
			// If some on asks again for the instance of IWindowManager because it is a singleton it will return reference to the
			// the same (already existing) instance of WindowManager
			_container
				.Singleton<IWindowManager, WindowManager>()
				.Singleton<IEventAggregator, EventAggregator>()
				.Singleton<ILoggedInUserModel, LoggedInUserModel>()
				.Singleton<IAPIHelper, APIHelper>();


			// This means that for every assembly that is a Class and it's name ends with ViewModel put them in a list
			// And then for each element from that list register it per request (give new instance when requested) 
			GetType().Assembly.GetTypes()
				.Where(t => t.IsClass)
				.Where(t => t.Name.EndsWith("ViewModel"))
				.ToList()
				.ForEach(viewModelType => _container.RegisterPerRequest(
					viewModelType, viewModelType.ToString(), viewModelType));
		}

		protected override void OnStartup(object sender, StartupEventArgs e)
		{
			DisplayRootViewFor<ShellViewModel>();
		}

		protected override object GetInstance(Type service, string key)
		{
			return _container.GetInstance(service, key);
		}

		protected override IEnumerable<object> GetAllInstances(Type service)
		{
			return _container.GetAllInstances(service);
		}

		protected override void BuildUp(object instance)
		{
			_container.BuildUp(instance);
		}
	}
}
