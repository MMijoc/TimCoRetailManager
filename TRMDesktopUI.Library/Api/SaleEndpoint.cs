using System;
using System.Net.Http;
using System.Threading.Tasks;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.Library.Api
{
	public class SaleEndpoint : ISaleEndpoint
	{
		private readonly IAPIHelper _apiHelper;

		public SaleEndpoint(IAPIHelper apiHelper)
		{
			_apiHelper = apiHelper;
		}

		public async Task PostSale(SaleModel sale)
		{
			using (HttpResponseMessage respons = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Sale", sale))
			{
				if (respons.IsSuccessStatusCode)
				{
					//Log successful call?
				}
				else
				{
					throw new Exception(respons.ReasonPhrase);
				}
			}
		}
	}
}
