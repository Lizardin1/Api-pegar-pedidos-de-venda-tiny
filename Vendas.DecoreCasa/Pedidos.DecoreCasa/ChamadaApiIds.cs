using Newtonsoft.Json;
using Vendas.DecoreCasa.Models;

namespace Pedidos.Decore.Geral.Pedidos.DecoreCasa
{
    public class Ids
    {
        public static ModelIds response = new ModelIds();
        public static List<string> ids_pedidos = new List<string>();
        public static async Task<List<string>> ChamadaApiIds(int pag)
        {
            VariaveisAmb variaves = new VariaveisAmb();

            try
            {
                Console.WriteLine("BUSCA DE ID's EM EXECUÇÃO...");

                while (true)
                {
                    pag++;
                    using (HttpClient client = new HttpClient())
                    {
                        var request = await client.GetAsync($"https://api.tiny.com.br/api2/pedidos.pesquisa.php?formato=json&token={variaves.token}&pagina={pag}");
                        var content = await request.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<ModelIds>(content.ToString());
                    }
                   
                    Console.WriteLine(pag);

                    if (response.retorno.codigo_erro == "23")
                    {
                        Console.WriteLine("PÁGINA NÃO ENCONTRADA");
                        break;
                    }
                    if (response.retorno.codigo_erro == "6")
                    {
                        Console.WriteLine("Acesso bloqueado");
                        Task.Delay(1000).Wait();
                        continue;
                    }
                    if (response.retorno.codigo_erro == "5")
                    {
                        Task.Delay(1000).Wait();
                        continue;
                    }
                    if (response.retorno.codigo_erro == "11")
                    {
                        Task.Delay(1000).Wait();
                        continue;
                    }
                    if (response.retorno.status_processamento == "1")
                    {
                        Task.Delay(1000).Wait();
                        continue;
                    }

                    foreach (var item in response.retorno.pedidos)
                    {
                        ids_pedidos.Add(item.pedido.id);
                    }
                    Task.Delay(500).Wait();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ids_pedidos;
        }
    }
}
