using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using System.Windows.Input;
using Plugin.Media;
using System.IO;
using System.Runtime.CompilerServices;
using CameraDemo2.Annotations;
using CameraDemo2.Models;
using CameraDemo2.Services;
using CameraDemo2.Views;

namespace CameraDemo2
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private INavigation _navigation;
        private ImageSource _sourceImage { get; set; }

        public ICommand NavigateCommand { get; protected set; }

        public bool IsPhotoPossible => CanTakePhoto();

        public ImageSource SourceImage
        {
            get
            {
                return _sourceImage;
            }
            set
            {
                _sourceImage = value;
                OnPropertyChanged(nameof(SourceImage));
            }
        }

        private ObservableCollection<ImageEntry> _entries;

        public ObservableCollection<ImageEntry> Entries
        {
            get { return _entries; }
            set
            {
                _entries = value;
                OnPropertyChanged(nameof(Entries));
            }
        }

        public MainPageViewModel(INavigation navigation)
        {
            // show loading
            //LoadData().Wait();

            this._navigation = navigation ;
            this.NavigateCommand = new Command(async () => await LaunchNextWindow(), () => IsPhotoPossible);
        }

        private async Task LoadData()
        {
            var imagesEncoded = await ApiService.GetImagesConverted();
            var images = new ObservableCollection<ImageEntry>();

            foreach (var image in imagesEncoded)
            {
                var source = ConvertService.ImageFromBase64(image.ImageConverted);
                var text = image.Text;
                images.Add(new ImageEntry() { Text = text, SelectedSource = source});
            }

            _entries = images;

        }

        private async Task LaunchNextWindow()
        {
            var vm = new CameraHandlerViewModel(_navigation);
            await _navigation.PushAsync(new CameraHandlerView(_navigation));
        }

        public bool CanTakePhoto()
        { 
            return (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported) ? false : true;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
