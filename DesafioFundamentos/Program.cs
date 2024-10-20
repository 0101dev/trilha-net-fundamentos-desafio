using DesafioFundamentos.Models;
 
Console.OutputEncoding = System.Text.Encoding.UTF8;

decimal precoInicial = 0;
decimal precoPorHora = 0;

Console.WriteLine("Seja bem vindo ao sistema de estacionamento!\n" +
                  "Digite o preço inicial formato(1.00):");
string input = Console.ReadLine();
if(!decimal.TryParse(input, out precoInicial))
{
    Console.WriteLine("O valor informado para o preço inicial está fora dos padões especificados.");
    return;
}

Console.WriteLine("Agora digite o preço por hora formato(1.00):");
input = Console.ReadLine();
if (!decimal.TryParse(input, out precoPorHora))
{
    Console.WriteLine("O valor informado para o preço por hora está fora dos padões especificados.");
    return;
}
Estacionamento es = new Estacionamento(precoInicial, precoPorHora);

 

string opcao = string.Empty;
bool exibirMenu = true;

// Realiza o loop do menu
while (exibirMenu)
{
    Console.Clear();
    Console.WriteLine("Digite a sua opção:");
    Console.WriteLine("1 - Cadastrar veículo");
    Console.WriteLine("2 - Remover veículo");
    Console.WriteLine("3 - Listar veículos");
    Console.WriteLine("4 - Encerrar");

    switch (Console.ReadLine())
    {
        case "1":
            es.AdicionarVeiculo();
            break;

        case "2":
            es.RemoverVeiculo();
            break;

        case "3":
            es.ListarVeiculos();
            break;

        case "4":
            exibirMenu = false;
            break;

        default:
            Console.WriteLine("Opção inválida");
            break;
    }

    Console.WriteLine("Pressione uma tecla para continuar");
    Console.ReadLine();
}

Console.WriteLine("O programa se encerrou");
