using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace AppCom
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


           //  MainPage = new NavigationPage(new AppCom.ComprarSemillas());
             MainPage = new NavigationPage(new AppCom.ComprarGomitass());
            // MainPage = new NavigationPage(new AppCom.ComprarPaletas());
            // MainPage = new NavigationPage(new AppCom.CompraChocolates());


            /* MainPage = new AppCom.MainPage()
            {
                Title = "Hola mundo en xamarin Forms"
            };*/
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
