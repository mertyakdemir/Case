using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseApp.Entities
{
    public class ContentModel
    {
        [Key]
        public int Id { get; set; }
        public string Translated { get; set; }
        public string Text { get; set; }
        public string Translation { get; set; }
        public DateTime Date { get; set; }
    }
}
