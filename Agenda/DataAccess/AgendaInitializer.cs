using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Agenda.Models;

namespace Agenda.DataAccess
{
    public class AgendaInitializer : DropCreateDatabaseIfModelChanges<AgendaContext>
    {
        protected override void Seed(AgendaContext context)
        {
            var Persons = new List<Person>
            {
                new Person { PersonID = 1, FirstName ="Antonio",LastName="Acosta Murillo",DOB=DateTime.Parse("1988-01-05") },
                new Person { PersonID= 2, FirstName ="Jorge",LastName="Navarro Murillo",DOB=DateTime.Parse("1979-02-01") },
                //new Person { PersonID = 3, FirstName="Manuel",LastName="Castro Hernandez",DOB=DateTime.Parse("1990-09-15") },
                new Person { PersonID = 4, FirstName="Grisell",LastName="Lopez Guevara",DOB=DateTime.Parse("1991-11-25") },
                //new Person { PersonID = 5, FirstName="Elvia",LastName="Santana Gil",DOB=DateTime.Parse("1980-08-29") },
                //new Person { PersonID = 6, FirstName="Pedro",LastName="Moreno Infante",DOB=DateTime.Parse("1987-04-04") },
                //new Person { PersonID = 7, FirstName="Fernanda",LastName="Gil Guevara",DOB=DateTime.Parse("1978-05-10") }
            };
            Persons.ForEach(p => context.Persons.Add(p));
            context.SaveChanges();

            var ContactInfo = new List<PersonInfo>
            {
                new PersonInfo {PersonInfoID = 1, PersonID = 1,  Phone = "6671318545", Mail = "antonio@aacosta.com.mx", Website = @"http://aacosta.com.mx/" },
                new PersonInfo {PersonInfoID = 2, PersonID = 1,  Phone = "5543712924", Mail = "eltony162@gmail.com", Website = @"http://aacosta.com.mx/" },
                new PersonInfo {PersonInfoID = 3, PersonID = 2,  Phone = "0000000001", Mail = "Jorge@gmail.com", Website = @"" },
                new PersonInfo {PersonInfoID = 3, PersonID = 3,  Phone = "0000000002", Mail = "Grisell@gmail.com", Website = @"" }
            };
            ContactInfo.ForEach(c => context.ContactInfo.Add(c));
            context.SaveChanges();

            var Adresses = new List<Address>
            {
                new Address { PersonID = 1, AddressID = 1, Direction="Bartolache 1731 int 14", Country=@"México", State="D.F.", ZIP="03100" },
                new Address { PersonID = 1, AddressID = 2, Direction="Lomas del bulevard 3132", Country=@"México", State="Sinaloa", ZIP="80160" },
                new Address { PersonID = 2, AddressID = 3, Direction="direction 1", Country=@"México", State="DF", ZIP="01300" },
                new Address { PersonID = 3, AddressID = 4, Direction="direction 2", Country=@"México", State="DF", ZIP="02301" }

            };
            Adresses.ForEach(a => context.Addresses.Add(a));
            context.SaveChanges();
        }
    }
}