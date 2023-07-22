using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionPractice {
    public class Utility {

      public void GetMessages() {
            List<string> msg = new List<string>
        {
           "Hi",
           "Hello",
           "Thanks"
        };
        List<string> fruits = new List<string>();
            fruits.Add("Apple");
            fruits.Add("Orange");
            fruits.Add("Bananna");
            msg.AddRange(fruits);

            foreach (string s in msg) {
                Console.WriteLine(s);
            }
        }

      public void GetMarks() {
            IList<int> marks = new List<int>();
            marks.Add(100);
            marks.Add(200);
            foreach(int v in marks) {
                Console.WriteLine(v);
            }
        }//end of GetMarks

        public void GetStudents() {
            IList<Student> students = new List<Student>();
            Student s1 = new Student()
            {
                Id=1,
                Name="Su Su"
            };
            Student s2= new Student()
            {
                Id = 2,
                Name = "Aye Aye"
            };
            Student s3 = new Student()
            {
                Id = 3,
                Name = "Aung Aung"
            };
            students.Add(s1); 
            students.Add(s2); 
            students.Add(s3);
            foreach(Student s in students) {//foreach(DataType <variableName> in <CollectionName>
                Console.WriteLine($"Id :{s.Id}");
                Console.WriteLine($"Name :{s.Name}");
            }
        }//end of getstudent

        public void GetOrderItem() {
            Stack<int> order= new Stack<int>();
            order.Push(100);
            order.Push(200);
            order.Push(700);
            int count= order.Count;
             for(int i=0; i<count; i++) { 
            Console.WriteLine(    order.Pop()); 
            }//700 200 100 
        }

        public void GetWordsHasGreaterThan10() {
            string[] words = new string[]
            {
                "Hi,How are you?","Hi","Hello"
            };

            var newWords = words.Where(x => x.Length > 10);//Linq Lamda Style Syntax
            var okay = (from w in newWords where w.Length > 10 select w);// Query Style
            foreach(var s in newWords) {
                Console.WriteLine(s);
            }
        }
    }
}
