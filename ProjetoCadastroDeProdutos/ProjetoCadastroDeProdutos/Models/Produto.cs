﻿namespace ProjetoCadastroDeProdutos.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco {  get; set; }
        public decimal Quantidade { get; set; }

    }
}
