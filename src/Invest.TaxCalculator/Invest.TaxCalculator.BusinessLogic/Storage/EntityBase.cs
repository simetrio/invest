namespace Invest.TaxCalculator.BusinessLogic.Storage
{
    public abstract class EntityBase
    {
        protected abstract object[] GetKeys();

        public override bool Equals(object obj)
        {
            return ToString().Equals(obj.ToString());
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return string.Join(";", GetKeys());
        }
    }
}