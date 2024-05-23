using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Base.Resource
{
    public static class Get
    {
        public static string SysUsuSessionIdInvalido = "SysUsuSessionId inválido";
        public static string RequestIdInvalido = "RequestId inválido";

        public static string IdInvalido = "Id inválido";

        public static string NomeInvalido = "Nome do Member inválido";
        public static string JaExisteUmMemberComEsteNome = "Já existe um member com este Nome";

        public static string PageNumberInvalido = "O Número da Página deverá estar entre 1 e 1000";
        public static string PageSizeInvalido = "O Tamanho da Página deverá estar entre 1 e 100";
        public static string FiltroNomeInvalido = "O Tamanho de FiltroNome deverá estar entre 1 e 100";
        public static string IntervaloDeDataInvalido = "O Intervalo de Data é Inválido";
        public static string StatusInvalido = "O Status é Inválido";

        public static string DataInicialInvalida = "A DataInicial é Inválida";
        public static string DataFinalInvalida = "A DataFinal é Inválida";

    }
}
