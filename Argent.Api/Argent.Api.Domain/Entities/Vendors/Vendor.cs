using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Cashflow;
using Argent.Api.Domain.Entities.Accounting.Taxes;
using Argent.Api.Domain.Entities.Support;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Vendors {
    public class Vendor : BaseEntity {
        public string Series { get; set; } = string.Empty;

        [Encryptable("Vendor Name")]
        public string Name { get; set; } = string.Empty;

        [Encryptable("Vendor Alias")]
        public string Alias { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public string LedgerAccount { get; set; } = string.Empty;
        public PaymentPriority Priority { get; set; }
        public VendorType Type { get; set; }

        public long? VendorGroupId { get; set; }
        public virtual VendorGroup? VendorGroup { get; set; }

        public long? DeliverTermsId { get; set; }
        public virtual DeliveryTerms? DeliveryTerm { get; set; }

        public long? DeliveryModeId { get; set; }
        public DeliveryMode? DeliveryMode { get; set; }
        public virtual ICollection<VendorTax> Taxes { get; set; } = [];
        public virtual ICollection<BusinessContact> BusinessContacts { get; set; } = [];
        public virtual ICollection<VendorAddress> VendorAddresses { get; set; } = [];
        public virtual ICollection<VendorBankAccount> BankAccounts { get; set; } = [];
        public virtual ICollection<InvoicingDefault> InvoicingDefaults { get; set; } = [];
        public virtual ICollection<DeliveryDefaults> DeliveryDefaults { get; set; } = [];
        public ICollection<PaymentDefault> VendorPaymentDefaults { get; set; } = [];
        public virtual ICollection<PurchasingDefault> PurchasingDefaults { get; set; } = [];
        public virtual ICollection<PurchaseOrderDefault> PurchaseOrderDefaults { get; set; } = [];
        public virtual ICollection<VendorReference> RefereceValues { get; set; } = [];
        public virtual ICollection<Card> Cards { get; set; } = [];
    }

}
