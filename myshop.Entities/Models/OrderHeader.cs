using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.Entities.Models
{
	public class OrderHeader
	{
		[Key]
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        [ValidateNever]
		public ApplicationUser applicationUser { get; set; } // Forgein Key to User Table (1 --> M)
        public DateTime orderDate { get; set; }
        public DateTime shippingDate { get; set; }
        public decimal totalPrice { get; set; }
        public string? orderStatus { get; set; }
        public string? paymentStatus { get; set; }
        public string? TrackingNumber { get; set; }
        public string? Carrier { get; set; }
        public DateTime paymentDate { get; set; }

        // Stripe
		public string? sessionId { get; set; }
		public string? paymentIntentId { get; set; }

		// Data For User
		public string name { get; set; }
		public string address { get; set; }
		public string city { get; set; }
		public string phone { get; set; }

	}
}
