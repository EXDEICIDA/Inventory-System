using System;
using System.Windows.Input;
using ModernDesign.MVVM.Model;
using ModernDesign.MVVM.ViewModel;

namespace ModernDesign.Core
{
    internal static class ButtonRelayCommands
    {
        // Relay command for the Add button
        public static readonly ICommand AddCommand = new RelayCommand(AddExecute);

        // Relay command for the Delete button
        public static readonly ICommand DeleteCommand = new RelayCommand(DeleteExecute, CanDeleteExecute);

        // Relay command for the Edit button
        public static readonly ICommand EditCommand = new RelayCommand(EditExecute, CanEditExecute);

        // Method to execute when the Add button is clicked
        private static void AddExecute(object parameter)
        {
            // Implement logic to handle adding functionality here
            Console.WriteLine("Add button clicked");
        }

        // Method to execute when the Delete button is clicked
        private static void DeleteExecute(object parameter)
        {
            // Check if the parameter is a Product object
            if (parameter is Product product)
            {
                // Get the current ViewModel
                DiscoveryViewModel viewModel = App.Current.MainWindow.DataContext as DiscoveryViewModel;

                // Remove the product from the ObservableCollection
                viewModel?.Products.Remove(product);

                // Delete product from the database
                viewModel?.DataAccessLayer.DeleteProduct(product);
            }
        }

        // Method to determine if the Delete button can be executed
        private static bool CanDeleteExecute(object parameter)
        {
            // Implement logic to determine if deletion is allowed
            return true; // Placeholder, change as needed
        }

        // Method to execute when the Edit button is clicked
        private static void EditExecute(object parameter)
        {
            // Implement logic to handle editing functionality here
            Console.WriteLine("Edit button clicked");
        }

        // Method to determine if the Edit button can be executed
        private static bool CanEditExecute(object parameter)
        {
            // Implement logic to determine if editing is allowed
            return true; // Placeholder, change as needed
        }
    }
}
