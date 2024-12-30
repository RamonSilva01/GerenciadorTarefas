using System;
using System.Collections.Generic;
using System.IO;

namespace GerenciadorDeTarefas
{
    class Tarefa
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Concluida { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Descricao} (Concluída: {Concluida})";
        }
    }

    class Program
    {
        static List<Tarefa> tarefas = new List<Tarefa>();
        static string caminhoArquivo = "tarefas.txt";

        static void Main(string[] args)
        {
            CarregarTarefas();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Sistema de Gerenciamento de Tarefas");
                Console.WriteLine("1. Adicionar Tarefa");
                Console.WriteLine("2. Exibir Tarefas");
                Console.WriteLine("3. Concluir Tarefa");
                Console.WriteLine("4. Excluir Tarefa");
                Console.WriteLine("5. Sair");

                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        AdicionarTarefa();
                        break;
                    case "2":
                        ExibirTarefas();
                        break;
                    case "3":
                        ConcluirTarefa();
                        break;
                    case "4":
                        ExcluirTarefa();
                        break;
                    case "5":
                        SalvarTarefas();
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Pressione qualquer tecla para tentar novamente.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void AdicionarTarefa()
        {
            Console.Write("Digite a descrição da tarefa: ");
            string descricao = Console.ReadLine();
            int novoId = tarefas.Count > 0 ? tarefas[tarefas.Count - 1].Id + 1 : 1;

            tarefas.Add(new Tarefa { Id = novoId, Descricao = descricao, Concluida = false });
            Console.WriteLine("Tarefa adicionada com sucesso!");
            Console.ReadKey();
        }

        static void ExibirTarefas()
        {
            if (tarefas.Count == 0)
            {
                Console.WriteLine("Nenhuma tarefa cadastrada.");
            }
            else
            {
                Console.WriteLine("Tarefas:");
                foreach (var tarefa in tarefas)
                {
                    Console.WriteLine(tarefa);
                }
            }
            Console.ReadKey();
        }

        static void ConcluirTarefa()
        {
            Console.Write("Digite o ID da tarefa a ser concluída: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var tarefa = tarefas.Find(t => t.Id == id);
                if (tarefa != null)
                {
                    tarefa.Concluida = true;
                    Console.WriteLine("Tarefa concluída com sucesso!");
                }
                else
                {
                    Console.WriteLine("Tarefa não encontrada.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
            Console.ReadKey();
        }

        static void ExcluirTarefa()
        {
            Console.Write("Digite o ID da tarefa a ser excluída: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var tarefa = tarefas.Find(t => t.Id == id);
                if (tarefa != null)
                {
                    tarefas.Remove(tarefa);
                    Console.WriteLine("Tarefa excluída com sucesso!");
                }
                else
                {
                    Console.WriteLine("Tarefa não encontrada.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
            Console.ReadKey();
        }

        static void SalvarTarefas()
        {
            using (StreamWriter writer = new StreamWriter(caminhoArquivo))
            {
                foreach (var tarefa in tarefas)
                {
                    writer.WriteLine($"{tarefa.Id}|{tarefa.Descricao}|{tarefa.Concluida}");
                }
            }
            Console.WriteLine("Tarefas salvas com sucesso!");
        }

        static void CarregarTarefas()
        {
            if (File.Exists(caminhoArquivo))
            {
                using (StreamReader reader = new StreamReader(caminhoArquivo))
                {
                    string linha;
                    while ((linha = reader.ReadLine()) != null)
                    {
                        var partes = linha.Split('|');
                        tarefas.Add(new Tarefa
                        {
                            Id = int.Parse(partes[0]),
                            Descricao = partes[1],
                            Concluida = bool.Parse(partes[2])
                        });
                    }
                }
            }
        }
    }
}
