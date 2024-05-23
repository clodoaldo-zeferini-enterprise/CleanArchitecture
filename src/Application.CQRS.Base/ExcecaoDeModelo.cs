namespace Application.CQRS.Base
{
    public class ExcecaoDeModelo : ArgumentException
    {
        public List<string> MensagensDeErro { get; private set; }

        public ExcecaoDeModelo(List<string> mensagensDeErros)
        {
            MensagensDeErro = mensagensDeErros;
        }
    }
}
