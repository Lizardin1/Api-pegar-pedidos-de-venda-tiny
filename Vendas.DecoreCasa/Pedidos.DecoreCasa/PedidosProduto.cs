using Dapper;
using ModeloProduto;
using System.Data;
using System.Data.SqlClient;

namespace Pedidos.Decore.Geral.Pedidos.DecoreCasa
{
    public class Produto
    {
        public static async Task PedidosProduto(ProdutoModel produtoModel, string id)
        {
            VariaveisAmb variaveis = new VariaveisAmb();
            try
            {
                foreach (var item1 in produtoModel.retorno.pedido.itens)
                {
                    DynamicParameters param = new DynamicParameters();

                    param.Add("@id", id);
                    param.Add("@id_produto", item1.item.id_produto);
                    param.Add("@sku", item1.item.codigo);
                    param.Add("@descricao", item1.item.descricao);
                    param.Add("@unidade", "UN");
                    param.Add("@quantidade", item1.item.quantidade);
                    param.Add("@valor_unitario", item1.item.valor_unitario);

                    using (SqlConnection connection = new SqlConnection(variaveis.Sqlconnection))
                    {
                        int produtos_registrados = connection.Execute(variaveis.QueryInserirProduto, param, commandType: CommandType.Text);
                        if (produtos_registrados != 0)
                        {
                            Console.WriteLine($"Produto: {item1.item.descricao} registrado");
                            Console.WriteLine("------------------------------------------------------------------");
                        }
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
