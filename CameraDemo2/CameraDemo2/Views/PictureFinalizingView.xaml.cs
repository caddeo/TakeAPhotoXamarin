using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CameraDemo2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CameraDemo2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PictureFinalizingView : ContentPage
    {
        public PictureFinalizingView(PictureFinalizingViewModel vm)
        {
            this.BindingContext = vm;
            InitializeComponent();
        }
    }
}
