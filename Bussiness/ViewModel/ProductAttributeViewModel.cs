﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.ViewModel
{
    public class ProductAttributeViewModel :BaseViewModel
    {
        [DisplayName("Tên thuộc tính")]
        [Required(ErrorMessage = "Nhập tên thuộc tính")]
        public string AttributeName { get; set; }

        [DisplayName("Loại thuộc tính")]
        public string AttributeType { get; set; }
    }
}
