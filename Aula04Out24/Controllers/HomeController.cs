using Aula04Out24.Models;
using System;
using System.Linq;
using System.Web.Mvc;
namespace Aula04Out24.Controllers
{
    public class HomeController : Controller
    {
        private readonly BD_MorandoDaCidadeEntities db;

        // Construtor padrão
        public HomeController()
        {
            db = new BD_MorandoDaCidadeEntities(); // Inicializa o contexto aqui
        }

        // Método para login e cadastro de novo cliente
        [HttpPost]
        public ActionResult Login(string nomeCliente, string emailCliente, string dataDeNascimentoCliente, string cpfCliente, string telefoneCliente, string senhaCliente)
        {
            if (ModelState.IsValid)
            {
                // Tentar encontrar um cliente existente pelo email
                var clienteExistente = db.Cliente.FirstOrDefault(u => u.EmailCliente == emailCliente);

                if (clienteExistente != null) // Cliente já existe
                {
                    if (clienteExistente.SenhaCliente == senhaCliente) // Senha correta
                    {
                        TempData["ToastrMessage"] = "Login realizado com sucesso!";
                        TempData["ToastrType"] = "success"; // Tipo da mensagem
                        return RedirectToAction("Serviço"); // Redireciona para a página de serviço
                    }
                    else // Senha incorreta
                    {
                        TempData["ToastrMessage"] = "Senha incorreta!";
                        TempData["ToastrType"] = "error"; // Tipo de mensagem de erro
                        return View(); // Retorna à view para tentar novamente
                    }
                }
                else // Cliente não existe
                {
                    // Cria um novo cliente
                    var novoCliente = new Cliente
                    {
                        NomeCliente = nomeCliente,
                        EmailCliente = emailCliente,
                        DataDeNascimentoCliente = dataDeNascimentoCliente, // Certifique-se de que o formato é válido
                        CPFCliente = cpfCliente,
                        TelefoneCliente = telefoneCliente,
                        SenhaCliente = senhaCliente
                    };

                    // Adiciona o novo cliente ao banco de dados
                    db.Cliente.Add(novoCliente);
                    db.SaveChanges();

                    TempData["ToastrMessage"] = "Cadastro realizado com sucesso!";
                    TempData["ToastrType"] = "success"; // Tipo da mensagem
                    return RedirectToAction("Serviço"); // Redireciona para a página de serviço
                }
            }
            else
            {
                TempData["ToastrMessage"] = "Preencha todos os campos corretamente!";
                TempData["ToastrType"] = "error"; // Tipo de mensagem de erro
            }

            return View(); // Retorna a view em caso de erro
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Produtos()
        {
            return View();
        }

        public ActionResult Serviço()
        {
            return View();
        }

        public ActionResult Sobre()
        {
            return View();
        }
    }
}
