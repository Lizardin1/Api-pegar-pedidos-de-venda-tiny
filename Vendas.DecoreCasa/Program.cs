using Pedidos.Decore.Geral.Pedidos.DecoreCasa;
using Vendas.DecoreCasa.Pedidos.DecoreCasa;

namespace Pedidos.Decore.Geral
{
    class Program
    {
        public static List<string> ids_pedidos = new List<string>();
        public static List<string> idsBdStrings;
        static async Task Main()
        {

            Variaveis variaveis = new Variaveis();
            var client = new HttpClient();
            var Data = DateTime.Now;
            int pag = await Paginas.NumeroPagina();
            
            try
            {
                while (true)
                {
           
                    ids_pedidos = await Ids.ChamadaApiIds(pag);

                    await ConsultarEInserirPedidos.PedidosTinyErp(ids_pedidos);

                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Ocorreu um erro de HTTP: {ex.Message} {DateTime.Now}");
                Task.Delay(60000).Wait();
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message} {DateTime.Now}");
                Task.Delay(1000).Wait();

                if (ex.InnerException == null)
                {
                    return;
                }
            }
        }
    }
}
