using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestaoEquipamentos.ConsoleApp.Dominio;

namespace GestaoEquipamentos.ConsoleApp.Controladores
{
    public class ControladorSolicitante : ControladorBase
    {
        private ControladorChamado controladorChamado;

        public ControladorSolicitante(ControladorChamado controlador)
        {
            controladorChamado = controlador;
        }

        public string RegistrarSolicitante(int id, int idChamadoSolicitante, string nome,
    string email, string telefone)
        {
            Solicitante solicitante = null;

            int posicao;

            if (id == 0)
            {
                solicitante = new Solicitante();
                posicao = ObterPosicaoVaga();
            }
            else
            {
                posicao = ObterPosicaoOcupada(new Solicitante(id));
                solicitante = (Solicitante)registros[posicao];
            }

            solicitante.chamado = controladorChamado.SelecionarChamadoPorId(idChamadoSolicitante);
            solicitante.nome = nome;
            solicitante.email = email;
            solicitante.telefone = telefone;

            string resultadoValidacao = solicitante.Validar();

            if (resultadoValidacao == "SOLICITANTE_VALIDO")
                registros[posicao] = solicitante;

            return resultadoValidacao;
        }

        public bool ExcluirSolicitante(int idSelecionado)
        {
            return ExcluirRegistro(new Solicitante(idSelecionado));
        }

        public Solicitante[] SelecionarTodosSolicitantes()
        {
            Solicitante[] solicitantesAux = new Solicitante[QtdRegistrosCadastrados()];

            Array.Copy(SelecionarTodosRegistros(), solicitantesAux, solicitantesAux.Length);

            return solicitantesAux;
        }
    }
}
