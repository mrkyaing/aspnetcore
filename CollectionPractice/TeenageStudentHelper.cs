using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionPractice {
    public class TeenageStudentHelper {

        public void GetTeenagetStudents() {
            Student[] students= {
                new Student() {Id=1,Name="SU SU",Age=20 },
                new Student {Id=2,Name="Aye Aye",Age=17 }, 
                new Student() {Id=3,Name="Min Aung",Age=20 }, 
                new Student() { Id=4,Name="Ko Ko",Age=18}, 
                new Student { Id=5,Name="Jone",Age=33}, 
                new Student() {Id=6,Name="Angel",Age=22 } ,
                new Student() {Id=7,Name="Junny",Age=23 },
                new Student {Id=8,Name="Jame",Age=13 },
                new Student() {Id=9,Name="Smith",Age=21 },
                new Student() { Id=10,Name="Arkar",Age=30},
                new Student { Id=11,Name="Bo Bo",Age=15},
                new Student() {Id=12,Name="Aung Aung ",Age=23 }
            };
           var  teenageStudents=students.Where(x=>x.Age>12 && x.Age<20).ToList();
            Student s1 = students.Where(x => x.Id == 7).SingleOrDefault();
            var  s2 = students.Where(x => x.Age > 15).SingleOrDefault();
            Console.WriteLine(s1.Name);//Junny
            foreach(var s in teenageStudents) {
                Console.WriteLine(s.Name);
            }
        }
    } 
}
