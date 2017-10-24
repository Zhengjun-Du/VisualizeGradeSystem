using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace VisualizeGradeSystem.Models.Score
{
    public class ScoreContext : DbContext
    {
       public ScoreContext()
            : base("name=Score-Context")
        { }

       public DbSet<Score> ScoreList { get; set; }

    }
}