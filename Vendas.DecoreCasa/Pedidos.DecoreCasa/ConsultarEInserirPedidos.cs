using Dapper;
using ModeloCliente;
using ModeloProduto;
using ModeloVenda;
using Newtonsoft.Json;
using Pedidos.Decore.Geral.Pedidos.DecoreCasa;
using System.Data.SqlClient;
using System.Net;


namespace Vendas.DecoreCasa.Pedidos.DecoreCasa
{
    public class ConsultarEInserirPedidos
    {
        public static VendaModel vendaModel;
        public static ClienteModel clienteModel;
        public static ProdutoModel produtoModel;
        public static List<string> idsBdStrings;


        public static async Task PedidosTinyErp(List<string> ids_pedidos)
        {
            VariaveisAmb variaveis = new VariaveisAmb();

            using (SqlConnection connection = new SqlConnection(variaveis.Sqlconnection))
            {
                idsBdStrings = connection
                       .Query<id_modelo>("select top(5000) id_pedido from DetalhesVenda order by numero desc")
                       .Select(id => id.id_pedido)
                       .ToList();
            }
            List<string> IdsNaoRegistrados = ids_pedidos.Except(idsBdStrings).ToList();

            foreach (var id in IdsNaoRegistrados)
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        var response = await client.GetAsync($"https://api.tiny.com.br/api2/pedido.obter.php?formato=json&token={variaveis.token}&id={id}");

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            vendaModel = JsonConvert.DeserializeObject<VendaModel>(content);
                            clienteModel = JsonConvert.DeserializeObject<ClienteModel>(content);
                            produtoModel = JsonConvert.DeserializeObject<ProdutoModel>(content);
                        }
                        else
                        {
                            Task.Delay(10000).Wait();
                            continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                await Venda.PedidosVenda(vendaModel);
                await Clientes.PedidosCliente(clienteModel, id);
                await Produto.PedidosProduto(produtoModel, id);
            }
        }
        public class id_modelo
        {
            public string id_pedido { get; set; }
        }
    }
}
