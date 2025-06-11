using MySql.Data.MySqlClient;
using System.Data;
using ProjetoCadastroDeProdutos.Models;


namespace ProjetoCadastroDeProdutos.Repositorio
{
    public class UsuarioRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");

        public Usuario ObterUsuario(string email)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new("SELECT * FROM Usuarios WHERE Email = @email", conexao);
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;

                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    Usuario usuario = null;
                    if (dr.Read())
                    {
                        usuario = new Usuario
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = dr["Nome"].ToString(),
                            Email = dr["Email"].ToString(),
                            Senha = dr["Senha"].ToString()
                        };
                    }

                    return usuario;
                }
            }
        }

        // Define um método público para adicionar um novo usuário ao banco de dados. Recebe um objeto 'Usuario' como parâmetro.
        public void AdicionarUsuario(Usuario usuario)
        {
            /* Cria uma nova instância da conexão MySQL dentro de um bloco 'using'.
             Isso garante que a conexão será fechada e descartada corretamente após o uso, mesmo em caso de erro.*/
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                // Abre a conexão com o banco de dados MySQL.

                conexao.Open();
                /* Cria um novo comando SQL para inserir dados na tabela 'Usuario'. Os valores para Nome, Email e Senha são passados como parâmetros
                 (@Nome, @Email, @Senha) para evitar SQL Injection.*/

                MySqlCommand cmd = new("INSERT INTO Usuarios (Nome, Email, Senha) VALUES (@Nome,@Email,@Senha)", conexao);
                // Adiciona um parâmetro ao comando SQL para o campo 'Nome', utilizando o valor da propriedade 'Nome' do objeto 'usuario'.
                cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
                // Adiciona um parâmetro ao comando SQL para o campo 'Email', utilizando o valor da propriedade 'Email' do objeto 'usuario'.
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                // Adiciona um parâmetro ao comando SQL para o campo 'Senha', utilizando o valor da propriedade 'Senha' do objeto 'usuario'.
                cmd.Parameters.AddWithValue("@Senha", usuario.Senha);
                // Executa o comando SQL INSERT no banco de dados. Retorna o número de linhas afetadas.
                cmd.ExecuteNonQuery();
                // Fecha a conexão com o banco de dados (embora o 'using' já faria isso, só garante o fechamento).
                conexao.Close();
            }
        }
    }
}
