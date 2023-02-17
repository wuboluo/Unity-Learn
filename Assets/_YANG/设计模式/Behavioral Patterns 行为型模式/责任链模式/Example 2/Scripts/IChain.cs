namespace Yang.DesignPattern.ChainOfResponsibility.Example2
{
    public interface IChain
    {
        void SetNextChain(IChain nextChain);
        void Calculate(Numbers numbers);
    }
}