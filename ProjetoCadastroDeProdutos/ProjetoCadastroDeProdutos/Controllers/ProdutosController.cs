using Microsoft.AspNetCore.Mvc;
using ProjetoCadastroDeProdutos.Models;
using ProjetoCadastroDeProdutos.Repositorio;

namespace ProjetoCadastroDeProdutos.Controllers
{
    public class ProdutosController : Controller
    {

        /* DECLARANDO UMA VÁRIAVEL PRIVADA SOMENTE PARA LEITURA DO TIPO UsuarioRepositorio
         * chamada _usuarioRepositorio*/
        private readonly ProdutoRepositorio _produtoRepositorio;

        /* DEFININDO O CONSTRUTOR DA CLASSE USUARIOCONTROLLER QUE VAI RECEBER UMA INSTANCIA DE
         * UsuarioRepositorio */
        public ProdutosController(ProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Produto produto)
        {
            // Verifica se o ModelState é válido. O ModelState é considerado válido se não houver erros de validação.
            if (ModelState.IsValid)
            {
                /* Se o modelo for válido:
                 Chama o método AdicionarUsuario do _usuarioRepositorio, passando o objeto Usuario recebido.
                 Isso  salvará as informações do novo usuário no banco de dados.*/

                _produtoRepositorio.AdicionarProduto(produto);

                /* Redireciona o usuário para a action "Login" deste mesmo Controller (LoginController).
                  após um cadastro bem-sucedido, redirecionará à página de login.*/
                return RedirectToAction("Produto");
            }

            /* Se o ModelState não for válido (houver erros de validação):
             Retorna a View de Cadastro novamente, passando o objeto Usuario com os erros de validação.
             Isso permite que a View exiba os erros para o usuário corrigir o formulário.*/
            return View(produto);

        }
    }
}
