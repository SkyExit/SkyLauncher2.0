using SkyLauncherRemastered.Core;

namespace SkyLauncherRemastered.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand ProjectViewCommand { get; set; }
        public RelayCommand EmojiViewCommand { get; set; }
        public RelayCommand ValorantViewCommand { get; set; }
        public RelayCommand TextEmojiViewCommand { get; set; }
        public RelayCommand MW2ViewCommand { get; set; }

        public HomeViewModel HomeViewModel { get; set; }
        public ProjectViewModel ProjectViewModel { get; set; }
        public EmojiViewModel EmojiViewModel { get; set; }
        public ValorantViewModel ValorantViewModel { get; set; }
        public TextEmojiViewModel TextEmojiViewModel { get; set; }
        public MW2ViewModel MW2ViewModel { get; set; }

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
            EmojiViewModel = new EmojiViewModel();
            ValorantViewModel = new ValorantViewModel();
            TextEmojiViewModel = new TextEmojiViewModel();
            MW2ViewModel = new MW2ViewModel();

            CurrentView = HomeViewModel;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeViewModel;
            });

            ProjectViewCommand = new RelayCommand(o =>
            {
                CurrentView = ProjectViewModel;
            });

            EmojiViewCommand = new RelayCommand(o =>
            {
                CurrentView = EmojiViewModel;
            });

            ValorantViewCommand = new RelayCommand(o =>
            {
                CurrentView = ValorantViewModel;
            });

            TextEmojiViewCommand = new RelayCommand(o =>
            {
                CurrentView = TextEmojiViewModel;
            });

            MW2ViewCommand = new RelayCommand(o =>
            {
                CurrentView = MW2ViewModel;
            });
        }
    }
}
