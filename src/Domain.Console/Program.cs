using Domain.Builders;
using Domain.Entities;
using NetCore.Base.Enum;

var insertedBy = Guid.NewGuid().ToString();

var grupoBuilder = new GrupoBuilder()
    .ComNomeDoGrupo("Grupo Exemplo")
    .ComRazaoSocial("Razao Social Exemplo")
    .ComNomeFantasia("Nome Fantasia Exemplo")
    .ComCnpj("00.000.000/0001-00")
    .ComInscricaoEstadual("123456789")
    .ComCpfDoAdministrador("45342261104")
    .ComPreNomeDoAdministrador("João")
    .ComNomeDoMeioDoAdministrador("Silva")
    .ComSobreNomeDoAdministrador("Santos")
    .ComEmailDoAdministrador("joao.santos@example.com")
    .ComInsertedBy(insertedBy);

Domain.Entities.Grupo grupo = grupoBuilder.BuildForCreate();

grupo.Update(EStatus.INATIVO, insertedBy);

Console.WriteLine(grupo.Status);

grupo.Delete(insertedBy);

Console.WriteLine(grupo.Status);

foreach (var empresa in grupo.Empresas)
{
    Console.WriteLine(empresa.Id);
    Console.WriteLine(empresa.NomeFantasia);
    Console.WriteLine(empresa.RazaoSocial);
    Console.WriteLine(empresa.CNPJ);
    Console.WriteLine(empresa.InscricaoEstadual);
    Console.WriteLine(empresa.NomeDoAdministrador);
    Console.WriteLine(empresa.EmailDoAdministrador);
    Console.WriteLine(empresa.PessoaAdministradorId);
    Console.WriteLine(empresa.DepartamentoSysMegaId);
    Console.WriteLine(empresa.PessoaUsuarioId);
    foreach (var filial in empresa.Filiais)
    {
        Console.WriteLine(filial.Id);
        Console.WriteLine(filial.NomeDaFilial);
        Console.WriteLine(filial.RazaoSocial);
        Console.WriteLine(filial.CNPJ);
        Console.WriteLine(filial.InscricaoEstadual);
        foreach (var departamento in filial.Departamentos)
        {
            Console.WriteLine(departamento.Id);
            Console.WriteLine(departamento.Name);
            Console.WriteLine(departamento.FilialId);
            foreach (var pessoa in departamento.Pessoas)
            {
                Console.WriteLine(pessoa.Id);
                Console.WriteLine(pessoa.CPF);
                Console.WriteLine(pessoa.PreNome);
                Console.WriteLine(pessoa.NomeDoMeio);
                Console.WriteLine(pessoa.SobreNome);
                foreach (var usuario in pessoa.Usuarios)
                {
                    Console.WriteLine(usuario.Id);
                    Console.WriteLine(usuario.Name);
                    Console.WriteLine(usuario.Email);
                    Console.WriteLine(usuario.Senha);
                    Console.WriteLine(usuario.NivelNumero);
                    Console.WriteLine(usuario.InsertedBy);
                }
            }
        }
    }
}