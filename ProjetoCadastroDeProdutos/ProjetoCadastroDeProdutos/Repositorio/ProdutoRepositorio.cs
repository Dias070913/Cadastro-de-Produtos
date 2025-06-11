using MySql.Data.MySqlClient;
using System.Data;
using ProjetoCadastroDeProdutos.Models;

namespace ProjetoCadastroDeProdutos.Repositorio
{
    public class ProdutoRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");

        public Produto ObterProduto(string nome)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new("SELECT * FROM Produtos WHERE Nome = @nome", conexao);
                cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = nome;

                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    Produto produto = null;
                    if (dr.Read())
                    {
                        produto = new Produto
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = dr["Nome"].ToString(),
                            Descricao = dr["Descricao"].ToString(),
                            Preco = Convert.ToDecimal(dr["Preco"]),
                            Quantidade = Convert.ToDecimal(dr["Quantidade"])
                        };
                    }

                    return produto;
                }
            }
        }

        // Define um método público para adicionar um novo usuário ao banco de dados. Recebe um objeto 'Usuario' como parâmetro.
        public void AdicionarProduto(Produto produto)
        {
            /* Cria uma nova instância da conexão MySQL dentro de um bloco 'using'.
             Isso garante que a conexão será fechada e descartada corretamente após o uso, mesmo em caso de erro.*/
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                // Abre a conexão com o banco de dados MySQL.

                conexao.Open();
                /* Cria um novo comando SQL para inserir dados na tabela 'Usuario'. Os valores para Nome, Email e Senha são passados como parâmetros
                 (@Nome, @Email, @Senha) para evitar SQL Injection.*/

                MySqlCommand cmd = new("INSERT INTO Produtos (Nome, Descricao, Preco, Quantidade) VALUES (@Nome,@Descricao,@Preco,@Quantidade)", conexao);
                // Adiciona um parâmetro ao comando SQL para o campo 'Nome', utilizando o valor da propriedade 'Nome' do objeto 'usuario'.
                cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                // Adiciona um parâmetro ao comando SQL para o campo 'Email', utilizando o valor da propriedade 'Email' do objeto 'usuario'.
                cmd.Parameters.AddWithValue("@Descricao", produto.Descricao);
                // Adiciona um parâmetro ao comando SQL para o campo 'Senha', utilizando o valor da propriedade 'Senha' do objeto 'usuario'.
                cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                // Adiciona um parâmetro ao comando SQL para o campo 'Senha', utilizando o valor da propriedade 'Senha' do objeto 'usuario'.
                cmd.Parameters.AddWithValue("@Quantidade", produto.Quantidade);
                // Executa o comando SQL INSERT no banco de dados. Retorna o número de linhas afetadas.
                cmd.ExecuteNonQuery();
                // Fecha a conexão com o banco de dados (embora o 'using' já faria isso, só garante o fechamento).
                conexao.Close();
            }
        }
    }
}
