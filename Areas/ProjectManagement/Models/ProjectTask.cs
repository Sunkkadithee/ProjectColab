using System;
using System.ComponentModel.DataAnnotations;


namespace COMP2139_labs.Areas.ProjectManagement.Models
{
	public class ProjectTask
	{
		[Key]
		public int ProjectTaskId { get; set; }

		[Required]
		public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }

		//forreign key for the Project
        public int ProjectId { get; set; }

		//Nevigetion Property
		//This property allows for easy access to the related Project entity from the Task Entity
		public Project? Project { get; set; }

    }

}