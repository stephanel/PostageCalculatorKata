using System;
using System.Net.NetworkInformation;

namespace PostageCalculator
{
    public abstract class Package
    {
        public abstract decimal PostageInBaseCurrency();

        public static Package CreateSizedPackage(int weight, int height, int width, int depth)
        {
            if (IsSmallPackage(weight, height, width, depth))
            {
                return new SmallPackage();
            }

            if (IsMediumPackage(weight, height, width, depth))
            {
                return new MediumPackage(weight);
            }

            return new LargePackage(weight, height, width, depth);
        }

        private static bool IsMediumPackage(int weight, int height, int width, int depth)
        {
            return weight <= 500 && height <= 324 && width <= 229 && depth <= 100;
        }

        private static bool IsSmallPackage(int weight, int height, int width, int depth)
        {
            return weight <= 60 && height <= 229 && width <= 162 && depth <= 25;
        }
    }

    public class SmallPackage : Package
    {
        public SmallPackage()
        {
        }

        public override decimal PostageInBaseCurrency()
        {
            return 120m;
        }
    }

    public class MediumPackage : Package
    {
        protected readonly int _weight;

        public MediumPackage(int weight)
        {
            _weight = weight;
        }

        public override decimal PostageInBaseCurrency()
        {
            return _weight * 4;
        }
    }

    public class LargePackage: Package
    {
        protected readonly int _weight;
        protected readonly int _height;
        protected readonly int _width;
        protected readonly int _depth;

        public LargePackage(int weight, int height, int width, int depth)
        {
            _weight = weight;
            _height = height;
            _width = width;
            _depth = depth;
        }

        public override decimal PostageInBaseCurrency()
        {
            return Math.Max(_weight, _height * _width * _depth / 1000m) * 6;
        }
    }    
}
