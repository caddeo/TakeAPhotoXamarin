﻿using System;
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

            // Local images instead of web api
            _entries = new ObservableCollection<ImageEntry>()
            {
                new ImageEntry()
                {
                    Text = "This is a test",
                    // Et billede af en banan i base64
                    SelectedSource = ConvertService.ImageFromBase64(@"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")
                },

                new ImageEntry()
                {
                    Text = "This is a test also",
                    SelectedSource = ConvertService.ImageFromBase64(@"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")
                }
            };
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
