using Microsoft.EntityFrameworkCore;
using StudentCRUDMVC.Models;

namespace StudentCRUDMVC.DAO {
    public class StudentDbContext:DbContext {
        public StudentDbContext(DbContextOptions<StudentDbContext> options):base(options){
        }

        public DbSet<Student> Students { get; set; }//define your student entity 

    }
}
