using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Models
{
	public class InfoCardModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public int CardNumber { get; set; }
		public int NumberOfCompletition { get; set; }
		public bool StarRating { get; set; }
	}
}
