using Dapper;
using ModeloVenda;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using static Vendas.DecoreCasa.Pedidos.DecoreCasa.ConsultarEInserirPedidos;


namespace Vendas.DecoreCasa.Pedidos.DecoreCasa
{
    public class UpdateDetalhesVenda
    {
        public static List<id_modelo> ids;
        public static VendaModel vendaModelUpdate;
        public static async Task UpdatePedidos()
        {
            Variaveis variaveis = new Variaveis();

            using (SqlConnection connection = new SqlConnection(variaveis.Sqlconnection))
            {
                ids = connection.Query<id_modelo>("Select top(2000) * from DetalhesVenda order by numero desc").ToList();
            }

            int Nid = 1;
            foreach (var id in ids)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var response = await client.GetAsync($"https://api.tiny.com.br/api2/pedido.obter.php?formato=json&token={variaveis.token}&id={id.id_pedido}");
                        if(response.StatusCode == HttpStatusCode.OK)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            vendaModelUpdate = JsonConvert.DeserializeObject<VendaModel>(content);
                        }
                        else
                        {
                            continue;
                        }
                    
                    }
                    var ite = vendaModelUpdate.retorno.pedido;

                    DynamicParameters param = new DynamicParameters(); 
                    param.Add("@id_pedido", id.id_pedido);
                    param.Add("@situacao", ite.situacao);
                    if (ite.ecommerce == null)
                    {
                        param.Add("@id_ecommerce", "null");
                    }
                    else
                    {
                        param.Add("@id_ecommerce", ite.ecommerce.id);
                    }
                    param.Add("@data_envio", ite.data_envio);
                    param.Add("@data_entregue", ite.data_entrega);
                    param.Add("@nome_transportador", ite.nome_transportador);
                    param.Add("@data_faturamento", ite.data_faturamento);

                    using (SqlConnection connection = new SqlConnection(variaveis.Sqlconnection))
                    {
                        int registrado = connection.Execute(variaveis.QueryUpdateDadosDaVenda, param, commandType: CommandType.Text);
                        if(registrado != 0)
                        {
                            Console.WriteLine($"Pedido Atualizado {Nid} de {ids.Count}");
                        }
                    }
                    Task.Delay(1000).Wait();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Nid++;
            }
        }
    }
}
