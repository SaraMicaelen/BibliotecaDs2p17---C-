using System.Globalization;
using System.Threading.Tasks; // Permite usar Task.Delay (função para exibir mensagens temporárias).

namespace BibliotecaDs2p17
{
    

    internal class Program
    {
        static void Main(string[] args)
        {
            MenuInicial().GetAwaiter().GetResult(); // Executa o método assíncrono de forma síncrona.
        }

        static async Task MenuInicial()
        {
            int opcao = 3;
            do
            {
                Console.Clear();
                Console.WriteLine("============================================");
                Console.WriteLine("|                                          |");
                Console.WriteLine("| *** Bem-vindo a Biblioteca 'Ds2p17'! *** |");
                Console.WriteLine("|                                          |");
                Console.WriteLine("============================================");
                Console.WriteLine();
                Console.WriteLine(" [1] Entrar como Administrador");
                Console.WriteLine(" [2] Entrar como Bibliotecário");
                Console.WriteLine(" [0] Sair");
                Console.WriteLine();
                Console.Write(" Escolha uma opção: "); // VERIFICAR ERRO: Caso o usuário não digite nada (""), o programa fecha. ***PODE OCORRER EM QUALQUER ENTRADA DE DADOS?***
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        await LoginAdministrador();
                        break;
                    case 2:
                        await LoginBibliotecario();
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine();
                        await Utils.MensagemTemporaria("Saindo... Até mais!", 3000); // Await aguarda o método assíncrono ser executado.
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine(" Opção inválida! Pressione qualquer tecla para tentar novamente.");
                        Console.ReadKey();
                        break;
                }
            } while (opcao != 0);
        }

