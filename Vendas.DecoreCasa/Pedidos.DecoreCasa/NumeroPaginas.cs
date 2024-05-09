using Newtonsoft.Json;

namespace Pedidos.Decore.Geral.Pedidos.DecoreCasa
{
    public class Paginas
    {
        public static async Task<int> NumeroPagina()
        {
            VariaveisAmb variaveis = new VariaveisAmb();
            using(HttpClient client = new HttpClient())
            {
                var request = await client.GetAsync($"https://api.tiny.com.br/api2/pedidos.pesquisa.php?formato=json&token={variaveis.token}");
                var content = await request.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<dynamic>(content);
                var pagina = response.retorno.numero_paginas;
                var pag = pagina - 5;
                return pag;
            }
        }
    }
}
