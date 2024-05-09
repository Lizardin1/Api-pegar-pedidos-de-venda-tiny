--------------------------------------------Projeto em C# .NET consumindo API do ERP Olist Tiny--------------------------------------------
 
 
 
 Este projeto faz uso de 2 Apis do ERP Olist TIny, a api que busca novos pedidos de venda, e uma api que faz o lançamento de estoque dos produtos vendidos em alguns marketplaces específicos como Amazon e Shopee
 As chamadas são realizadas utilizando a biblioteca nativa do C# para requisições http que é o HttpClient,
 as respostas que são retornadas em Json são tratadas com a biblioteca Newtonsoft.Json
 e os dados são guardados em um banco de dados relacional SqlServer hospedado no serviço de cloud Azure.

 Este projeto contém conhecimentos de:
 - Linguagem C# com .NET core 8.0
 - Linguagem SQL
 - Micro ORM Dapper
 - Requisições API
 - Manipulação de dados
 - CRUD
