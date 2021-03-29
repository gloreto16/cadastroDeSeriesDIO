using System;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarSerie();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                opcaoUsuario = ObterOpcaoUsuario();
            }
            Console.WriteLine("Obrigado por utilizar nossos serviços.");
            Console.ReadLine();
        }
        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine("==============================");
            Console.WriteLine("Toreto´s Séries iniciado!");
            Console.WriteLine("Escolha a opção desejada: ");

            Console.WriteLine("1 - Listar Séries");
            Console.WriteLine("2 - Inserir nova série");
            Console.WriteLine("3 - Atualizar série");
            Console.WriteLine("4 - Excluir série");
            Console.WriteLine("5 - Visualizar série");
            Console.WriteLine("C - Limpar tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            return opcaoUsuario;
        }
        private static void ListarSerie()
        {
            Console.WriteLine("Você escolheu listar as séries.");

            var lista = repositorio.Lista();
            if(lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada.");
                return;
            }

            foreach(var serie in lista)
            {
                var excluido = serie.Exclusao();
                Console.WriteLine("#ID {0} - {1} - {2} - {3}", serie.retornaID(), serie.retornaTitulo(), serie.retornaAno(), (excluido ? "Excluido" : "Disponível"));
            }
        }
        private static void InserirSerie()
        {
            Console.WriteLine("Você escolheu inserir uma série.");

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }
            Console.Write("Digite o gênero da série: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            while(entradaGenero > Enum.GetNames(typeof(Genero)).Length || entradaGenero < 1)
            {
                Console.WriteLine("Por favor, escolha uma opção válida.");
                entradaGenero = int.Parse(Console.ReadLine());
            }

            Console.Write("Digite o título da série: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("Digite o ano em que a série começou: ");
            int entradaAno = int.Parse(Console.ReadLine());
            
            Console.Write("Digite a descrição da série: ");
            string entradaDescricao = Console.ReadLine();

            Series novaSerie = new Series(id: repositorio.ProximoId(), genero: (Genero)entradaGenero, titulo: entradaTitulo, ano: entradaAno, descricao: entradaDescricao);
            repositorio.Insere(novaSerie);
            Console.WriteLine("Série cadastrada com sucesso.");
        }
        private static void AtualizarSerie()
        {
            Console.WriteLine("Você escolheu atualizar uma série.");

            var lista = repositorio.Lista();
            if(lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada, portanto impossível atualizar.");
                return;
            }

            Console.Write("Digite o ID da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            while(indiceSerie > repositorio.listaSerie.Count - 1 || indiceSerie < 0)
            {
                Console.WriteLine("ID inválida.");
                indiceSerie = int.Parse(Console.ReadLine());
            }

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }
            Console.Write("Digite o gênero da série: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            while(entradaGenero > Enum.GetNames(typeof(Genero)).Length || entradaGenero < 1)
            {
                Console.WriteLine("Por favor, escolha uma opção válida.");
                entradaGenero = int.Parse(Console.ReadLine());
            }

            Console.Write("Digite o título da série: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("Digite o ano em que a série começou: ");
            int entradaAno = int.Parse(Console.ReadLine());
            
            Console.Write("Digite a descrição da série: ");
            string entradaDescricao = Console.ReadLine();

            Series atualizaSerie = new Series(id: indiceSerie, genero: (Genero)entradaGenero, titulo: entradaTitulo, ano: entradaAno, descricao: entradaDescricao);
            repositorio.Atualizar(indiceSerie, atualizaSerie);
            Console.WriteLine("Série atualizada com sucesso.");
        }
        private static void ExcluirSerie()
        {
            Console.WriteLine("Você escolheu excluir uma série.");

            Console.Write("Digite o ID da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            while(indiceSerie > repositorio.listaSerie.Count - 1 || indiceSerie < 0)
            {
                Console.WriteLine("ID inválida.");
                indiceSerie = int.Parse(Console.ReadLine());
            }

            Console.Write("Você tem certeza que deseja excluir esta série? S/N");
            string res = Console.ReadLine().ToUpper();

            while(res != "S" && res != "N")
            {
                Console.WriteLine("Escolha S-Sim ou N-Não: ");
                res = Console.ReadLine().ToUpper();
            }
            if(res == "S")
            {
                repositorio.Exclui(indiceSerie);
                Console.WriteLine("Série excluída com sucesso.");
            }
        }
        private static void VisualizarSerie()
        {
            Console.WriteLine("Você escolheu visualizar uma série.");
            
            Console.Write("Digite o ID da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            while(indiceSerie > repositorio.listaSerie.Count - 1 || indiceSerie < 0)
            {
                Console.WriteLine("ID inválida.");
                indiceSerie = int.Parse(Console.ReadLine());
            }

            var serie = repositorio.RetornaPorId(indiceSerie);

            Console.WriteLine(serie);    
        }
    }
}
