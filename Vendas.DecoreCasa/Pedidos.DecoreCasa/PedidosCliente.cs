using Dapper;
using ModeloCliente;
using System.Data;
using System.Data.SqlClient;

namespace Pedidos.Decore.Geral.Pedidos.DecoreCasa
{
    public class Clientes
    {
    
        public static async Task PedidosCliente(ClienteModel clienteModel, string id)
        {
            VariaveisAmb variaveis = new VariaveisAmb();
            try
            {
                var cliente = clienteModel.retorno.pedido.cliente;

                DynamicParameters param = new DynamicParameters();

                param.Add("@id", id);
                param.Add("@nome", cliente.nome);
                param.Add("@codigo", cliente.codigo);
                param.Add("@tipo_pessoa", cliente.tipo_pessoa);
                param.Add("@cpf_cnpj", cliente.cpf_cnpj);
                param.Add("@rg", cliente.rg);
                param.Add("@endereco", cliente.endereco);
                param.Add("@numero", cliente.numero);
                param.Add("@complemento", cliente.complemento);
                param.Add("@bairro", cliente.bairro);
                param.Add("@cidade", cliente.cidade);
                param.Add("@uf", cliente.uf);
                param.Add("@fone", cliente.fone);
                param.Add("@email", cliente.email);
                param.Add("@cep", cliente.cep);

                using (SqlConnection connection = new SqlConnection(variaveis.Sqlconnection))
                {
                    int registrado = connection.Execute(variaveis.QueryInserirCliente, param, commandType: CommandType.Text);
                    if (registrado != 0)
                    {
                        Console.WriteLine($"Cliente: {cliente.nome} registrado");
                    }
                }
            }
            catch(NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }
         
        }
    }
}
