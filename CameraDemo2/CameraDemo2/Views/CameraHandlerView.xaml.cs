using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CameraDemo2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraHandlerView : ContentPage
    {
        public CameraHandlerView(INavigation navigation)
        {
            InitializeComponent();

            this.BindingContext = new CameraHandlerViewModel(navigation);
        }
    }
}
