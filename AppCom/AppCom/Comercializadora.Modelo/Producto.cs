using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCom
{

    public class Producto
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string imagen { get; set; }
        public int piezas { get; set; }
        public string descripcion { get; set; }
        public string url { get; set; }
        public int cantidad { get; set; }
        public string foto { get; set; }

    }

   public class RootObject
    {
        public List<Producto> productos { get; set; }
    }

}
