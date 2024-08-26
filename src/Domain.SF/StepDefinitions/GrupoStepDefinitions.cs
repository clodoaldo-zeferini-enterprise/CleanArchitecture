using System;
using TechTalk.SpecFlow;

namespace Domain.SF.StepDefinitions
{
    [Binding]
    public class GrupoStepDefinitions
    {
        [Given(@"the following Grupos exist:")]
        public void GivenTheFollowingGruposExist(Table table)
        {
            throw new PendingStepException();
        }

        [When(@"I create a new Grupo with the following details:")]
        public void WhenICreateANewGrupoWithTheFollowingDetails(Table table)
        {
            throw new PendingStepException();
        }

        [Then(@"a new Grupo should be created with the following details:")]
        public void ThenANewGrupoShouldBeCreatedWithTheFollowingDetails(Table table)
        {
            throw new PendingStepException();
        }

        [When(@"I retrieve the Grupo with Id ""([^""]*)""")]
        public void WhenIRetrieveTheGrupoWithId(string p0)
        {
            throw new PendingStepException();
        }

        [Then(@"the retrieved Grupo should have the following details:")]
        public void ThenTheRetrievedGrupoShouldHaveTheFollowingDetails(Table table)
        {
            throw new PendingStepException();
        }

        [Given(@"the Grupo with Id ""([^""]*)"" exists")]
        public void GivenTheGrupoWithIdExists(string p0)
        {
            throw new PendingStepException();
        }

        [When(@"I update the Grupo with the following details:")]
        public void WhenIUpdateTheGrupoWithTheFollowingDetails(Table table)
        {
            throw new PendingStepException();
        }

        [Then(@"the Grupo with Id ""([^""]*)"" should have the following status:")]
        public void ThenTheGrupoWithIdShouldHaveTheFollowingStatus(string p0, Table table)
        {
            throw new PendingStepException();
        }

        [When(@"I delete the Grupo with Id ""([^""]*)"" updated by ""([^""]*)""")]
        public void WhenIDeleteTheGrupoWithIdUpdatedBy(string p0, string p1)
        {
            throw new PendingStepException();
        }
    }
}
