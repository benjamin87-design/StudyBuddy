﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Models
{
	public class FlashCardModel
	{
		public int Id { get; set; }
		public string Question { get; set; }
		public string Answer { get; set; }
		public string CategoryName { get; set; }
		public int CategoryId { get; set; }
	}
}
