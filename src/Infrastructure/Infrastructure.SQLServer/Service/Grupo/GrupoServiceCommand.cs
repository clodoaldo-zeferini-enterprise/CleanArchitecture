using Application.DTO.Response;
using Infrastructure.Base;
using Infrastructure.Base.Abstractions.Grupo;
using NetCore.Base.Enum;
using System.Reflection;

namespace Infrastructure.SQLServer.Service.Grupo
{
    public class GrupoServiceCommand : IGrupoServiceCommand
    {
        public IUnitOfWork _unitOfWork;

        private GrupoOutResponse _grupoOutResponse;
        private string _userId;

        public GrupoServiceCommand(IUnitOfWork unitOfWork, string userId)
        {
            _unitOfWork = unitOfWork;
            _userId = userId;

            _grupoOutResponse = new GrupoOutResponse();
        }

        public async Task<GrupoOutResponse> CreateGrupo(Domain.Entities.Grupo grupo)
        {
            _grupoOutResponse.AddMensagem($@"Iniciando Metodo {GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

            grupo.InsertedOn = grupo.InsertedOn.ToUniversalTime();
            await _unitOfWork.GrupoRepositoryCommand.Add(grupo);

            var result = await _unitOfWork.Save();

            if (result > 0)
            {
                _grupoOutResponse.SetData(GrupoMapper.FromEntityToDTO(grupo));
                _grupoOutResponse.SetResultado(true);
                _grupoOutResponse.AddMensagem($@"Finalizando Metodo {GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

                return _grupoOutResponse;
            }
            else
            {
                _grupoOutResponse.SetData(null);
                _grupoOutResponse.SetResultado(false);
                _grupoOutResponse.AddMensagem($@"Finalizando Metodo {GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

                return _grupoOutResponse;
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

            var grupo = await _unitOfWork.GrupoRepositoryCommand.GetById(id);
            if (grupo != null)
            {
                var grupoDB = new Domain.Entities.Grupo(grupo.Id, grupo.FirstName, grupo.LastName, grupo.Gender, grupo.Email, EStatus.EXCLUIDO, grupo.InsertedOn, grupo.InsertedBy, DateTime.Now , _userId);
                grupoDB.UpdatedOn = grupo.UpdatedOn.ToUniversalTime();
                grupoDB.Status = EStatus.EXCLUIDO;
                return await UpdateGrupo(grupoDB);
            }
            else
            {
                _grupoOutResponse.SetData(null);
                _grupoOutResponse.SetResultado(false);
                _grupoOutResponse.AddMensagem("Registro Não encontrado!");
                return _grupoOutResponse;
            }
        }

        public async Task<Domain.Entities.Grupo> GetGrupoById(string id)
        {
            var grupo = await _unitOfWork.GrupoRepositoryCommand.GetById(id);

            return grupo;
        }

        public async Task<GrupoOutResponse> UpdateGrupo(Domain.Entities.Grupo grupo)
        {
            if (grupo == null)
            {
                _grupoOutResponse.SetData(null);
                _grupoOutResponse.SetResultado(false);
                return _grupoOutResponse;
            }

            var grupoDB = await _unitOfWork.GrupoRepositoryCommand.GetById(grupo.Id);
            if (grupoDB == null)
            {
                _grupoOutResponse.SetData(null);
                _grupoOutResponse.SetResultado(false);
                return _grupoOutResponse;
            }
            grupoDB = new Domain.Entities.Grupo(grupo.Id, grupo.FirstName, grupo.LastName, grupo.Gender, grupo.Email, grupo.Status, grupo.InsertedOn, grupo.InsertedBy, DateTime.Now, _userId);
            grupoDB.InsertedOn = grupo.InsertedOn.ToUniversalTime();
            grupoDB.UpdatedOn = grupo.UpdatedOn.ToUniversalTime();

            _unitOfWork.GrupoRepositoryCommand.Update(grupoDB);

            var result = await _unitOfWork.Save();

            if (result > 0)
            {
                return await UpdateGrupo(grupoDB);
            }
            else
            {
                _grupoOutResponse.SetData(null);
                _grupoOutResponse.SetResultado(false);
                return _grupoOutResponse;
            }
        }
    }
}
