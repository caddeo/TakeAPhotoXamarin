using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CameraDemo2.Annotations;
using Xamarin.Forms;
using System.IO;
using CameraDemo2.Services;

namespace CameraDemo2.ViewModels 
{
    public class PictureFinalizingViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;
        private ImageSource _selectedImageSource;
        public bool _loading;

        public bool IsLoading
        {
            get { return _loading; }
            set
            {
                _loading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public ImageSource SelectedImageSource
        {
            get { return _selectedImageSource; }
            set
            {
                this._selectedImageSource = value;
                OnPropertyChanged(nameof(SelectedImageSource));
            }
        }

        private string _imageText;
        public string ImageText
        {
            get { return _imageText; }
            set
            {
                this._imageText = value;
                OnPropertyChanged(nameof(ImageText));
            }
        }

        public ICommand UploadCommand { get; protected set; }

        public PictureFinalizingViewModel(INavigation navigation, Stream source)
        {
            this.IsLoading = true;
            this._navigation = navigation;
            
            // Handle this different - stream gets closed after this
            this.SelectedImageSource = ConvertStreamToSource(source);

            this.UploadCommand = new Command(async () => await UploadPicture(source), () => true);
            this.IsLoading = false;
        }

        public ImageSource ConvertStreamToSource(Stream stream)
        {
            var source = ImageSource.FromStream(() => stream);
            return source;
        }

        private async Task<string> UploadPicture(Stream source)
        {
            var text = _imageText ?? string.Empty;
            var pictureEncoded = ConvertService.Base64FromStream(source).Result;
            var image = new SavedImage() {ImageConverted = pictureEncoded, Text = text};

           // var response = await ApiService.PostImagesConverted(image);
            return "";
            //return response;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
