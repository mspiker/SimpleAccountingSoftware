using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleAccountingSoftware.Models
{
    public class Item
    {
        /// <summary>
        /// A note about validation, it should be set in the Model.  To do validation
        /// we must add System.Component.DataValidation which can be added via NuGet.
        /// 1. Right click on the Models class and go to Manage NuGet Packages...
        /// 2. Search for System.ComponentModel.DataAnnotations 
        /// 3. Select Microsoft.AspNetCore.Mvc.DataAnnotations, then click Install
        /// 4. Start decorating the properties
        /// NOTE: To confirm installation go to Dependencies in the Solution Explorer, 
        /// open Packages and it should be listed.
        /// 
        /// [Required]
        /// [RegularExpression] 
        ///   E-Mail format: @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$
        /// [Range] Range of numbers
        /// [MinLength] Min length of string
        /// [MaxLength] Max length of string
        /// [Compare] Compare e-mail and confirm e-mail
        /// </summary>
        public int Id { get; set; }
        public string Sku { get; set; }
        [Required(ErrorMessage = "Name is Required"),
            MinLength(3, ErrorMessage = "Name should contain at least 3 characters."),
            Display(Name="Product Name")]
        public string Name { get; set; }
        public int OnHand { get; set; }
        public int Ordered { get; set; }
        public int BackOrdered { get; set; }
        public Location? Location { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Retail { get; set; }
        public string PhotoPath { get; set; }
    }
}
