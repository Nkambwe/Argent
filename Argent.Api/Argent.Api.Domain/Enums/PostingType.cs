namespace Argent.Api.Domain.Enums {
    public enum PostingType {
        /// <summary>
        /// Can only be debited 
        /// </summary>
        Debit = 1,
        /// <summary>
        /// Can only be credited
        /// </summary>
        Credit = 2,
        /// <summary>
        /// Can be debited or credited 
        /// </summary>
        Both = 3,
        /// <summary>
        /// Only system-generated postings allowed 
        /// </summary>
        System = 4    
    }
}
