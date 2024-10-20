using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioFundamentos.Models
{
    public class Veiculo
    { 
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public DateTime HoraEntrada { get; set; }

        public Veiculo(string modelo, string placa)
        { 
            Modelo = modelo;
            Placa = placa;
            HoraEntrada = DateTime.Now;
        }
    }
}
