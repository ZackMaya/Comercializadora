﻿using Newtonsoft.Json;
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
            getComercializadora();

        }

        private void BtnBuy_Clicked(object sender, EventArgs e)
        {
            var id = boxId.Text;
            var cantidad = boxCantidad.Text;
            //Verificamos que no sean vacios los campos.
            if (!string.IsNullOrEmpty(cantidad) && !string.IsNullOrEmpty(id))
            {
                var pc = Convert.ToInt32(cantidad);
                if (pc > 0)
                {
                    var i = Convert.ToInt32(boxId.Text) - 1;
                    if (pc <= Convert.ToInt32(arregloDatos[i]["piezas"].ToString()))
                    {
                        //realizar el put
                      //  putComercializadora(cantidad);
                    }
                    else
                    {
                        DisplayAlert("Error, solo de dispone de ", arregloDatos[i]["piezas"].ToString(), "Aceptar");
                    }
                }
                else
                {
                    DisplayAlert("Error, se espera un numero mayor a 0.", cantidad, "Aceptar");
                }
            }
            else
            {
                DisplayAlert("Error, campo vacio.", null, "Aceptar");
            }
        }


        private async void getComercializadora()
        {
            try
            {
                var cliente = new HttpClient();
                //await this.DisplayAlert("ClienteHTTP", "Si llega a la peticion", "Acepar");
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



                    JObject valores = JObject.Parse(respStr);
                    arregloDatos = (JArray)valores["productos"];

                    var arregloProductos = new List<Producto>();

                    for (var i = 0; i < arregloDatos.Count; i++)
                    {
                        Producto tmp = new Producto()
                        {
                            nombre = arregloDatos[i]["nombre"].ToString(),
                            descripcion = arregloDatos[i]["descripcion"].ToString(),
                            url = arregloDatos[i]["url"].ToString(),
                            piezas = Convert.ToInt32(arregloDatos[i]["piezas"].ToString())
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

   /* public async Task putComercializadora(int cantidad)
    {
        try
        {
            JArray arregloDatos2 = new JArray();

            var cliente = new HttpClient();
            //await this.DisplayAlert("ClienteHTTP", "Si llega a la peticion", "Acepar");
            cliente.BaseAddress = new Uri("https://semillas-luis.herokuapp.com/api/");
            // await this.DisplayAlert("URL", "Si llega a la URI", "Aceptar");
            String url = string.Format("actualizarProducto");
            var resp = await cliente.PutAsync(url);
            var respStr2 = await resp.Content.ReadAsStringAsync();
            var l = JsonConvert.DeserializeObject<Producto>(respStr2);



            JObject valores = JObject.Parse(respStr2);
            arregloDatos2 = (JArray)valores["productos"];

            var arregloProductos = new List<Producto>();

            if (resp.IsSuccessStatusCode)
            {

                for (var i = 0; i < arregloDatos2.Count; i++)
                {
                    Producto tmp2 = new Producto()
                    {
                        cantidad = Convert.ToInt32(arregloDatos2[i]["cantidad"])
                    };
                    arregloProductos.Add(tmp2);
                    await this.DisplayAlert("Se actualizaron las cantidades: ", "cantidad", "Aceptar");
                }


            }

        }
        catch (Exception ex)
        {
            await this.DisplayAlert("Tienes una excepción en: ", ex.Message, "Aceptar");
        }
    }*/


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
