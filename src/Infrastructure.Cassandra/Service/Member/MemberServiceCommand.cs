using Application.DTO.Response;
using Cassandra;
using NetCore.Base.Enum;
using Infrastructure.Base.Abstractions.Grupo;
using Infrastructure.Base.Enums;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.Json;


namespace Infrastructure.Cassandra.Service.Member
{
    public class MemberServiceCommand : IGrupoServiceCommand
    {
        private readonly ISession _session;

        private GrupoOutResponse _memberOutResponse;
        private string _userId;

        public MemberServiceCommand(ISession session)
        {
            _memberOutResponse = new GrupoOutResponse();
            _userId = Guid.NewGuid().ToString();

            _session = session ?? throw new ArgumentNullException(nameof(session));
        }

        private string GetFields()
        {
            // Get the type handle of a specified class.
            Type myType = typeof(Domain.Entities.Member);

            string fields = String.Empty;

            // Get the fields of the specified class.
            PropertyInfo[] myProperties = myType.GetProperties();

            string[] properties = new string[myProperties.Length];

            for (int i = 0; i < (myProperties.Length); i++)
            {
                fields += $@" {myProperties[i].Name} {(i < (myProperties.Length - 1) ? " , " : "")}";
            }

            fields = $@"{fields}";

            return fields;
        }

        private async Task<SimpleStatement> CreateCommandSQL (ETypeCpmmand typeCpmmand, Domain.Entities.Member member)
        {
            if (typeCpmmand == ETypeCpmmand.INSERT)
            {
                return new SimpleStatement(
                    $@"INSERT INTO mykeyspace.members ( {GetFields()}) 
                   VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", 
                      member.Id, 
                      member.Name,
                      member.FirstName, 
                      member.LastName,
                      member.Email,
                      member.Status,
                      member.InsertedOn,
                      member.InsertedBy,
                      member.UpdatedOn,
                      member.UpdatedBy
                );
            }
            else
            {
                return new SimpleStatement(
                    $@"UPDATE
                            mykeyspace.members ( {GetFields()}) 
                       SET 
                         name       = ?,
                         firstname  = ?,
                         lastname   = ?,
                         email      = ?,
                         status     = ?,
                         insertedon = ?,
                         insertedby = ?,
                         updatedon  = ?,
                         updatedby  = ?

                         WHERE id   = ?
                    ",                      
                      member.Name,
                      member.FirstName,
                      member.LastName,
                      member.Email,
                      member.Status,
                      member.InsertedOn,
                      member.InsertedBy,
                      member.UpdatedOn,
                      member.UpdatedBy, 
                      member.Id
                );
            }
        }

        public async Task<GrupoOutResponse> CreateGrupo(Domain.Entities.Member member)
        {
            /*Verifico se o Parâmetro é nulo*/
            if (member == null)
            {
                _memberOutResponse.SetResultado(false);
                _memberOutResponse.SetMensagem("O objeto recebido é nulo.");
                return _memberOutResponse;
            }

            await _session.ExecuteAsync(await CreateCommandSQL(ETypeCpmmand.INSERT, member));

            _memberOutResponse.SetResultado(true);
            _memberOutResponse.SetMensagem("Membro cadastrado com Sucesso!");
            _memberOutResponse.SetData(MemberMapper.FromEntityToDTO(member));
            return _memberOutResponse;   
        }        

        public async Task<GrupoOutResponse> UpdateGrupo(Domain.Entities.Member member)
        {
            /*Verifico se o Parâmetro é nulo*/
            if (member == null)
            {
                _memberOutResponse.SetResultado(false);
                _memberOutResponse.SetMensagem("O objeto recebido é nulo.");
                return _memberOutResponse;
            }

            var memberCassandra = await GetMemberById(member.Id);
            if (memberCassandra == null)
            {
                _memberOutResponse.SetResultado(true);
                _memberOutResponse.SetMensagem("Registro não encontrado!");
                return _memberOutResponse;
            }
            else
            {
                await _session.ExecuteAsync(await CreateCommandSQL(ETypeCpmmand.UPDATE, member));

                _memberOutResponse.SetData(MemberMapper.FromEntityToDTO(member));
                _memberOutResponse.SetResultado(true);
                _memberOutResponse.SetMensagem("Member Atualizado com Sucesso!");

                return _memberOutResponse;
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
                _memberOutResponse.SetResultado(false);
                _memberOutResponse.SetMensagem("Id inválido.");
                return _memberOutResponse;
            }

            var member = await GetGrupoById(id);

            if (member == null)
            {
                _memberOutResponse.SetResultado(true);
                _memberOutResponse.SetMensagem("Registro não encontrado!");
                return _memberOutResponse;
            }

            member.Status = EStatus.EXCLUIDO;
            member.UpdatedBy = _userId;
            member.UpdatedOn = DateTime.Now;

            member.Status = Domain.Enums.EStatus.EXCLUIDO;
            member.UpdatedBy = _userId;
            member.UpdatedOn = DateTime.Now;

            await _session.ExecuteAsync(await CreateCommandSQL(ETypeCpmmand.UPDATE, member));

            _memberOutResponse.SetData(MemberMapper.FromEntityToDTO(member));
            _memberOutResponse.SetResultado(true);
            _memberOutResponse.SetMensagem("Member Excluído com Sucesso!");

            return _memberOutResponse;
        }

        public async Task<Domain.Entities.Member> GetGrupoById(string id)
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
                _memberOutResponse.SetResultado(false);
                _memberOutResponse.SetMensagem("Id inválido.");
                return null;
            }

            var rs = await _session.ExecuteAsync(new SimpleStatement($@"SELECT {GetFields()} FROM mykeyspace.members WHERE id = ?", id));
            var row = rs.FirstOrDefault();
            if (row == null)
            {
                _memberOutResponse.SetMensagem("Registro não encontrado.");
                _memberOutResponse.SetResultado(true);
                return null;
            }

            return new Domain.Entities.Member(
                row.GetValue<string>("id"),
                row.GetValue<string>("firstname"),
                row.GetValue<string>("lastname"),
                (Domain.Enums.EGenero)row.GetValue<int>("gender"),
                row.GetValue<string>("email"),
                (Domain.Enums.EStatus)row.GetValue<int>("status"),
                row.GetValue<DateTime>("insertedon"),
                row.GetValue<string>("insertedby"),
                row.GetValue<DateTime>("updatedon"),
                row.GetValue<string>("updatedby")
            );
        }
    }
}
