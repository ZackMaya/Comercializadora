using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCom
{
    public partial class MainPage : ContentPage
    {
         public MainPage()
        {
            InitializeComponent();
         
        }
        private void BtnVerChocolates_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new CompraChocolates());
        }
        private void BtnVerGomitas_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new ComprarGomitass());
        }
        private void BtnVerPaletas_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new ComprarPaletas());
        }
        private void BtnVerSemillas_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new ComprarSemillas());
        }

    }
}

