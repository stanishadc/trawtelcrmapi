using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Visa")]
    public class Visa
    {
        [Key]
        public Guid VisaId { get; set; }        
        
        [Required(ErrorMessage = "VisaName is required")]
        [StringLength(45, ErrorMessage = "Name can't be longer than 45 characters")]
        public string? VisaName { get; set; }

        [Required(ErrorMessage = "VisaType is required")]
        public string? VisaType { get; set; }

        public string? ProcessingTime { get; set; }
        public string? StayPeriod { get; set; }
        public string? Validity { get; set; }
        public string? Entry { get; set; }
        public string? VisaDocuments { get; set; }
        public string? VisaTerms { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [ForeignKey(nameof(VisaCountry))]
        public Guid VisaCountryId { get; set; }
        public VisaCountry? VisaCountry { get; set; }
    }
    public class VisaDocument
    {
        [Key]
        public Guid VisaDocumentId { get; set; }
        public string? DocumentName { get; set; }
    }
    public class VisaTerm
    {
        [Key]
        public Guid VisaTermsId { get; set; }
        public string? TermsandConditions { get; set; }
    }
    [Table("VisaCountry")]
    public class VisaCountry
    {
        [Key]
        public Guid VisaCountryId { get; set; }
        public string? Name { get; set; }
        public string? ProcessingType { get; set; }
    }
    [Table("VisaPrice")]
    public class VisaPrice
    {
        [Key]
        public Guid VisaPriceId { get; set; }
        public double Price { get; set; }
        public Guid SupplierId { get; set; }
        public Guid VisaId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
