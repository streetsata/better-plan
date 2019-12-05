using System;
using System.ComponentModel.DataAnnotations;

namespace PostingAPI.Models
{
	public class PostModel
	{
		[Key]
		public int Id { get; set; }
		public string PostId { get; set; }
		public string TextOfPost { get; set; }
		public string TextOfLink { get; set; }
		public DateTime DateOfPosting { get; set; }
		public DateTime ModificationDate { get; set; }
		public DateTime DateOfDeletion { get; set; }
		public bool IsDelete { get; set; }
	}
}
