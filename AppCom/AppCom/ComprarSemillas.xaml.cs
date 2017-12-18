using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCom
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ComprarSemillas : ContentPage
    {
        JArray arregloDatos = new JArray();

        public ComprarSemillas()
        {
            InitializeComponent();
            //BindingContext = new ContentPageViewModel();
            cargarJSONasincrono();
        }


        private async void cargarJSONasincrono()
        {
            try
            {
                var cliente = new HttpClient();
                //await this.DisplayAlert("ClienteHTTP", "Si llega a la peticion", "Acepar");
                //cliente.DefaultRequestHeaders.Add("id","descripcion");
                cliente.BaseAddress = new Uri("https://semillas-luis.herokuapp.com/api/");
                // await this.DisplayAlert("URL", "Si llega a la URI", "Aceptar");
                String url = string.Format("getproductos");
                var resp = await cliente.GetAsync(url);

                if (resp.IsSuccessStatusCode)
                {
                    var respStr = await resp.Content.ReadAsStringAsync();
                    //await this.DisplayAlert("URL", respStr, "Aceptar");
                    //hasta aqui la peticion es xida
                    var l = JsonConvert.DeserializeObject<Producto>(respStr);
                    // await DisplayAlert("lista", "deserializo l", "aceptar");
                    /* JObject valores = JObject.Parse(respStr);
                     arregloDatos = (JArray)valores["productos"];
                     await this.DisplayAlert("Arreglo", "despues de arreglo", "Acepar");
                     //lista.ItemsSource = arregloDatos;*/


                    JObject valores = JObject.Parse(respStr);
                    arregloDatos = (JArray)valores["productos"];

                    var arregloProductos = new List<Producto>();

                    for (var i = 0; i < arregloDatos.Count; i++)
                    {
                        Producto tmp = new Producto()
                        {
                            nombre = arregloDatos[i]["nombre"].ToString(),
                            descripcion = arregloDatos[i]["descripcion"].ToString(),
                            url = arregloDatos[i]["imagen"].ToString()
                          //  piezas = Convert.ToInt32(arregloDatos[i]["piezas"].ToString()),


                        };
                        arregloProductos.Add(tmp);
                    }
                    loadingItems2.IsVisible = false;
                    ListView2.ItemsSource = arregloProductos;
                    ListView2.IsPullToRefreshEnabled = true;



                }
            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Tienes una excepción en: ", ex.Message, "Aceptar");
            }

        }
    }

    class ComprarSemillasViewModel : INotifyPropertyChanged
    {

        public ComprarSemillasViewModel()
        {
            IncreaseCountCommand = new Command(IncreaseCount);
        }

        int count;

        string countDisplay = "You clicked 0 times.";
        public string CountDisplay
        {
            get { return countDisplay; }
            set { countDisplay = value; OnPropertyChanged(); }
        }

        public ICommand IncreaseCountCommand { get; }

        void IncreaseCount() =>
            CountDisplay = $"You clicked {++count} times";


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
