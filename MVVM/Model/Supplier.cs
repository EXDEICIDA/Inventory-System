using System;
using System.ComponentModel;

namespace ModernDesign.MVVM.Model
{
    public class Supplier : INotifyPropertyChanged 
    {
        private int _supplierID;
        private string _name;
        private string _address;
        private string _contactInfo;

        public int SupplierID
        {
            get { return _supplierID; }
            set
            {
                _supplierID = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }

        public string ContactInfo
        {
            get { return _contactInfo; }
            set
            {
                _contactInfo = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
