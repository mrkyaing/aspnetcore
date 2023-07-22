using CollectionPractice;
using System.Collections;

Console.WriteLine("Hello, World!");

ArrayList items = new ArrayList();
items.Add(1);
items.Add("two");
items.Add(3);
items.Add(4);
items.Add(5.5f);
items.Add("hello");
foreach (var i in items) {
    Console.WriteLine(i);
}
Console.WriteLine("======================");
items.Remove(1);//1
items.RemoveAt(0);//two
items.Insert(0, "me");
foreach (var i in items) {
    Console.WriteLine(i);
}
items.Reverse();
Console.WriteLine("==========after reversing order ===========");
foreach (var i in items) {
    Console.WriteLine(i);
}
ArrayList students= new ArrayList();
students.Add("su su");
students.Add("Min Min");
students.Add("Aye Aye");
students.Sort();
Console.WriteLine("==========after sorting order ASC============");
foreach (var i in students) {
    Console.WriteLine(i);
}
Stack s=new Stack();
s.Push(1);
s.Push(2);
Console.WriteLine(s.Pop());//2
Console.WriteLine(s.Pop());//1
items.AddRange(students);//
foreach (var i in items) {
    Console.WriteLine(i);
}
Console.WriteLine("==========Result from non-generic demo ============");
Utility u=new Utility();
u.GetMessages();
u.GetMarks();
u.GetStudents();
u.GetOrderItem();
Console.WriteLine("==========Result from LINQ demo ============");
TeenageStudentHelper helper = new TeenageStudentHelper();
helper.GetTeenagetStudents();