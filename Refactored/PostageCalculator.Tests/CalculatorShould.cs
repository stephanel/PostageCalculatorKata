using Xunit;

namespace PostageCalculator.Tests
{
    public class CalculatorShould
    {
        private const decimal SmallPackagePrice = 120m;
        private const int MaximumSmallWeight = 60;
        private const int MaximumSmallHeight = 229;
        private const int MaximumSmallWidth = 162;
        private const int MaximumSmallDepth = 25;
        private const int MaximumMediumWeight = 500;
        private const int MaximumMediumHeight = 324;
        private const int MaximumMediumWidth = 229;
        private const int MaximumMediumDepth = 100;
        private const double GbpToEur = 1.25;
        private const double GbpToChf = 1.36;
        private const decimal Commission = 200m;

        private readonly Calculator _calculator;

        public CalculatorShould()
        {
            _calculator = new Calculator();
        }

        [Theory]
        [InlineData(1, 1, 1, 1)]
        [InlineData(MaximumSmallWeight, 1, 1, 1)]
        [InlineData(1, MaximumSmallHeight, 1, 1)]
        [InlineData(1, 1, MaximumSmallWidth, 1)]
        [InlineData(1, 1, 1, MaximumSmallDepth)]
        public void Charge_a_flat_rate_for_a_small_package(int weight, int height, int width, int depth)
        {
            Assert.Equal(new Money(Currency.Gbp, SmallPackagePrice),
                _calculator.Calculate(weight, height, width, depth, Currency.Gbp));
        }

        [Theory]
        [InlineData(MaximumSmallWeight + 1, 1, 1, 1)]
        [InlineData(MaximumMediumWeight, 1, 1, 1)]
        [InlineData(1, MaximumSmallHeight + 1, 1, 1)]
        [InlineData(1, MaximumMediumHeight, 1, 1)]
        [InlineData(1, 1, MaximumSmallWidth + 1, 1)]
        [InlineData(1, 1, MaximumMediumWidth, 1)]
        [InlineData(1, 1, 1, MaximumSmallDepth + 1)]
        [InlineData(1, 1, 1, MaximumMediumDepth)]
        public void Price_a_medium_package_by_weight(int weight, int height, int width, int depth)
        {
            Assert.Equal(new Money(Currency.Gbp, weight * 4),
                _calculator.Calculate(weight, height, width, depth, Currency.Gbp));
        }

        [Theory]
        [InlineData(MaximumMediumWeight + 1, 1, 1, 1)]
        [InlineData(1, MaximumMediumHeight + 1, 1, 1)]
        [InlineData(1, 1, MaximumMediumWidth + 1, 1)]
        [InlineData(1, 1, 1, MaximumMediumDepth + 1)]
        public void Price_a_large_heavy_package_by_weight(int weight, int height, int width, int depth)
        {
            Assert.Equal(new Money(Currency.Gbp, weight * 6),
                _calculator.Calculate(weight, height, width, depth, Currency.Gbp));
        }

        [Theory]
        [InlineData(1, 1001, 1, 1)]
        [InlineData(1, 1, 1001, 1)]
        [InlineData(1, 1, 1, 1001)]
        public void Price_a_large_light_package_by_volume(int weight, int height, int width, int depth)
        {
            Assert.Equal(new Money(Currency.Gbp, (height * width * depth / 1000m) * 6),
                _calculator.Calculate(weight, height, width, depth, Currency.Gbp));
        }

        [Theory]
        [InlineData(Currency.Eur, GbpToEur)]
        [InlineData(Currency.Chf, GbpToChf)]
        public void Add_commission_for_currency_other_than_gbp(Currency currency, decimal exchangeRate)
        {
            Assert.Equal(new Money(currency, (SmallPackagePrice + Commission) * exchangeRate),
                _calculator.Calculate(20, 20, 20, 20, currency));
        }
    }
}
