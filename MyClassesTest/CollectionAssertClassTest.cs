using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;

namespace MyClassesTest
{
  [TestClass]
  public class CollectionAssertClassTest
  {
    /// <summary>
    /// NOTE: We want this test to fail to illustrate Equality means collections reference the exact same objects
    /// </summary>
    [TestMethod]
    [Owner("JohnK")]
    public void AreCollectionsEqualFailsBecauseNoComparerTest() {
      PersonManager mgr = new PersonManager();
      List<Person> peopleExpected = new List<Person>();
      List<Person> peopleActual = new List<Person>();

      peopleExpected.Add(new Person() { FirstName = "Paul", LastName = "Sheriff" });
      peopleExpected.Add(new Person() { FirstName = "John", LastName = "Kuhn" });
      peopleExpected.Add(new Person() { FirstName = "Jim", LastName = "Ruhl" });

      peopleActual = mgr.GetPeople();

      // NOTE: By default it compares the person objects to see if they are Equal (they refer to the same object)
      CollectionAssert.AreEqual(peopleExpected, peopleActual);
    }

    [TestMethod]
    [Owner("JohnK")]
    public void AreCollectionsEqualTest() {
      PersonManager mgr = new PersonManager();
      List<Person> peopleExpected = new List<Person>();
      List<Person> peopleActual = new List<Person>();

      peopleActual = mgr.GetPeople();
      peopleExpected = peopleActual;

      // NOTE: By default it compares the person objects to see if they are Equal (they refer to the same object)
      CollectionAssert.AreEqual(peopleExpected, peopleActual);
    }

    [TestMethod]
    [Owner("PaulS")]
    public void AreCollectionsEqualWithComparerTest() {
      PersonManager mgr = new PersonManager();
      List<Person> peopleExpected = new List<Person>();
      List<Person> peopleActual = new List<Person>();

      peopleExpected.Add(new Person() { FirstName = "Paul", LastName = "Sheriff" });
      peopleExpected.Add(new Person() { FirstName = "John", LastName = "Kuhn" });
      peopleExpected.Add(new Person() { FirstName = "Jim", LastName = "Ruhl" });

      peopleActual = mgr.GetPeople();

      // Provide your own "Comparer" to determine equality
      CollectionAssert.AreEqual(peopleExpected, peopleActual,
        Comparer<Person>.Create((x, y) =>
          x.FirstName == y.FirstName && x.LastName == y.LastName ? 0 : 1));
    }

    [TestMethod]
    [Owner("PaulS")]
    public void AreCollectionsEquivalentTest() {
      PersonManager mgr = new PersonManager();
      List<Person> peopleExpected = new List<Person>();
      List<Person> peopleActual = new List<Person>();

      // Get Person objects
      peopleActual = mgr.GetPeople();

      // Add same Person objects to new collection, but in a different order
      peopleExpected.Add(peopleActual[1]);
      peopleExpected.Add(peopleActual[2]);
      peopleExpected.Add(peopleActual[0]);

      // Checks for same objects, but in any order
      CollectionAssert.AreEquivalent(peopleExpected, peopleActual);
    }

    [TestMethod]
    [Owner("PaulS")]
    public void IsCollectionOfTypeTest() {
      PersonManager mgr = new PersonManager();
      List<Person> peopleActual = new List<Person>();

      peopleActual = mgr.GetSupervisors();

      CollectionAssert.AllItemsAreInstancesOfType(peopleActual, typeof(Supervisor));
    }
  }
}