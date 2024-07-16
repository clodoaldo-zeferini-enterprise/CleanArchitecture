using Application.DTO.Request;
using Application.DTO.Response;
using Infrastructure.Base.Abstractions.Grupo;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text.Json;

namespace Infrastructure.Redis.Service.Grupo
{
    public class GrupoServiceQuery : IGrupoServiceQuery
    {
        private readonly IDistributedCache _redisCache;
        private GrupoOutResponse _grupoOutResponse;
        private string _userId;

        public GrupoServiceQuery(IDistributedCache redisCache, string userId)
        {
            _grupoOutResponse = new GrupoOutResponse();
            _userId = userId;

            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        /*
        public async Task<GrupoOutResponse> GetGrupoById(string id)
        {
            var serializedGrupo = await _redisCache.GetStringAsync(id);
            if (serializedGrupo == null)
            {
                _grupoOutResponse.SetResultado(true);
                _grupoOutResponse.SetMensagem("Registro não encontrado!");
            }
            else
            {
                var grupo = JsonConvert.DeserializeObject<Application.DTO.Response.Grupo.Grupo>(serializedGrupo);

                _grupoOutResponse.SetData(grupo);
                _grupoOutResponse.SetResultado(true);
                _grupoOutResponse.SetMensagem("Grupo Obtido com Sucesso!");
            }

            return _grupoOutResponse;
        }        
        */
        public async Task<GrupoOutResponse> GetGrupos(GetFrom getFrom)
        {
            var allGrupoInstance = await _redisCache.GetStringAsync("Grupo");

            if (string.IsNullOrEmpty(allGrupoInstance))
            {
                _grupoOutResponse.SetResultado(true);
                _grupoOutResponse.SetMensagem("Não foi encontrado nenhum registro que atenda as condições do Filtro!");
                return _grupoOutResponse;
            }

            if (allGrupoInstance.Substring(0, 1) == "{")
                allGrupoInstance = $"[{allGrupoInstance}]"; // Adiciona "[" e "]" para transformar em um array de objetos
            else
                allGrupoInstance = allGrupoInstance.Replace("}{", "},{"); // Adiciona "," para separar os objetos "}{ 

            try
            {
                var grupos = JsonConvert.DeserializeObject<List<Application.DTO.Response.Grupo.Grupo>>(allGrupoInstance);

                var IsIdValido = false;

                if (getFrom.Id == null) IsIdValido = false;
                else IsIdValido = Guid.TryParse(getFrom.Id.ToString(), out Guid isIdValido);
                                
                if (IsIdValido)
                {
                    grupos = grupos.Where(m => m.Id == getFrom.Id).ToList();

                    _grupoOutResponse.SetResultado(true);
                    _grupoOutResponse.SetMensagem("Seguem registros que atendem as condições do Filtro!");
                    _grupoOutResponse.SetData(grupos);
                    return _grupoOutResponse;
                }


                // Aplicar filtros
                if (getFrom.FiltraPorNome)
                {
                    grupos = grupos.Where(m => m.Name.Contains(getFrom.FiltroNome, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (getFrom.FiltraUpdatedOn)
                {
                    if (getFrom.DataInicial.HasValue)
                    {
                        grupos = grupos.Where(m => m.UpdatedOn >= getFrom.DataInicial.Value).ToList();
                    }

                    if (getFrom.DataFinal.HasValue)
                    {
                        grupos = grupos.Where(m => m.UpdatedOn <= getFrom.DataFinal.Value).ToList();
                    }
                }

                if (getFrom.FiltraStatus)
                {
                    grupos = grupos.Where(m => m.Status == getFrom.Status).ToList();
                }

                // Paginação
                grupos = grupos.Skip((getFrom.PageNumber - 1) * getFrom.PageSize).Take(getFrom.PageSize).ToList();
                
                _grupoOutResponse.SetResultado(true);
                _grupoOutResponse.SetMensagem("Seguem registros que atendem as condições do Filtro!");
                _grupoOutResponse.SetData(grupos);

            }
            catch (Exception ex)
            {
                string sEx = ex.Message;
            }
            return _grupoOutResponse;
        }
    }
}
