using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models.DTO
{
    public class DepartmentDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? LocationId { get; set; }
        public bool Status { get; set; }
    }
}
