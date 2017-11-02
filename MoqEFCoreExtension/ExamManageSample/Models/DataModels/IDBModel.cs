using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExamManageSample.Models
{
    /// <summary>
    /// EF操作类接口
    /// </summary>
    public interface IDBModel
    {
        DbSet<Answers> Answers { get; set; }
        DbSet<Classes> Classes { get; set; }
        DbSet<ClassTests> ClassTests { get; set; }
        DbSet<Questions> Questions { get; set; }
        DbSet<Scores> Scores { get; set; }
        DbSet<Students> Students { get; set; }
        DbSet<Subjects> Subjects { get; set; }
        DbSet<Tests> Tests { get; set; }
        DbSet<Teachers> Teachers { get; set; }
        int SaveChanges();
    }
}
