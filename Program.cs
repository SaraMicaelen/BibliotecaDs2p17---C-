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
                        await Utils.MensagemTemporaria("Saindo... Até mais!", 1000); // Await aguarda o método assíncrono ser executado. Apenas métodos assíncronos usam await.
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
                Console.WriteLine(" Digite seus dados ou '0' para voltar.");

                // Solicitar Nome
                Console.WriteLine();
                Console.Write(" Nome de Admin: ");
                string entradaNome = Console.ReadLine();
                if (entradaNome == "0")
                {
                    Console.WriteLine();
                    await Utils.MensagemTemporaria(" Voltando...", 500); 
                    return;
                }
                // Solicitar Senha
                Console.WriteLine();
                Console.Write(" Senha: ");
                string entradaSenha = LerSenha();
                if (entradaSenha == "0")
                {
                    Console.WriteLine(); Console.WriteLine();
                    await Utils.MensagemTemporaria(" Voltando...", 500);
                    return;
                }
                // Verificar credenciais
                if (entradaNome == adm.NomeAdm1 && entradaSenha == adm.SenhaAdm1)
                {
                    Console.WriteLine(); Console.WriteLine();
                    await Utils.MensagemTemporaria(" Login bem-sucedido! Entrando... ", 3000);
                    await MenuAdministrador(); // Segue para o Menu de Administrador
                    break;
                }
                else
                {
                    Console.WriteLine(); Console.WriteLine();
                    Console.Write(" Usuário ou senha inválidos. Tente novamente. ");
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
                        await Utils.MensagemTemporaria(" Saindo... ", 1500);
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
                        await Utils.MensagemTemporaria(" Voltando...", 500);
                        return;
                    default:
                        Console.WriteLine();
                        Console.WriteLine(" Opção inválida! Pressione qualquer tecla para tentar novamente.");
                        Console.ReadKey();
                        break;
                }
            } while (opcao != 0);
        }
        public static async Task CadastrarBibliotecario()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===========================================");
                Console.WriteLine("|                                         |");
                Console.WriteLine("|    *** Cadastro de bibliotecário ***    |");
                Console.WriteLine("|                                         |");
                Console.WriteLine("===========================================");

                if (Bibliotecario.MaxAtingido())
                {
                    Console.WriteLine();
                    Console.Write(" Limite máximo de bibliotecários atingido. ");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    Console.WriteLine(" Digite os dados ou '0' para voltar.");

                    // Solicitar Nome
                    Console.WriteLine();
                    Console.Write("   Nome de login: ");
                    string nome = Console.ReadLine();
                    if (nome == "0") // if (nome == "0") return;
                    {
                        Console.WriteLine();
                        await Utils.MensagemTemporaria(" Voltando... ", 500);
                        return;
                    }
                    if (ValidarNomes(nome, 30) == false)
                    {
                        Console.WriteLine();
                        Console.Write(" Nome inválido! Digite no máximo 30 caracteres. ");
                        Console.ReadKey();
                        return;
                    }
                    // Solicitar Senha
                    Console.WriteLine();
                    Console.Write("   Crie uma senha: ");
                    string senha = LerSenha();
                    if (senha == "0")
                    {
                        Console.WriteLine();
                        Console.WriteLine();
                        await Utils.MensagemTemporaria(" Voltando... ", 500);
                        return;
                    }
                    // Solicitar Código
                    Console.WriteLine(); Console.WriteLine();
                    Console.Write("   Código de registro do bibliotecário: ");
                    string cod = Console.ReadLine();
                    if (cod == "0")
                    {
                        Console.WriteLine();
                        await Utils.MensagemTemporaria(" Voltando... ", 500);
                        return;
                    }
                    // Cadastro autenticado
                    Bibliotecario bib = new Bibliotecario(nome, senha, cod);

                    Console.WriteLine();
                    Console.WriteLine(" Bibliotecário(a) '" + bib.Nome + "' cadastrado(a) com sucesso!");
                    
                    // Novo cadastro
                    Console.WriteLine();
                    Console.Write(" Criar novo cadastro? (s/n): ");
                    string resp = Console.ReadLine();
                    if (resp == "S" || resp == "s")
                    {
                        await CadastrarBibliotecario();
                    }
                    return;
                }
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
                        // ListaUsuarios(); 
                        break;
                    case 2:
                        // ListaLivros(); 
                        break;
                    case 3:
                        // ListaEmprestimos(); 
                        break;
                    case 4:
                        // ListaBibliotecarios();
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
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=========================================");
                Console.WriteLine("|                                       |");
                Console.WriteLine("|     *** Login : Bibliotecário ***     |");
                Console.WriteLine("|                                       |");
                Console.WriteLine("=========================================");
                if (Bibliotecario.numBibliotecarios == 0)
                {

                    Console.WriteLine(); 
                    Console.WriteLine(" Nenhum bibliotecário cadastrado no sistema! ");
                    Console.WriteLine();
                    Console.Write(" Para obter acesso, por favor, solicite a um administrador realizar seu cadastro e volte novamente. ");
                    Console.ReadKey();
                    return;
                }
                Console.WriteLine(" Digite seus dados ou '0' para voltar.");

                // Solicitar Nome
                Console.WriteLine();
                Console.Write(" Nome: ");
                string nome = Console.ReadLine();
                if (nome == "0")
                {
                    Console.WriteLine();
                    await Utils.MensagemTemporaria(" Voltando... ", 1000);
                    return;
                }
                // Solicitar Senha
                Console.WriteLine();
                Console.Write(" Senha: ");
                string senha = LerSenha();
                if (senha == "0")
                {
                    Console.WriteLine(); Console.WriteLine();
                    await Utils.MensagemTemporaria(" Voltando... ", 1000);
                    return;
                }
                // Verificar credenciais
                /*if (nome == bib.Nome && senha == bib.Senha) // *** Testar se não há nenhum bibliotecário cadastrado + mensagem ***
                {
                    Console.WriteLine();
                    await Utils.MensagemTemporaria(" Login bem-sucedido! Entrando... ", 3000);
                    await MenuBibliotecario(); // Segue para o Menu de Bibliotecário
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine(" Usuário ou senha inválidos. Tente novamente.");
                    Console.ReadKey();
                }*/
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
                Console.WriteLine(" [4] Devolver livro");
                Console.WriteLine(" [5] Relatórios");
                Console.WriteLine(" [6] Manual de navegação (leitura)");
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
                        ManualMenuBibliotecario();
                        break;
                    case 0:
                        Console.WriteLine();
                        await Utils.MensagemTemporaria(" Saindo... ", 2000);
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
        static void ManualMenuBibliotecario()
        {
            Console.WriteLine("=======================================================");
            Console.WriteLine("|                                                     |");
            Console.WriteLine("| *** Manual de navegação : Menu do bibliotecário *** |");
            Console.WriteLine("|                                                     |");
            Console.WriteLine("=======================================================");
            Console.WriteLine(" Este menu ofere ao bibliotecário as seguintes opções: ");
            Console.WriteLine();
            Console.WriteLine(" 1. (Usuários): Permite o cadastro de usuários assim como atualizações e exclusão.");
            Console.WriteLine(" 2. (Livros): Permite o cadastro de livros assim como atualizações e exclusão.");
            Console.WriteLine(" 3. (Emprestar livro): Permite o empréstimo de livros, utilizando o código do livro junto ao RA do usuário que deseja emprestar, marcando o livro como (reservado).");
            Console.WriteLine(" 4. (Devolver livro): Permite a devolução de livros utilizando o código do livro junto ao RA do usuário, marcando o livro como (disponível).");
            Console.WriteLine(" 5. (Relatórios): Permite escolher e visualizar listas de todos os usuários, livros, empréstimos e bibliotecários cadastrados no sistema.");
            Console.WriteLine(" 6. (Manual de navegação): Orienta e trás uma breve descrição sobre todas as funções do menu de bibliotecário.");
            Console.WriteLine(" 0. (Voltar): Permite voltar ao menu anterior.");
            Console.WriteLine();
            Console.Write(" Aperte qualquer tecla para voltar. ");
            Console.ReadKey();
            return;
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
        public static bool ValidarNomes(string entrada, int maxLength)
        {
            return !string.IsNullOrWhiteSpace(entrada) && entrada.Length <= maxLength;
        }
        public static class Utils // Função para exibir mensagens temporárias.
        {
            public static async Task MensagemTemporaria(string mensagem, int tempoMilissegundos)
            {
                Console.WriteLine(mensagem);
                await Task.Delay(tempoMilissegundos);
            }
        }
    }
}
