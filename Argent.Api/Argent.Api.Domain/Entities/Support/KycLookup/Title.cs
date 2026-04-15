using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Support.KycLookup {

    public class Title : BaseEntity {
        /// <summary>
        /// Mr, Mrs, Dr, Prof, etc. 
        /// </summary>
        public string Name { get; set; } = string.Empty;   
    }
}
