using ModernDesign.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ModernDesign.MVVM.ViewModel
{
    internal class MainViewModel: ObservableObject
    {



       public RelayCommand WatchlistViewCommand {  get; set; }
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand DiscoveryViewCommand { get; set; }
        public RelayCommand FeaturedViewCommand { get; set; }


        public HomeViewModel HomeVm { get; set; }
        private object _currentView;

        public DiscoveryViewModel DiscoveryVm { get; set; }
        public VisualsViewModel FeaturedVm { get; set; }

        public WatchlistViewModel WatchlistVm { get; set; }


        public object CurrentView
        {
            get { return _currentView; }
            set
            { _currentView = value;
                OnPropertyChanged();
            }
        }


      

        //Creating an new view
        public MainViewModel() 
        {
            HomeVm = new HomeViewModel();
            DiscoveryVm = new DiscoveryViewModel();
            FeaturedVm = new VisualsViewModel();
            WatchlistVm = new WatchlistViewModel();
            CurrentView = HomeVm;




           

            HomeViewCommand = new RelayCommand(o => 
            {

                CurrentView = HomeVm;
             });

           DiscoveryViewCommand = new RelayCommand(o =>
            {

                CurrentView = DiscoveryVm;
            });


            FeaturedViewCommand = new RelayCommand(o =>
            {

                CurrentView = FeaturedVm;
            });

            WatchlistViewCommand = new RelayCommand(o => 
            {
                CurrentView = WatchlistVm;

            });

          





        }
      
    }
}
