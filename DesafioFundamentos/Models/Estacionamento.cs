using System.Numerics;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal _precoInicial = 0;
        private decimal _precoPorHora = 0;
        private const decimal _periodoInicial = 1;
        private List<Veiculo> veiculos = new List<Veiculo>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this._precoInicial = precoInicial;
            this._precoPorHora = precoPorHora;

            if (veiculos.Count.Equals(0))
            {
                veiculos.Add(new Veiculo("Fusca","ppp1231")); 
                veiculos.Add(new Veiculo("Gol","ppp1232")); 
                veiculos.Add(new Veiculo("Santana","ppp1233")); 
                veiculos.Add(new Veiculo("Fusca","ppp1234"));
            }
        }

        public void AdicionarVeiculo()
        {
            Console.WriteLine("Digite o modelo do veículo:");
             
            var modelo = Console.ReadLine();

            Console.WriteLine("Digite a placa do veículo:");

            var placa = Console.ReadLine();

            veiculos.Add(new Veiculo(modelo.ToString(), placa.ToString()));


        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");

            var placa = Console.ReadLine();

            Veiculo veiculo = veiculos.Find(v => v.Placa.Equals(placa, StringComparison.OrdinalIgnoreCase));

            if (veiculo != null)
            { 
                Console.WriteLine("Informe a hora de saída (formato HH:mm):");
                string horaSaidaInput = Console.ReadLine();
                DateTime? horaSaida = Util.Util.ConverterStringToDatetime(horaSaidaInput);

                if (horaSaida == null)
                {
                    return;
                }

                RemovendoVeiculoValido(placa, horaSaida, veiculo);
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        public void ListarVeiculos()
        {
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                Console.WriteLine("{0,-15} {1,-30} {2,-30}", $"Placa", $"Modelo", $"Hora Entrada");

                foreach (var item in veiculos)
                {
                    Console.WriteLine("{0,-15} {1,-30} {2,-30}",item.Placa, item.Modelo,item.HoraEntrada);
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }

        private void RemovendoVeiculoValido(string placa, DateTime? horaSaida, Veiculo veiculo)
        {

            try
            {
                Console.WriteLine($"calculando...");

                decimal horasEstacionada = CalcularHorasEstacionadas(placa, horaSaida);

                int horasAdicionais = (int)Math.Ceiling(horasEstacionada - _periodoInicial);

                int horasAdicionaisCobradas = CalcularTotalHoraAdicionaisaSerCobrada(horasEstacionada);

                decimal valorTotal = CalcularValor(horasAdicionaisCobradas);

                Util.Util.EscrevendoComPontos("Placa", placa);
                Util.Util.EscrevendoComPontos("Hora Entrada", veiculo.HoraEntrada.ToString("dd/MM/yyyy HH:mm"));
                Util.Util.EscrevendoComPontos("Hora Saída", horaSaida.ToString());
                Util.Util.EscrevendoComPontos("Valor da Hora Inicial", Util.Util.DecimalToMoedaReal(_precoInicial));
                Util.Util.EscrevendoComPontos("Valor da Hora Adicional", Util.Util.DecimalToMoedaReal(_precoPorHora));
                Util.Util.EscrevendoComPontos("Horas Adicionais Cobradas", Util.Util.DecimalToMoedaReal(horasAdicionaisCobradas));
                Util.Util.EscrevendoComPontos("TOTAL", Util.Util.DecimalToMoedaReal(valorTotal));

                RemoverVeiculoDaListaPorPlaca(placa);

                Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: R$ {Util.Util.DecimalToMoedaReal(valorTotal)}");

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Erro na rotina de retirada de veiculo. Erro:{ex.Message}");

            }
        }

        private decimal CalcularHorasEstacionadas(string placa, DateTime? horaSaida = null)
        {

            var veiculo = veiculos.Find(v => v.Placa.Equals(placa, StringComparison.OrdinalIgnoreCase));
            if (veiculo != null)
            {
                var horaSaidaReal = horaSaida ?? DateTime.Now;

                return (decimal)(horaSaidaReal - veiculo.HoraEntrada).TotalHours; 
            }
            else
            {
                throw new Exception($"Erro ao tentar calcular o valor a ser pago no momento da saida da placa {placa}.");
            }
        }

        private int CalcularTotalHoraAdicionaisaSerCobrada(decimal horasEstacionada)
        {
            if(horasEstacionada < 0 ) { return 0; }

            int horasAdicionais =  (int)Math.Ceiling(horasEstacionada - _periodoInicial);

            return horasAdicionais < 0 ? 0 : horasAdicionais;
        } 

        private decimal CalcularValor(int horasAdicionais, decimal periodoInicial = 1)
        {

            if (horasAdicionais < 1)
                return _precoInicial;
             
            return (decimal)(_precoInicial + (horasAdicionais * _precoPorHora));           
        }

        private bool RemoverVeiculoDaListaPorPlaca(string placa)
        {
            try
            {
                Veiculo veiculo = veiculos.Find(v => v.Placa.Equals(placa, StringComparison.OrdinalIgnoreCase));
                veiculos.Remove(veiculo);
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine($"Erro ao tentar remover o veiculo com placa {placa} da lista de veiculos estacionados.");
                return false;
            }
        }
    }
}
