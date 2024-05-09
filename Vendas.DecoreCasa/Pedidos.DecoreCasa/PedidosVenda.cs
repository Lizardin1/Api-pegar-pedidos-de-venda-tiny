using Dapper;
using ModeloVenda;
using System.Data;
using System.Data.SqlClient;

namespace Pedidos.Decore.Geral.Pedidos.DecoreCasa
{
    public class Venda
    {
        public static async Task PedidosVenda(VendaModel vendaModel)
        {
            VariaveisAmb variaveis = new VariaveisAmb();
            try
            {
                var ite = vendaModel.retorno.pedido;

                DynamicParameters param = new DynamicParameters();

                param.Add("@id_pedido", ite.id);
                param.Add("@situacao", ite.situacao);
                param.Add("@numero", ite.numero);
                param.Add("@numero_ecommerce", ite.numero_ecommerce ?? "null");
                param.Add("@data_pedido", ite.data_pedido);
                param.Add("@data_prevista", ite.data_prevista);
                param.Add("@data_faturamento", ite.data_faturamento);
                param.Add("@data_envio", ite.data_envio);
                param.Add("@data_entrega", ite.data_entrega);
                param.Add("@descricao_lista_preco", ite.descricao_lista_preco);
                param.Add("@condicao_pagamento", ite.condicao_pagamento);
                param.Add("@forma_pagamento", ite.forma_pagamento);
                param.Add("@nome_transportador", ite.nome_transportador ?? "null");
                param.Add("@frete_por_conta", ite.frete_por_conta);
                param.Add("@valor_frete", ite.valor_frete);
                param.Add("@valor_desconto", ite.valor_desconto);
                param.Add("@outras_despesas", ite.outras_despesas);
                param.Add("@total_produtos", ite.total_produtos);
                param.Add("@total_pedido", ite.total_pedido);
                param.Add("@id_no_ecommerce", ite.numero_ordem_compra);
                param.Add("@deposito", ite.deposito);
                param.Add("@forma_envio", ite.forma_envio);
                param.Add("@codigo_rastreamento", ite.codigo_rastreamento);
                param.Add("@url_rastreamento", ite.url_rastreamento ?? "null");
                param.Add("@id_nota_fiscal", ite.id_nota_fiscal);
                param.Add("@id_natureza_operacao", ite.id_natureza_operacao);
                if (ite.ecommerce == null)
                {
                    param.Add("@id_ecommerce", "null");
                }
                else
                {  
                     param.Add("@id_ecommerce", ite.ecommerce.id ??  null);
                    
                }

                using (SqlConnection connection = new SqlConnection(variaveis.Sqlconnection))
                {
                    int pedido_registrado = connection.Execute(variaveis.QueryInserirDadosDaVenda, param, commandType: CommandType.Text);
                    if (pedido_registrado != 0)
                    {
                        Console.WriteLine($"Pedido número: {ite.numero} registrado");
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            List<string> idsElegiveis = new List<string> { "9662", "9593", "9621" };

            if (!string.IsNullOrEmpty(vendaModel.retorno.pedido.ecommerce.id) && vendaModel.retorno.pedido.situacao != "Dados incompletos" && idsElegiveis.Contains(vendaModel.retorno.pedido.ecommerce.id))
            { 
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        var request = await client.GetAsync($"https://api.tiny.com.br/api2/pedido.lancar.estoque.php?formato=json&token={variaveis.token}&id={vendaModel.retorno.pedido.id}");
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return;
                    }  
                }
            }
        }
    }
}
