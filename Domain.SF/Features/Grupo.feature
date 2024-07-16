Feature: Grupo

Feature: Grupo
  As a user
  I want to manage Grupos
  So that I can create, read, update, and delete Grupo records

  Background:
    Given the following Grupos exist:
        | Id                                  | NomeDoGrupo | RazaoSocial | NomeFantasia | Cnpj        | InscricaoEstadual | CpfDoAdministrador | PreNomeDoAdministrador | NomeDoMeioDoAdministrador | SobreNomeDoAdministrador | EmailDoAdministrador | InsertedBy | Status   |
        | 11111111-1111-1111-1111-111111111111| Grupo1      | Razao1      | Fantasia1    | 11111111111 | 1234567890        | 11111111111        | John                   | Middle                    | Doe                      | john@example.com      | user1      | ATIVO    |
        | 22222222-2222-2222-2222-222222222222| Grupo2      | Razao2      | Fantasia2    | 22222222222 | 2345678901        | 22222222222        | Jane                   | Middle                    | Doe                      | jane@example.com      | user2      | ATIVO    |

  Scenario: Create a new Grupo
    When I create a new Grupo with the following details:
      | NomeDoGrupo | RazaoSocial | NomeFantasia | Cnpj        | InscricaoEstadual | CpfDoAdministrador | PreNomeDoAdministrador | NomeDoMeioDoAdministrador | SobreNomeDoAdministrador | EmailDoAdministrador   | InsertedBy |
      | NovoGrupo   | NovaRazao   | NovaFantasia | 33333333333 | 3456789012        | 33333333333        | New                   | Middle                   | User                     | newuser@example.com    | user3      |
    Then a new Grupo should be created with the following details:
      | NomeDoGrupo | RazaoSocial | NomeFantasia | Cnpj        | InscricaoEstadual | CpfDoAdministrador | PreNomeDoAdministrador | NomeDoMeioDoAdministrador | SobreNomeDoAdministrador | EmailDoAdministrador   | InsertedBy |

  Scenario: Retrieve an existing Grupo
    When I retrieve the Grupo with Id "11111111-1111-1111-1111-111111111111"
    Then the retrieved Grupo should have the following details:
      | NomeDoGrupo | RazaoSocial | NomeFantasia | Cnpj        | InscricaoEstadual | CpfDoAdministrador | PreNomeDoAdministrador | NomeDoMeioDoAdministrador | SobreNomeDoAdministrador | EmailDoAdministrador   | InsertedBy |
      | Grupo1      | Razao1      | Fantasia1    | 11111111111 | 1234567890        | 11111111111        | John                   | Middle                   | Doe                      | john@example.com       | user1      |

  Scenario: Update an existing Grupo
    Given the Grupo with Id "11111111-1111-1111-1111-111111111111" exists
    When I update the Grupo with the following details:
      | Status | UpdatedBy |
      | INATIVO | user4     |
    Then the Grupo with Id "11111111-1111-1111-1111-111111111111" should have the following status:
      | Status |
      | INATIVO |

  Scenario: Delete an existing Grupo
    Given the Grupo with Id "11111111-1111-1111-1111-111111111111" exists
    When I delete the Grupo with Id "11111111-1111-1111-1111-111111111111" updated by "user5"
    Then the Grupo with Id "11111111-1111-1111-1111-111111111111" should have the following status:
      | Status  |
      | EXCLUIDO |
