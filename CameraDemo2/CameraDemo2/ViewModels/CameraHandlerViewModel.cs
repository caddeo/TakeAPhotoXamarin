using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CameraDemo2.Annotations;
using CameraDemo2.ViewModels;
using CameraDemo2.Views;
using Plugin.Media;
using Xamarin.Forms;

namespace CameraDemo2
{
    public class CameraHandlerViewModel : INotifyPropertyChanged
    {

        private INavigation _navigation;
        private bool _loading;
        public bool IsLoading
        {
            get
            {
                return _loading;
            }
            set
            {
                _loading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        public CameraHandlerViewModel(INavigation navigation)
        {
            _navigation = navigation;
            LoadData();
        }

        public async Task LoadData()
        {
            this.IsLoading = true;
            var photo = await TakePhoto();
            this.IsLoading = false;

            if (photo == null)
            {
                return;
            }

            var vm = new PictureFinalizingViewModel(_navigation, photo);
            await _navigation.PushAsync(new PictureFinalizingView(vm));
            return;
        }

        private async Task<Stream> TakePhoto()
        {
            var pictureTaken = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "PictureAppDir",
                Name = Guid.NewGuid().ToString()
            });

            // Error - display error message

            var stream = pictureTaken?.GetStream();
            return stream;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}