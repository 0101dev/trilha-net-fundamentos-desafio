using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioFundamentos.Util
{
    public static class Util
    {
        public static DateTime? ConverterStringToDatetime(string strLine) {


            if (string.IsNullOrEmpty(strLine))
            {
                return DateTime.Now;
            }
           
            if (DateTime.TryParseExact(strLine, "HH:mm", null, System.Globalization.DateTimeStyles.None, out var horaSaidaTemp))
            {
                return new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, horaSaidaTemp.Hour, horaSaidaTemp.Minute, 0);
            }
            
            Console.WriteLine("Formato de hora inválido.");
            return null;
           
             

        }
        public static string DecimalToMoedaReal(decimal valor)
        {

            try
            {
                return valor.ToString("C", new System.Globalization.CultureInfo("pt-BR"));
            }
            catch (Exception)
            {

                throw new Exception("Erro ao tentar converter o valor informado para moeda (R$).s");
            }
        }

        public static void EscrevendoComPontos(string label, string valor)
        {
            const int tamanhoLinha = 100;
            string resultado = label + new string('.', tamanhoLinha - label.Length - valor.Length)+valor;

            Console.WriteLine(resultado);
        }
    }
}
