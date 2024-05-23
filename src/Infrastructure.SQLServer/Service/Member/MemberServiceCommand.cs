using Application.DTO.Response;
using Application.DTO.Response.Member;
using Domain.Enums;
using Infrastructure.Base;
using Infrastructure.Base.Abstractions.Member;
using System.Reflection;

namespace Infrastructure.SQLServer.Service.Member
{
    public class MemberServiceCommand : IMemberServiceCommand
    {
        public IUnitOfWork _unitOfWork;

        private MemberOutResponse _memberOutResponse;
        private string _userId;

        public MemberServiceCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userId = Guid.NewGuid().ToString();

            _memberOutResponse = new MemberOutResponse();
        }

        async public Task<MemberOutResponse> CreateMember(Domain.Entities.Member member)
        {
            _memberOutResponse.AddMensagem($@"Iniciando Metodo {GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

            await _unitOfWork.MemberRepositoryCommand.Add(member);

            var result = await _unitOfWork.Save();

            if (result > 0)
            {
                return await GetMemberById(member.Id);
            }
            else
            {
                _memberOutResponse.SetData(null);
                _memberOutResponse.SetResultado(false);
                _memberOutResponse.AddMensagem($@"Finalizando Metodo {GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

                return _memberOutResponse;
            }
        }
        
        async public Task<MemberOutResponse> DeleteMember(string id)
        {
            var member = await _unitOfWork.MemberRepositoryCommand.GetById(id);
            if (member != null)
            {
                var memberDB = new Domain.Entities.Member(member.Id, member.FirstName, member.LastName, member.Gender, member.Email, EStatus.EXCLUIDO, member.InsertedOn, member.InsertedBy, DateTime.Now , _userId);
                return await UpdateMember(memberDB);
            }
            else
            {
                _memberOutResponse.SetData(null);
                _memberOutResponse.SetResultado(false);
                _memberOutResponse.AddMensagem("Registro Não encontrado!");
                return _memberOutResponse;
            }
        }

        async public Task<MemberOutResponse> GetMemberById(string id)
        {
            var member = await _unitOfWork.MemberRepositoryCommand.GetById(id);
            if (member != null)
            {
                _memberOutResponse.SetResultado(true);
                _memberOutResponse.SetData(MemberMapper.FromEntityToDTO(member));
                return _memberOutResponse;
            }

            return _memberOutResponse;
        }

        async public Task<MemberOutResponse> UpdateMember(Domain.Entities.Member member)
        {
            if (member == null)
            {
                _memberOutResponse.SetData(null);
                _memberOutResponse.SetResultado(false);
                return _memberOutResponse;
            }

            var memberDB = await _unitOfWork.MemberRepositoryCommand.GetById(member.Id);
            if (memberDB != null)
            {
                memberDB = new Domain.Entities.Member(member.Id, member.FirstName, member.LastName, member.Gender, member.Email, member.Status, member.InsertedOn, member.InsertedBy, DateTime.Now, _userId);

                _unitOfWork.MemberRepositoryCommand.Update(memberDB);

                var result = await _unitOfWork.Save();

                if (result > 0)
                {
                    return await GetMemberById(member.Id);
                }
                else
                {
                    _memberOutResponse.SetData(null);
                    _memberOutResponse.SetResultado(false);
                    return _memberOutResponse;
                }
            }
            else
            {
                _memberOutResponse.SetData(null);
                _memberOutResponse.SetResultado(false);
                _memberOutResponse.AddMensagem("Registro Não encontrado!");
                return _memberOutResponse;
            }
        }
    }
}
