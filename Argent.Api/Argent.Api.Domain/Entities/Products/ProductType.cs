using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Products {

    public class ProductType : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string Series { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<TimedepositProduct> TimedepositProducts { get; set; } = [];
        public virtual ICollection<InsuranceProduct> InsuranceProducts { get; set; } = [];
        public virtual ICollection<ShareProduct> ShareProducts { get; set; } = [];
        public virtual ICollection<SavingProduct> SavingProducts { get; set; } = [];
        public virtual ICollection<LoanProduct> LoanProducts { get; set; } = [];
        /// <summary>
        /// Override product ToString() method
        /// </summary>
        /// <returns>Returns string representation of product class</returns>
        public override string ToString() => $"{Code}-{Name}";

        /// <summary>
        /// Override product GetHashCode() method
        /// </summary>
        /// <returns>Returns hashcode representation of product class's ToString() class</returns>
        public override int GetHashCode() => ToString().GetHashCode() ^ 3;
    }
}
