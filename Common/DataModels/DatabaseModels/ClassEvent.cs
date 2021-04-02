using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.DataModels.DatabaseModels
{
    public class ClassEvent
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        [ForeignKey("ScheduleId")]
        public int ScheduleId { get; set; }
    }
}
