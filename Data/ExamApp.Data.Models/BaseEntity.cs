using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Data.Models
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
    }
}