namespace Argent.Api.Domain.Entities.Kyc.KycFiles {
    /// <summary>
    /// Customer Agreements
    /// </summary>
    public class CustomerAgreement : FileAttachment {
        // agreement document reference/title
        public string? Document { get; set; }  
    }

}
