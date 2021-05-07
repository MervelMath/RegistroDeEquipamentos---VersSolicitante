using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestaoEquipamentos.ConsoleApp.Controladores;
using GestaoEquipamentos.ConsoleApp.Dominio;
using System;


namespace GestaoEquipamentos.ConsoleApp.Telas
{
    public class TelaSolicitante : TelaBase
    {
        private TelaChamado telaChamado;
        private ControladorSolicitante controladorSolicitante;

        public TelaSolicitante(TelaChamado tela, ControladorSolicitante controlador)
            : base("Cadastro de Solicitantes")
        {
            telaChamado = tela;
            controladorSolicitante = controlador;
        }
        public override void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo um novo solicitante...");

            bool conseguiuGravar = GravarSolicitante(0);

            if (conseguiuGravar)
                ApresentarMensagem("Solicitante inserido com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar inserir o solicitante", TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }
        public override void EditarRegistro()
        {
            ConfigurarTela("Editando um solicitante...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número do solicitante que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bool conseguiuGravar = GravarSolicitante(id);

            if (conseguiuGravar)
                ApresentarMensagem("Solicitante editado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar editar o solicitante", TipoMensagem.Erro);
                EditarRegistro();
            }
        }
        public override void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo um solicitante...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número do solicitante que deseja excluir: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            bool conseguiuExcluir = controladorSolicitante.ExcluirSolicitante(idSelecionado);

            if (conseguiuExcluir)
                ApresentarMensagem("Solicitante excluído com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar excluir o solicitante", TipoMensagem.Erro);
                ExcluirRegistro();
            }
        }
        public override void VisualizarRegistros()
        {
            ConfigurarTela("Visualizando solicitantes...");

            MontarCabecalhoTabela();

            Solicitante[] solicitantes = controladorSolicitante.SelecionarTodosSolicitantes();

            if (solicitantes.Length == 0)
            {
                ApresentarMensagem("Nenhum solicitante registrado!", TipoMensagem.Atencao);
                return;
            }

            foreach (Solicitante solicitante in solicitantes)
            {
                Console.WriteLine("{0,-10} | {1,-30} | {2,-55} | {3,-25} | {4,-40}", solicitante.nome,
                    solicitante.id, solicitante.chamado.titulo, solicitante.email, solicitante.telefone);
            }
        }
        #region Métodos Privados
        private static void MontarCabecalhoTabela()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("{0,-10} | {1,-30} | {2,-30} | {3,-25} | {4,-30}", "nome", "Id", "Chamado", "email", "telefone");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }

        private bool GravarSolicitante(int idSolicitanteSelecionado)
        {
            telaChamado.VisualizarRegistros();

            Console.Write("Digite o Id do chamado para o solicitante: ");
            int idChamadoSolicitante = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite o nome do solicitante: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o email do solicitante: ");
            string email = Console.ReadLine();

            Console.Write("Digite o telefone do solicitante: ");
            string telefone = Console.ReadLine();

            string resultadoValidacao = controladorSolicitante.
                RegistrarSolicitante(idSolicitanteSelecionado, idChamadoSolicitante, nome, email, telefone);

            bool conseguiuGravar = true;

            if (resultadoValidacao != "SOLICITANTE_VALIDO")
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                conseguiuGravar = false;
            }

            return conseguiuGravar;
        }
        #endregion

    }
}
