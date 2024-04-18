using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using ModernDesign.Core;
using ModernDesign.MVVM.Model;

namespace ModernDesign.MVVM.ViewModel
{
    internal class DiscoveryViewModel : INotifyPropertyChanged
    {
        private readonly DataAccessLayer _dataAccessLayer;
        private ObservableCollection<Product> _products;
        private Product _selectedProduct;

        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }

        public DiscoveryViewModel()
        {
            _dataAccessLayer = new DataAccessLayer();
            LoadProducts();

            AddCommand = new RelayCommand(AddProduct);
            DeleteCommand = new RelayCommand(DeleteProduct);
            EditCommand = new RelayCommand(EditProduct);
        }

        private void LoadProducts()
        {
            Products = new ObservableCollection<Product>(_dataAccessLayer.GetProducts());
        }

        private void AddProduct(object obj)
        {
            if (SelectedProduct != null)
            {
                if (string.IsNullOrWhiteSpace(SelectedProduct.Name))
                {
                    MessageBox.Show("Name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    var newProduct = new Product
                    {
                        Name = SelectedProduct.Name,
                        Price = SelectedProduct.Price,
                        Description = SelectedProduct.Description,
                        Quantity = SelectedProduct.Quantity,
                        Category = SelectedProduct.Category
                    };

                    _dataAccessLayer.AddProduct(newProduct);
                    Products.Add(newProduct);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Selected product is null.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteProduct(object parameter)
        {
            if (SelectedProduct != null)
            {
                try
                {
                    _dataAccessLayer.DeleteProduct(SelectedProduct); // Pass SelectedProduct as argument
                    Products.Remove(SelectedProduct);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("No product selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void EditProduct(object obj)
        {
            if (SelectedProduct != null)
            {
                try
                {
                    _dataAccessLayer.UpdateProduct(SelectedProduct);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error editing product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("No product selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
