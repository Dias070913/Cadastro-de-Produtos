using MySql.Data.MySqlClient;
using System.Data;
using ProjetoCadastroDeProdutos.Models;
using MySqlX.XDevAPI;

namespace ProjetoCadastroDeProdutos.Repositorio
{
    public class ProdutoRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");

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

        // Método para listar todos os clientes do banco de dados
        public IEnumerable<Produto> TodosProdutos()
        {
            // Cria uma nova lista para armazenar os objetos Cliente
            List<Produto> Produtolist = new List<Produto>();

            // Bloco using para garantir que a conexão seja fechada e os recursos liberados após o uso
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                // Abre a conexão com o banco de dados MySQL
                conexao.Open();
                // Cria um novo comando SQL para selecionar todos os registros da tabela 'cliente'
                MySqlCommand cmd = new MySqlCommand("SELECT * from Produtos", conexao);

                // Cria um adaptador de dados para preencher um DataTable com os resultados da consulta
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                // Cria um novo DataTable
                DataTable dt = new DataTable();
                // metodo fill- Preenche o DataTable com os dados retornados pela consulta
                da.Fill(dt);
                // Fecha explicitamente a conexão com o banco de dados 
                conexao.Close();

                // interage sobre cada linha (DataRow) do DataTable
                foreach (DataRow dr in dt.Rows)
                {
                    // Cria um novo objeto Cliente e preenche suas propriedades com os valores da linha atual
                    Produtolist.Add(
                                new Produto
                                {
                                    Id = Convert.ToInt32(dr["Id"]), // Converte o valor da coluna "Id" para inteiro
                                    Nome = dr["Nome"].ToString(), // Converte o valor da coluna "nome" para string
                                    Descricao = dr["Descricao"].ToString(), // Converte o valor da coluna "Descricao" para string
                                    Preco = Convert.ToDecimal(dr["Preco"]), // Converte o valor da coluna "Preco" para string
                                    Quantidade = Convert.ToDecimal(dr["Quantidade"]), // Converte o valor da coluna "Quantidade" para string
                                });
                }
                // Retorna a lista de todos os clientes
                return Produtolist;
            }
        }

        // Método para buscar um cliente específico pelo seu código (Codigo)
        public Produto ObterProduto(int Id)
        {
            // Bloco using para garantir que a conexão seja fechada e os recursos liberados após o uso
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                // Abre a conexão com o banco de dados MySQL
                conexao.Open();
                // Cria um novo comando SQL para selecionar um registro da tabela 'cliente' com base no código
                MySqlCommand cmd = new MySqlCommand("SELECT * from Produtos where Id=@Id ", conexao);

                // Adiciona um parâmetro para o código a ser buscado, definindo seu tipo e valor
                cmd.Parameters.AddWithValue("@Id", Id);

                // Cria um adaptador de dados (não utilizado diretamente para ExecuteReader)
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                // Declara um leitor de dados do MySQL
                MySqlDataReader dr;
                // Cria um novo objeto Cliente para armazenar os resultados
                Produto produto = new Produto();

                /* Executa o comando SQL e retorna um objeto MySqlDataReader para ler os resultados
                CommandBehavior.CloseConnection garante que a conexão seja fechada quando o DataReader for fechado*/

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                // Lê os resultados linha por linha
                while (dr.Read())
                {
                    // Preenche as propriedades do objeto Produto com os valores da linha atual
                    produto.Id = Convert.ToInt32(dr["Id"]);//propriedade Codigo e convertendo para int
                    produto.Nome = dr["Nome"].ToString(); // propriedade Nome e passando string
                    produto.Descricao = dr["Descricao"].ToString(); //propriedade telefone e passando string
                    produto.Preco = Convert.ToDecimal(dr["Preco"]); //propriedade email e passando string
                    produto.Quantidade = Convert.ToDecimal(dr["Quantidade"]); //propriedade email e passando string
                }
                // Retorna o objeto Cliente encontrado (ou um objeto com valores padrão se não encontrado)
                return produto;

            }
        }
    }
}
