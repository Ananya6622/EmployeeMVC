using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLayer
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        [Required(ErrorMessage ="{0} input should be given")]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2,}$",ErrorMessage ="First name is not valid")]
        public string Name { get; set; }
        [Required(ErrorMessage = "{0} input should be given")]
        public string ProfileImage { get; set; }
        [Required(ErrorMessage = "{0} input should be given")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "{0} input should be given")]
        public string Department { get; set; }
        [Required(ErrorMessage = "{0} input should be given")]
        public decimal Salary { get; set; }
        [Required(ErrorMessage = "{0} input should be given")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "{0} input should be given")]
        public string Notes { get; set; }

    }
}
