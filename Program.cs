using System.Text;
using DesafioProjetoHospedagem.Models;

Console.OutputEncoding = Encoding.UTF8;

bool reservando = true;
while (reservando)
{
    // Cria os modelos de hóspedes e cadastra na lista de hóspedes
    List<Pessoa> hospedes = new List<Pessoa>();

    Console.Clear();
    bool cadastrandoHospedes = true;
    int contador = 1;
    while (cadastrandoHospedes)
    {
        System.Console.WriteLine($"\n{contador}º Hóspede:\n");
        System.Console.WriteLine("Digite o nome do hóspede:");
        string nome = Console.ReadLine();

        System.Console.WriteLine("Digite o sobrenome do hóspede:");
        string sobrenome = Console.ReadLine();

        hospedes.Add(new Pessoa(nome, sobrenome));

        System.Console.WriteLine("\nHóspede cadastrados com sucesso!\n");
        contador ++;

        System.Console.WriteLine("""
        Deseja cadastrar mais um hóspede?
        [1] Sim         [2] Não

        Resposta:
        """);
        int.TryParse(Console.ReadLine(), out int respostaCadastro);
        if(respostaCadastro != 1)
        {
            cadastrandoHospedes = false;
        }
        Console.Clear();
        System.Console.WriteLine("Hóspedes cadastrados:");
        hospedes.ForEach(h =>{
            System.Console.WriteLine($"Nome Completo: {h.NomeCompleto}");
        });
    }

    // Cria a suíte
    System.Console.WriteLine("\nCriação da suíte:");
    System.Console.WriteLine("\nTipo da suíte:");
    string tipoSuite = Console.ReadLine();
    System.Console.WriteLine("Quantidade de hóspedes suportado:");
    int.TryParse(Console.ReadLine(),out int qtdSuite);
    System.Console.WriteLine("Valor da diária:");
    decimal.TryParse(Console.ReadLine(),out decimal valorDiaria);

    // Cria uma nova reserva, passando a suíte e os hóspedes
    try
    {
        System.Console.WriteLine("Quantos dias deseja reservar?");
        int.TryParse(Console.ReadLine(), out int diasReservados);
        
        Reserva reserva = new Reserva(diasReservados);

        Suite suite = new Suite(tipoSuite, qtdSuite, valorDiaria);
        reserva.CadastrarSuite(suite);
        reserva.CadastrarHospedes(hospedes);

        // Exibe a quantidade de hóspedes e o valor da diária
        //Console.WriteLine($"Hóspedes: {reserva.ObterQuantidadeHospedes()}");
        //Console.WriteLine($"Valor diária: {reserva.CalcularValorDiaria()}");

        var (valor,desconto) = reserva.CalcularValorDiaria();

        System.Console.WriteLine("\nReserva cadastrada com sucesso!");
        System.Console.WriteLine($"""
    Informações da Reserva:

    Suíte:
        Tipo: {reserva.Suite.TipoSuite}
        Capacidade: {reserva.Suite.Capacidade}
        Valor Diária: {reserva.Suite.ValorDiaria:C2}
    
    Reserva:
        Quantidade de Hóspedes: {reserva.ObterQuantidadeHospedes()}
        Dias Reservados: {reserva.DiasReservados}
        Desconto: {desconto:C2}
        Valor Total: {valor:C2}
    """);

        var hospedesText = "\nHóspedes: \n";
        reserva.Hospedes.ForEach(h =>
        {
            hospedesText += $"\t{h.NomeCompleto}\n";
        });

        System.Console.WriteLine(hospedesText);
    }
    catch (Exception ex)
    {
        System.Console.WriteLine("Ocorreu um erro ao cadastrar a reserva: " + ex.Message);
    }

    System.Console.WriteLine("""
    Deseja fazer uma nova reserva?
    [1] Sim         [2] Não 

    Resposta:
    """);
    int.TryParse(Console.ReadLine(), out int respostaReserva);
    
    if (respostaReserva != 1)
    {
        reservando = false;
        System.Console.WriteLine("\nObrigado por utilizar nosso sistema de reservas!\nSaindo da aplicação...");
        
    }

}