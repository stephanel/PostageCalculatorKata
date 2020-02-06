namespace PostageCalculator
{
    public struct Money
    {
        public Money(CurrencyKind currency, decimal amount)
        {
            Currency = currency;
            Amount = amount;
        }

        public CurrencyKind Currency { get; }
        public decimal Amount { get; }

        public override string ToString()
        {
            return $"Currency: {Currency}, Amount: {Amount}";
        }
    }
}
