using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models.DTO
{
    public class EmployeeDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string CCCD { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public int? DepartmentId { get; set; }
        public int? ShiftId { get; set; }
        public string UserCreate { get; set; }
        public string UserUpdate { get; set; }
    }
}
