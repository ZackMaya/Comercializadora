using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCom
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        //public UserModel _user { get; set; }


        public UserPage(UserModel user)
        {
            InitializeComponent();
            //_user = user;
            BindingContext = user;
        }
    }
}
