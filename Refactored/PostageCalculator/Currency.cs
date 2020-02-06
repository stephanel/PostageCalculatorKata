using System;
using System.Collections.Generic;

namespace PostageCalculator
{
    public enum CurrencyKind
    {
        Gbp,
        Eur,
        Chf
    }

    public abstract class Currency 
    {
        private static readonly Dictionary<CurrencyKind, Currency> _currencies = new Dictionary<CurrencyKind, Currency>()
        {
            { CurrencyKind.Chf, new CurrencyChf() },
            { CurrencyKind.Eur, new CurrencyEur() },
            { CurrencyKind.Gbp, new CurrencyGbp() },
        };

        protected abstract CurrencyKind GetCurrencyKind();
        protected abstract decimal GetConvertedAmount(decimal amountInBaseCurrency);

        public Money ConvertCurrency(decimal amountInBaseCurrency)
        {
            var currencyKind = GetCurrencyKind();
            var convertedAmount = GetConvertedAmount(amountInBaseCurrency);

            return new Money(currencyKind, convertedAmount);
        }

        public static Currency CreateCurrency(CurrencyKind currencyKind)
        {
            if(!_currencies.ContainsKey(currencyKind))
            {
                throw new Exception("Currency not supported");
            }

            return _currencies[currencyKind];
        }
    }

    public class CurrencyGbp : Currency
    {
        protected override decimal GetConvertedAmount(decimal amountInBaseCurrency)
        {
            return amountInBaseCurrency;
        }

        protected override CurrencyKind GetCurrencyKind()
        {
            return CurrencyKind.Gbp;
        }
    }

    public class CurrencyEur : Currency
    {
        protected override decimal GetConvertedAmount(decimal amountInBaseCurrency)
        {
            return (amountInBaseCurrency + 200) * 1.25m;
        }

        protected override CurrencyKind GetCurrencyKind()
        {
            return CurrencyKind.Eur;
        }
    }

    public class CurrencyChf : Currency
    {
        protected override decimal GetConvertedAmount(decimal amountInBaseCurrency)
        {
            return (amountInBaseCurrency + 200) * 1.36m;
        }

        protected override CurrencyKind GetCurrencyKind()
        {
            return CurrencyKind.Chf;
        }
    }
}