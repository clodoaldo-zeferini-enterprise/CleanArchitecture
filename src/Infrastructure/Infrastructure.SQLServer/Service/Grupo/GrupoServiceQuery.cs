using Application.DTO.Request;
using Application.DTO.Response;
using Infrastructure.Base.Abstractions.Grupo;
using Infrastructure.SQLServer.Repositories.Base.Dapper;
using Infrastructure.SQLServer.Repositories.Grupo;

namespace Infrastructure.SQLServer.Service.Grupo
{
    public class GrupoServiceQuery : IGrupoServiceQuery
    {
        private IGrupoDapperRepository _grupoDapperRepository; 
        private GrupoOutResponse _grupoOutResponse;
        private string _userId;

        public GrupoServiceQuery(IGrupoDapperRepository grupoDapperRepository, string userId)
        {
            _grupoDapperRepository = grupoDapperRepository;
            _grupoOutResponse = new GrupoOutResponse();
            _userId = userId;
        }

        public async Task<GrupoOutResponse> GetGrupos(GetFrom getFrom)
        {
            try 
            {
                var navigatorGrupos = _grupoDapperRepository.GetMultiple(new DapperQuery(Guid.NewGuid().ToString(), getFrom.Select), new { param = "" },
                    gr => gr.Read<Navigator>()
                  , gr => gr.Read<Domain.Entities.Grupo>()

                );

                var navigators = navigatorGrupos.Item1;
                var grupos = navigatorGrupos.Item2;

                if (navigators.Any() && grupos.Any())
                {
                    _grupoOutResponse.SetData(GrupoMapper.FromSQLDBToDTO(navigators, grupos));

                    _grupoOutResponse.SetResultado(true);
                    _grupoOutResponse.AddMensagem("Dados retornados com sucesso!");
                }
                else
                {
                    _grupoOutResponse.SetResultado(true);
                    _grupoOutResponse.AddMensagem("Nenhum dado encontrado!");
                }
            }
            catch (Exception ex)
            {

                throw;
            }


            return _grupoOutResponse;
        }
    }
}
