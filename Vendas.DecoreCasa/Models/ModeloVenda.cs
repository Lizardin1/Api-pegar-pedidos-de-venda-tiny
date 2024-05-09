namespace ModeloVenda
{

    public class VendaModel
        {
            public Retorno? retorno { get; set; }
        }

        public class Retorno
        {
            public string? status_processamento { get; set; }
            public string? status { get; set; }
            public Pedido? pedido { get; set; }

            public string? codigo_erro { get; set; }
        }

        public class Pedido
        {
            public string? id { get; set; }
            public string? numero { get; set; }
            public string? numero_ecommerce { get; set; }
            public string? data_pedido { get; set; }
            public string? data_prevista { get; set; }
            public string? data_faturamento { get; set; }
            public string? data_envio { get; set; }
            public string? data_entrega { get; set; }
            public object? id_lista_preco { get; set; }
            public string? descricao_lista_preco { get; set; }
            public Cliente? cliente { get; set; }
            public Iten[] itens { get; set; }
            public Parcela[] parcelas { get; set; }
            public object[] marcadores { get; set; }
            public string? condicao_pagamento { get; set; }
            public string? forma_pagamento { get; set; }
            public object? meio_pagamento { get; set; }
            public string? nome_transportador { get; set; }
            public string? frete_por_conta { get; set; }
            public string? valor_frete { get; set; }
            public decimal? valor_desconto { get; set; }
            public string? outras_despesas { get; set; }
            public string? total_produtos { get; set; }
            public string? total_pedido { get; set; }
            public string? numero_ordem_compra { get; set; }
            public string? deposito { get; set; }
            public Ecommerce? ecommerce { get; set; }
            public string? forma_envio { get; set; }
            public string? situacao { get; set; }
            public string? obs { get; set; }
            public string? obs_interna { get; set; }
            public string? id_vendedor { get; set; }
            public string? codigo_rastreamento { get; set; }
            public string? url_rastreamento { get; set; }
            public string? id_nota_fiscal { get; set; }
            public string? id_natureza_operacao { get; set; }
        }

        public class Cliente
        {
            public string? nome { get; set; }
            public string? codigo { get; set; }
            public object? nome_fantasia { get; set; }
            public string? tipo_pessoa { get; set; }
            public string? cpf_cnpj { get; set; }
            public string? ie { get; set; }
            public string? rg { get; set; }
            public string? endereco { get; set; }
            public string? numero { get; set; }
            public string? complemento { get; set; }
            public string? bairro { get; set; }
            public string? cidade { get; set; }
            public string? uf { get; set; }
            public string? fone { get; set; }
            public string? email { get; set; }
            public string? cep { get; set; }
        }

        public class Ecommerce
        {
            public string? id { get; set; }
            public string? numeroPedidoEcommerce { get; set; }
            public string? numeroPedidoCanalVenda { get; set; }
        }

        public class Iten
        {
            public Item? item { get; set; }
        }

        public class Item
        {
            public string? id_produto { get; set; }
            public string? codigo { get; set; }
            public string? descricao { get; set; }
            public string? unidade { get; set; }
            public string? quantidade { get; set; }
            public string? valor_unitario { get; set; }
        }

        public class Parcela
        {
            public Parcela1? parcela { get; set; }
        }

        public class Parcela1
        {
            public string? dias { get; set; }
            public string? data { get; set; }
            public string? valor { get; set; }
            public string? obs { get; set; }
            public string? forma_pagamento { get; set; }
            public object? meio_pagamento { get; set; }
        }
    
}