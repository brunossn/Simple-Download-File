using System;
using System.Linq;
using System.IO;
using System.Net;

namespace Simple_Download_File
{
    class Program
    {
        // Arg 0: URL do arquivo a ser baixado
        // Arg 1: Caminho local a salvar
        static void Main(string[] args)
        {
            if (!ValidateArgs(args)) return;

            var url = args[0];
            var local = args[1];

            using (var wc = new WebClient())
            {
                Console.WriteLine($"Fazendo o download do arquivo {url}...");

                try
                {
                    wc.DownloadFile(url, local);
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Erro ao fazer download do arquivo. Motivo: {ex.Message}");
                    Console.WriteLine($"Detalhes técnicos: {ex.ToString()}");
                }
            }
        }

        static bool ValidateArgs(string[] args)
        {
            // Número de parâmetros
            if(args.Length != 2)
            {
                Console.WriteLine("Você deve passar dois parâmetros após a chamada do aplicativo:");
                Console.WriteLine("1 - URL do arquivo a ser baixado");
                Console.WriteLine("2 - Caminho local a salvar o arquivo");

                return false;
            }

            // Parâmetro vazio
            if(args.Count(a => string.IsNullOrWhiteSpace(a)) > 0)
            {
                Console.WriteLine("Todos os parâmetros são obrigatórios");

                return false;
            }

            // Caminho físico válido
            var diretorio = Path.GetFullPath(args[1]);
            if (!Directory.Exists(diretorio))
            {
                try
                {
                    Directory.CreateDirectory(diretorio);
                }
                catch
                {
                    return false;
                }
            }

            // URL válida
            if(!(Uri.IsWellFormedUriString(args[1], UriKind.Absolute)))
            {
                Console.WriteLine("URL inválida no parâmetro 2.");
                return false;
            }

            return true;
        }
    }
}
