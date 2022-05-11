using AutoMapper;
using Caliburn.Micro;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TRMDesktopUI.Helpers;
using TRMDesktopUI.Library.Api;
using TRMDesktopUI.Library.Models;
using TRMDesktopUI.Models;
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

		private IMapper ConfigureAutompper()
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<ProductModel, ProductDisplayModel>();
				cfg.CreateMap<CartItemModel, CartItemDisplayModel>();
			});

			var output = config.CreateMapper();

			return output;
		}

		private IConfiguration AddConfiguration()
		{
			IConfigurationBuilder builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json");

#if DEBUG
			builder.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
#else
			builder.AddJsonFile("appsettings.Production.json", optional: true, reloadOnChange: true);
#endif
			return builder.Build();
		}
		protected override void Configure()
		{
			_container.Instance(ConfigureAutompper());

			_container.Instance(_container)
				.PerRequest<IProductEndpoint, ProductEndpoint>()
				.PerRequest<IUserEndpoint, UserEndpoint>()
				.PerRequest<ISaleEndpoint, SaleEndpoint>();

			_container
				.Singleton<IWindowManager, WindowManager>()
				.Singleton<IEventAggregator, EventAggregator>()
				.Singleton<ILoggedInUserModel, LoggedInUserModel>()
				.Singleton<IAPIHelper, APIHelper>();

			_container.RegisterInstance(typeof(IConfiguration), "IConfiguration", AddConfiguration());

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
