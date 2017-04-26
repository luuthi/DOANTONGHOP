using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.ViewModel
{
    public class BaseViewModel
    {
        public BaseViewModel()
        {
            Id = Guid.NewGuid();
        }
        [Required]
        public Guid Id { get; set; }
    }
}
