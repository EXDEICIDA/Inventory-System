using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using ModernDesign.Core;
using ModernDesign.MVVM.Model;
using DataLayer;

namespace ModernDesign.MVVM.ViewModel
{
    public class WatchlistViewModel : INotifyPropertyChanged
    {
        private readonly SuppliersDataAccess _suppliersDataAccess;
        private ObservableCollection<Supplier> _suppliers;
        private Supplier _selectedSupplier;

        public ObservableCollection<Supplier> Suppliers
        {
            get => _suppliers;
            set
            {
                _suppliers = value;
                OnPropertyChanged();
            }
        }

        public Supplier SelectedSupplier
        {
            get => _selectedSupplier;
            set
            {
                _selectedSupplier = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }

        public WatchlistViewModel()
        {
            _suppliersDataAccess = new SuppliersDataAccess();
            LoadSuppliers();

            AddCommand = new RelayCommand(AddSupplier);
            DeleteCommand = new RelayCommand(DeleteSupplier);
            EditCommand = new RelayCommand(EditSupplier);
        }

        private void LoadSuppliers()
        {
            Suppliers = new ObservableCollection<Supplier>(_suppliersDataAccess.GetAllSuppliers());
        }

        private void AddSupplier(object obj)
        {
            if (SelectedSupplier != null)
            {
                if (string.IsNullOrWhiteSpace(SelectedSupplier.Name))
                {
                    MessageBox.Show("Name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    _suppliersDataAccess.AddSupplier(SelectedSupplier);
                    Suppliers.Add(SelectedSupplier);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding supplier: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Selected supplier is null.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteSupplier(object parameter)
        {
            if (SelectedSupplier != null)
            {
                try
                {
                    _suppliersDataAccess.DeleteSupplier(SelectedSupplier.SupplierID);
                    Suppliers.Remove(SelectedSupplier);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting supplier: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("No supplier selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditSupplier(object obj)
        {
            if (SelectedSupplier != null)
            {
                try
                {
                    _suppliersDataAccess.UpdateSupplier(SelectedSupplier);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error editing supplier: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("No supplier selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
