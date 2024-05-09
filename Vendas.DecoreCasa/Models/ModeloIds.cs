using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendas.DecoreCasa.Models
{

    public class ModelIds
    {
        public Retorno retorno { get; set; }
    }

    public class Retorno
    {
        public string status_processamento { get; set; }
        public string status { get; set; }
        public string pagina { get; set; }
        public int numero_paginas { get; set; }
        public Pedido[] pedidos { get; set; }

        public string codigo_erro { get; set; } 
    }

    public class Pedido
    {
        public Pedido1 pedido { get; set; }
    }

    public class Pedido1
    {
        public string id { get; set; }
        public string numero { get; set; }
        public string numero_ecommerce { get; set; }
        public string data_pedido { get; set; }
        public string data_prevista { get; set; }
        public string nome { get; set; }
        public float valor { get; set; }
        public string id_vendedor { get; set; }
        public string nome_vendedor { get; set; }
        public string situacao { get; set; }
        public string codigo_rastreamento { get; set; }
        public string url_rastreamento { get; set; }
    }

}