        static async Task LoginAdministrador()
        {
            Admin adm = new Admin();
            adm.NomeAdm1 = "admin";
            adm.SenhaAdm1 = "senhaAdm";

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=========================================");
                Console.WriteLine("|                                       |");
                Console.WriteLine("|     *** Login : Administrador ***     |");
                Console.WriteLine("|                                       |");
                Console.WriteLine("=========================================");
                Console.WriteLine(" Digite seus dados ou 0 para voltar.");

                // Solicitar Nome
                Console.WriteLine();
                Console.Write(" Nome de Admin: ");
                string entradaNome = Console.ReadLine();
                if (entradaNome == "0")
                {
                    Console.WriteLine();
                    await Utils.MensagemTemporaria(" Voltando...", 1000); // Mensagem por 1 segundo
                    return;
                }
                // Solicitar Senha
                Console.WriteLine();
                Console.Write(" Senha: ");
                string entradaSenha = Console.ReadLine();
                if (entradaSenha == "0")
                {
                    Console.WriteLine();
                    await Utils.MensagemTemporaria(" Voltando...", 1000);
                    return;
                }
                // Verificar credenciais
                if (entradaNome == adm.NomeAdm1 && entradaSenha == adm.SenhaAdm1)
                {
                    Console.WriteLine();
                    await Utils.MensagemTemporaria(" Login bem-sucedido!", 3000);
                    await MenuAdministrador(); // Segue para o Menu de Administrador
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine(" Usuário ou senha inválidos. Tente novamente.");
                    Console.ReadKey();
                }
            }
        }

        static async Task MenuAdministrador()
        {
            int opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("=========================================");
                Console.WriteLine("|                                       |");
                Console.WriteLine("| *** Administrador seja bem-vindo! *** |");
                Console.WriteLine("|                                       |");
                Console.WriteLine("=========================================");
                Console.WriteLine();
                Console.WriteLine(" [1] Bibliotecários");
                Console.WriteLine(" [2] Relatórios");
                Console.WriteLine(" [0] Voltar ao início");
                Console.WriteLine();
                Console.Write(" Qual menu deseja acessar? ");
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        await AdmBibliotecarios();
                        break;
                    case 2:
                        await MenuRelatorios();
                        break;
                    case 0:
                        Console.WriteLine();
                        await Utils.MensagemTemporaria(" Saindo... ", 3000);
                        await MenuInicial();
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine(" Opção inválida! Pressione qualquer tecla para tentar novamente.");
                        Console.ReadKey();
                        break;
                }

            } while (opcao != 0);
        }

        static async Task AdmBibliotecarios()
        {
            int opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("===========================================");
                Console.WriteLine("|                                         |");
                Console.WriteLine("| *** Administração de Bibliotecários *** |");
                Console.WriteLine("|                                         |");
                Console.WriteLine("===========================================");
                Console.WriteLine();
                Console.WriteLine(" [1] Cadastrar bibliotecário");
                Console.WriteLine(" [2] Atualizar cadastros");
                Console.WriteLine(" [3] Excluir bibliotecário"); // Excluir ou desativar?
                Console.WriteLine(" [0] Voltar");
                Console.WriteLine();
                Console.Write(" Escolha uma opção: ");
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        await CadastrarBibliotecario();
                        break;
                    case 2:
                        // await AtualizarCadastroBibliotecario();
                        break;
                    case 3:
                        // await ExcluirBibliotecario();
                        break;
                    case 0:
                        Console.WriteLine();
                        await Utils.MensagemTemporaria(" Voltando...", 1000);
                        return;
                    default:
                        Console.WriteLine();
                        Console.WriteLine(" Opção inválida! Pressione qualquer tecla para tentar novamente.");
                        Console.ReadKey();
                        break;
                }
            } while (opcao != 0);
        }
        static async Task CadastrarBibliotecario()
        {
            while (true)
            {
                Bibliotecário b = new Bibliotecário();

                Console.Clear();
                Console.WriteLine("===========================================");
                Console.WriteLine("|                                         |");
                Console.WriteLine("|    *** Cadastro de bibliotecário ***    |");
                Console.WriteLine("|                                         |");
                Console.WriteLine("===========================================");
                Console.WriteLine(" Insira os dados ou digite 0 para voltar.");

                // Solicitar Nome
                Console.WriteLine();
                Console.Write("   Nome de login: ");
                string entradaNome = Console.ReadLine();
                if (entradaNome == "0")
                {
                    Console.WriteLine();
                    await Utils.MensagemTemporaria(" Voltando... ", 1000);
                    return;
                }
                // Solicitar Código
                Console.WriteLine();
                Console.Write("   Código de registro do bibliotecário: ");
                string entradaCod = Console.ReadLine();
                if (entradaCod == "0")
                {
                    Console.WriteLine();
                    await Utils.MensagemTemporaria(" Voltando... ", 1000);
                    return;
                }
                // Solicitar Senha
                Console.WriteLine();
                Console.Write("   Crie uma senha: ");
                string entradaSenha = LerSenha();
                if (entradaSenha == "0")
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    await Utils.MensagemTemporaria(" Voltando... ", 1000);
                    return;
                }
                // Cadastro autenticado
                b.Nome = entradaNome;
                b.Cod_b = entradaCod;
                b.Senha = entradaSenha;

                Console.WriteLine();
                Console.WriteLine();
                await Utils.MensagemTemporaria(" Bibliotecário(a) " + b.Nome + " cadastrado(a) com sucesso!", 1000);
                Console.WriteLine();
                Console.Write(" Realizar outro cadastro? (s/n): ");
                string resp = Console.ReadLine();
                if (resp == "S" || resp == "s")
                {
                    await CadastrarBibliotecario();
                }
                return;
            }
        }

        static async Task MenuRelatorios() // Menu acessado por Administradores e Bibliotecários.
        {
            int opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("==========================================");
                Console.WriteLine("|                                        |");
                Console.WriteLine("| *** Relatórios de dados do sistema *** |");
                Console.WriteLine("|                                        |");
                Console.WriteLine("==========================================");
                Console.WriteLine();
                Console.WriteLine(" [1] Lista de usuários");
                Console.WriteLine(" [2] Lista de livros");
                Console.WriteLine(" [3] Lista de empréstimos");
                Console.WriteLine(" [4] Lista de bibliotecários");
                Console.WriteLine(" [0] Voltar");
                Console.WriteLine();
                Console.Write(" Escolha uma opção: ");
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        // await ListaUsuarios(); 
                        break;
                    case 2:
                        // await ListaLivros(); 
                        break;
                    case 3:
                        // await ListaEmprestimos(); 
                        break;
                    case 4:
                        // await ListaBibliotecarios(); 
                        break;
                    case 0:
                        Console.WriteLine();
                        await Utils.MensagemTemporaria(" Voltando...", 1000);
                        return;
                    default:
                        Console.WriteLine();
                        Console.WriteLine(" Opção inválida! Pressione qualquer tecla para tentar novamente.");
                        Console.ReadKey();
                        break;
                }
            } while (opcao != 0);
        }

        static async Task LoginBibliotecario()
        {
            Bibliotecário b = new Bibliotecário();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=========================================");
                Console.WriteLine("|                                       |");
                Console.WriteLine("|     *** Login : Bibliotecário ***     |");
                Console.WriteLine("|                                       |");
                Console.WriteLine("=========================================");
                Console.WriteLine(" Digite seus dados ou 0 para voltar.");

                // Solicitar Nome
                Console.WriteLine();
                Console.Write(" Nome: ");
                string entradaNome = Console.ReadLine();
                if (entradaNome == "0")
                {
                    Console.WriteLine();
                    await Utils.MensagemTemporaria(" Voltando... ", 1000);
                    return;
                }
                // Solicitar Senha
                Console.WriteLine();
                Console.Write(" Senha: ");
                string entradaSenha = Console.ReadLine();
                if (entradaSenha == "0")
                {
                    Console.WriteLine();
                    await Utils.MensagemTemporaria(" Voltando... ", 1000);
                    return;
                }
                // Verificar credenciais
                if (entradaNome == b.Nome && entradaSenha == b.Senha) // *** Testar se não há nenhum bibliotecário cadastrado + mensagem ***
                {
                    Console.WriteLine();
                    await Utils.MensagemTemporaria(" Login bem-sucedido!", 3000);
                    await MenuBibliotecario(); // Segue para o Menu de Bibliotecário
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine(" Usuário ou senha inválidos. Tente novamente.");
                    Console.ReadKey();
                }
            }
        }

        static async Task MenuBibliotecario()
        {
            int opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("=========================================");
                Console.WriteLine("|                                       |");
                Console.WriteLine("| *** Bibliotecário seja bem-vindo! *** |");
                Console.WriteLine("|                                       |");
                Console.WriteLine("=========================================");
                Console.WriteLine();
                Console.WriteLine(" [1] Usuários");
                Console.WriteLine(" [2] Livros");
                Console.WriteLine(" [3] Emprestar livro");
                Console.WriteLine(" [4] Devolver Livro");
                Console.WriteLine(" [5] Relatórios");
                Console.WriteLine(" [6] Manual de Navegação (leitura)");
                Console.WriteLine(" [0] Voltar ao início");
                Console.WriteLine();
                Console.Write(" Qual menu deseja acessar? ");
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        //await MenuUsarios();
                        break;
                    case 2:
                        //await MenuLivros();
                        break;
                    case 3:
                        //await EmprestarLivro();
                        break;
                    case 4:
                        //await DevolverLivro();
                        break;
                    case 5:
                        await MenuRelatorios();
                        break;
                    case 6:
                        //await ManualDeNavegacao();
                        break;
                    case 0:
                        Console.WriteLine();
                        await Utils.MensagemTemporaria(" Saindo... ", 3000);
                        await MenuInicial();
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine(" Opção inválida! Pressione qualquer tecla para tentar novamente.");
                        Console.ReadKey();
                        break;
                }

            } while (opcao != 0);
        }
        
        public static string LerSenha() // Função para ler senhas com "*".
        {
            string senha = string.Empty;
            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(intercept: true);
                if (keyInfo.Key != ConsoleKey.Backspace && keyInfo.Key != ConsoleKey.Enter)
                {
                    senha += keyInfo.KeyChar;
                    Console.Write("*");
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && senha.Length > 0)
                {
                    senha = senha.Substring(0, senha.Length - 1);
                    Console.Write("\b \b");
                }
            } while (keyInfo.Key != ConsoleKey.Enter);

            return senha;
        }

        public static class Utils
        {
            public static async Task MensagemTemporaria(string mensagem, int tempoMilissegundos)
            {
                Console.WriteLine(mensagem);
                await Task.Delay(tempoMilissegundos);
            }
        }
    }
}
