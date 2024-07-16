using Application.DTO.Response;
using Infrastructure.Base.Abstractions.Grupo;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text.Json;


namespace Infrastructure.Redis.Service.Grupo
{
    public class GrupoServiceCommand : IGrupoServiceCommand
    {
        private readonly IDistributedCache _redisCache;
        private GrupoOutResponse _grupoOutResponse;
        private string _userId;

        public GrupoServiceCommand(IDistributedCache redisCache, string userId)
        {
            _grupoOutResponse = new GrupoOutResponse();
            _userId = userId;

            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }       

        private async Task<GrupoOutResponse> SetGrupo(Domain.Entities.Grupo grupo)
        {         
            Entities.Grupo grupoRedis = new Entities.Grupo(
                grupo.FirstName, grupo.LastName, grupo.Gender, grupo.Email, grupo.Status, _userId);

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(10)
            };

            var serializedGrupo = JsonSerializer.Serialize(grupoRedis);
            await _redisCache.SetStringAsync("Grupo", serializedGrupo, options);            

            _grupoOutResponse.SetData(GrupoMapper.FromEntityToDTO(grupo));
            _grupoOutResponse.SetResultado(true);
            _grupoOutResponse.SetMensagem("Grupo Criado com Sucesso!");

            return _grupoOutResponse;
        }

        public async Task<GrupoOutResponse> CreateGrupo(Domain.Entities.Grupo grupo)
        {
            /*Verifico se o Parâmetro é nulo*/
            if (grupo == null)
            {
                _grupoOutResponse.SetResultado(false);
                _grupoOutResponse.SetMensagem("O objeto recebido é nulo.");
                return _grupoOutResponse;
            }

            return await SetGrupo(grupo);
        }

        public async Task<GrupoOutResponse> UpdateGrupo(Domain.Entities.Grupo grupo)
        {
            /*Verifico se o Parâmetro é nulo*/
            if (grupo == null)
            {
                _grupoOutResponse.SetResultado(false);
                _grupoOutResponse.SetMensagem("O objeto recebido é nulo.");
                return _grupoOutResponse;
            }

            var serializedGrupo = await _redisCache.GetStringAsync(grupo.Id);
            if (serializedGrupo == null)
            {
                _grupoOutResponse.SetResultado(true);
                _grupoOutResponse.SetMensagem("Registro não encontrado!");
                return _grupoOutResponse;
            }
            else
            {
                return await SetGrupo(grupo);
            }
        }

        public async Task<GrupoOutResponse> DeleteGrupo(string id)
        {
            bool IsIdValido = false;

            if (id == null)
            {
                IsIdValido = false;
            }
            else
            {
                IsIdValido = Guid.TryParse(id.ToString(), out Guid isIdValido);
            }

            if (!IsIdValido)
            {
                _grupoOutResponse.SetResultado(false);
                _grupoOutResponse.SetMensagem("Id inválido.");
                return _grupoOutResponse;
            }

            var serializedGrupo = await _redisCache.GetStringAsync(id);
            if (serializedGrupo == null)
            {
                _grupoOutResponse.SetResultado(true);
                _grupoOutResponse.SetMensagem("Registro não encontrado!");
                return _grupoOutResponse;
            }
            else
            {
                var grupo = JsonSerializer.Deserialize<Domain.Entities.Grupo>(serializedGrupo);
                grupo.SetStatus(NetCore.Base.Enum.EStatus.INATIVO);
                return await SetGrupo(grupo);
            }
        }

        
        public async Task<Domain.Entities.Grupo> GetGrupoById(string id)
        {
            bool IsIdValido = false;

            if (id == null)
            {
                IsIdValido = false;
            }
            else
            {
                IsIdValido = Guid.TryParse(id.ToString(), out Guid isIdValido);
            }

            if (!IsIdValido)
            {
                _grupoOutResponse.SetResultado(false);
                _grupoOutResponse.SetMensagem("Id inválido.");
                return null;
            }

            var serializedGrupo = await _redisCache.GetStringAsync(id);
            if (serializedGrupo == null)
            {
                _grupoOutResponse.SetResultado(true);
                _grupoOutResponse.SetMensagem("Registro não encontrado!");

                return null;               
            }
            else {
                var grupo = JsonSerializer.Deserialize<Domain.Entities.Grupo>(serializedGrupo);
                                
                _grupoOutResponse.SetResultado(true);
                _grupoOutResponse.SetMensagem("Grupo Obtido com Sucesso!");

                return grupo;
            }
        }        
    }
}
