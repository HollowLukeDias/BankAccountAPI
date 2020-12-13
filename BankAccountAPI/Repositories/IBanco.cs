namespace BankAccountAPI.Helpers
{
    public interface IBanco
    {
        void TransacaoDeposito(Conta conta, decimal quantidade);
        void TransacaoSaque(Conta conta, decimal quantidade);
        void TransacaoTransferencia(Conta conta, Conta contaDestino, decimal quantidade);
    }
}
