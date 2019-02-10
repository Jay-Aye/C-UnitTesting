using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;

namespace MyClassesTest
{
  [TestClass]
  public class PersonManagerTest
  {
    [TestMethod]
    public void CreatePerson_OfTypeEmployeeTest()
    {
      PersonManager mgr = new PersonManager();
      Person per;

      per = mgr.CreatePerson("Paul", "Sheriff",
               false);

      Assert.IsInstanceOfType(per,
                      typeof(Employee));
    }

    [TestMethod]
    [Owner("JohnK")]
    public void GetEmployeesTest()
    {
      PersonManager mgr = new PersonManager();
      List<Person> employees;

      employees = mgr.GetEmployees();

      CollectionAssert.AllItemsAreInstancesOfType(employees, typeof(Employee));
    }

    [TestMethod]
    [Owner("PaulS")]
    public void IsCollectionOfTypeSupervisorsAndEmployeeTest()
    {
      PersonManager mgr = new PersonManager();
      List<Person> peopleActual = new List<Person>();

      peopleActual = mgr.GetSupervisorsAndEmployees();

      CollectionAssert.AllItemsAreNotNull(peopleActual);
    }
    [TestMethod]
    [Owner("JohnK")]
    public void DoEmployeesExistTest()
    {
      Supervisor super = new Supervisor();

      super.Employees = new List<Employee>();
      super.Employees.Add(new Employee()
      {
        FirstName = "Jim",
        LastName = "Ruhl"
      });

      Assert.IsTrue(super.Employees.Count > 0);
    }




  }
}
