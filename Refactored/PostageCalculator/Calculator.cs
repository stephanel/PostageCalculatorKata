using System;

namespace PostageCalculator
{
    public class Calculator
    {
        public Money Calculate(int weight, int height, int width, int depth, CurrencyKind currencyKind)
        {
            var package = Package.CreateSizedPackage(weight, height, width, depth);

            var postageInBaseCurrency = package.PostageInBaseCurrency();

            var currency = Currency.CreateCurrency(currencyKind);

            return currency.ConvertCurrency(postageInBaseCurrency);
        }        
    }
}