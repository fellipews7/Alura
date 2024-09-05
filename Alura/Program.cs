using Alura.Application.Interface;
using ExtracaoDeInformacoesDoIPTU.Infra.Crosscutting;

Console.WriteLine("Digite a palavra pesquisada");

var termo = Console.ReadLine();

if (string.IsNullOrWhiteSpace(termo))
{
    Console.WriteLine("Favor digite uma palavra.");
    return;
}


Bootstrap.Start();

await Bootstrap.container.GetInstance<IBuscarInformacoesService>().Executar("python");

Console.ReadLine();