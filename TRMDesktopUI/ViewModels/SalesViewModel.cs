using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDesktopUI.Library.Api;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.ViewModels
{
	public class SalesViewModel : Screen
	{
		private BindingList<ProductModel> _products;
		private int _itemQuantity = 1;
		private BindingList<CartItemModel> _cart = new BindingList<CartItemModel>();
		private readonly IProductEndpoint _productEndpoint;


		private ProductModel _selectedProduct;

		public ProductModel SelectedProduct
		{
			get { return _selectedProduct; }
			set 
			{ 
				_selectedProduct = value;
				NotifyOfPropertyChange(() => SelectedProduct);
				NotifyOfPropertyChange(() => CanAddToCart);
			
			}
		}


		public BindingList<ProductModel> Products
		{
			get { return _products; }
			set
			{
				_products = value;
				NotifyOfPropertyChange(() => Products);
			}
		}
		public int ItemQuantity
		{
			get { return _itemQuantity; }
			set
			{
				_itemQuantity = value;
				NotifyOfPropertyChange(() => ItemQuantity);
				NotifyOfPropertyChange(() => CanAddToCart);
			}
		}
		public BindingList<CartItemModel> Cart
		{
			get { return _cart; }
			set
			{
				_cart = value;
				NotifyOfPropertyChange(() => Cart);
			}
		}

		public string SubTotal
		{
			get
			{
				decimal subTotal = 0;
				foreach (var item in Cart)
				{
					subTotal += (item.Product.RetailPrice * item.QuantityInCart);
				}
				return subTotal.ToString("C");
			}
		}
		public string Tax
		{
			get
			{
				return "$0.00";
			}
		}
		public string Total
		{
			get
			{
				return "$0.00";
			}
		}

		public bool CanAddToCart
		{
			get
			{
				bool output = false;

				if (ItemQuantity > 0 && SelectedProduct?.QuantityInStock >= ItemQuantity)
				{
					output = true;
				}

				return output;
			}
		}
		public bool CanRemoveFromCart
		{
			get
			{
				bool output = false;

				// Make sure something is selected


				return output;
			}
		}

		public bool CanCheckOut
		{
			get
			{
				bool output = false;

				// Make sure something is in the cart


				return output;
			}
		}

		public SalesViewModel(IProductEndpoint productEndpoint)
		{
			_productEndpoint = productEndpoint;
		}

		protected override async void OnViewLoaded(object view)
		{
			base.OnViewLoaded(view);
			await LoadProducts();
		}

		private async Task LoadProducts()
		{
			var productList = await _productEndpoint.GetAll();
			Products = new BindingList<ProductModel>(productList);
		}

		public void AddToCart()
		{
			CartItemModel existingItem = Cart.FirstOrDefault(x => x.Product == SelectedProduct);

			if (existingItem != null)
			{
				existingItem.QuantityInCart += ItemQuantity;

			}
			else
			{
				CartItemModel item = new CartItemModel
				{
					Product = SelectedProduct,
					QuantityInCart = ItemQuantity,
				};
				Cart.Add(item);
			}

			SelectedProduct.QuantityInStock -= ItemQuantity;
			ItemQuantity = 1;
			NotifyOfPropertyChange(() => SubTotal);
			NotifyOfPropertyChange(() => Cart);
		}

		public void RemoveFromCart()
		{
			NotifyOfPropertyChange(() => SubTotal);

		}

		public void CheckOut()
		{

		}


	}
}
