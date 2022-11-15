using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class VisaDTO
    {
        public Guid VisaId { get; set; }
        public Guid VisaCountryId { get; set; }
        public string? VisaName { get; set; }
        public string? VisaType { get; set; }
        public string? ProcessingTime { get; set; }
        public string? StayPeriod { get; set; }
        public string? Validity { get; set; }
        public string? Entry { get; set; }
        public List<VisaDocument>? VisaDocuments { get; set; }
        public List<VisaTerm>? VisaTerms { get; set; }
    }
    public class VisaRoot
    {
        public List<VisaDocument>? VisaDocuments { get; set; }
        public List<VisaTerm>? VisaTerms { get; set; }
    }
    public class VisaCountryDTO
    {
        public Guid VisaCountryId { get; set; }
        public string? Name { get; set; }
        public string? ProcessingType { get; set; }
    }
    public class VisaPriceDTO
    {
        public Guid VisaPriceId { get; set; }
        public double Price { get; set; }
        public Guid SupplierId { get; set; }
        public Guid VisaId { get; set; }
    }
}
