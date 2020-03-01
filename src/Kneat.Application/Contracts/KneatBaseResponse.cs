namespace Kneat.Application.Contracts
{
    public abstract class KneatBaseResponse
    {

        public bool Success { get; private set; } = true;

        public string ErrorMessage { get; private set; }

        public void SetError(string message)
        {
            this.Success = false;
            this.ErrorMessage = message;

        }


    }
}
