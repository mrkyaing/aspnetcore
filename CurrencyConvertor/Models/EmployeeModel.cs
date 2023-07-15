namespace CurrencyConvertor.Models {
    public class EmployeeModel {
      //Encapsulated Proepties variales to save the values
       public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //Has-A Relationship Properties (OOD Point Of View)
        public HomeAddress HomeAddress { get; set; }
        //Has-A Relationship Properties (OOD Point Of View)
        public NativeAddress NativeAddress { get; set; }
    }
}
