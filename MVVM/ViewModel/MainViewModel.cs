using SkyLauncherRemastered.Core;

namespace SkyLauncherRemastered.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand ProjectViewCommand { get; set; }

        public HomeViewModel HomeViewModel { get; set; }
        public ProjectViewModel ProjectViewModel { get; set; }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; onPropertyChanged(); }
        } 

        public MainViewModel()
        {
            HomeViewModel = new HomeViewModel();
            ProjectViewModel = new ProjectViewModel();
            CurrentView = HomeViewModel;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeViewModel;
            });

            ProjectViewCommand = new RelayCommand(o =>
            {
                CurrentView = ProjectViewModel;
            }); ;
        }
    }
}
