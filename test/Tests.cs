using App.Controllers;

namespace test;

[TestFixture]
public class Tests
{
        [Test]
    public void AreQueriesSimilar_WhenQueriesAreIdentical_ReturnsTrue()
    {
        // Arrange
        string query1 = "SELECT * FROM Customers WHERE Country='Mexico' ORDER BY CustomerName ASC";
        string query2 = "SELECT * FROM Customers WHERE Country='Mexico' ORDER BY CustomerName ASC";

        // Act
        bool result = QueryComparer.AreQueriesSimilar(query1, query2);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void AreQueriesSimilar_WhenQueriesAreABitDifferentButCloseToSame_ReturnsTrue()
    {
        // Arrange
        string query1 = "SELECT * FROM Customers WHERE Country='Mexico'";
        string query2 = "SELECT * FROM Orders WHERE ShipCountry='Mexico'";

        // Act
        bool result = QueryComparer.AreQueriesSimilar(query1, query2);

        // Assert
        Assert.IsTrue(result);
    }

        [Test]
    public void AreQueriesSimilar_WhenQueriesAreABitMoreDifferent_ReturnsFalse()
    {
        // Arrange
        string query1 = "SELECT * FROM Customers WHERE Country='Mexico' AND ShipCountry='Mexico' ORDER BY Country ASC";
        string query2 = "SELECT * FROM Orders WHERE ShipCountry='Mexico'";

        // Act
        bool result = QueryComparer.AreQueriesSimilar(query1, query2);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void AreQueriesSimilar_WhenQueriesHaveDifferentSelects_ReturnsTrue()
    {
        // Arrange
        string query1 = "SELECT CustomerName, ContactName, City FROM Customers WHERE Country='Mexico'";
        string query2 = "SELECT CustomerName, ContactTitle, Country FROM Customers WHERE Country='Mexico'";

        // Act
        bool result = QueryComparer.AreQueriesSimilar(query1, query2);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void AreQueriesSimilar_WhenQueriesHaveDifferentWheres_ReturnsTrue()
    {
        // Arrange
        string query1 = "SELECT * FROM Customers WHERE Country='Mexico'";
        string query2 = "SELECT * FROM Customers WHERE Country='Brazil'";

        // Act
        bool result = QueryComparer.AreQueriesSimilar(query1, query2);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void AreQueriesSimilar_WhenQueriesHaveDifferentSorts_ReturnsTrue()
    {
        // Arrange
        string query1 = "SELECT * FROM Customers WHERE Country='Mexico' ORDER BY CustomerName ASC";
        string query2 = "SELECT * FROM Customers WHERE Country='Mexico' ORDER BY ContactName ASC";

        // Act
        bool result = QueryComparer.AreQueriesSimilar(query1, query2);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void AreQueriesSimilar_WhenQueriesAreCompletelyDifferent_ReturnsFalse()
    {
        // Arrange
        string query1 = "SELECT * FROM Customers WHERE FirstName = 'John'";
        string query2 = "DELETE FROM Orders WHERE OrderDate < '2022-01-01'";

        // Act
        bool result = QueryComparer.AreQueriesSimilar(query1, query2);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void AreQueriesSimilar_WhenQueriesHaveDifferentOrderOfClauses_ReturnsTrue()
    {
        // Arrange
        string query1 = "SELECT * FROM Customers WHERE Country = 'USA' AND City = 'New York' ORDER BY LastName";
        string query2 = "SELECT * FROM Customers WHERE City = 'New York' AND Country = 'USA' ORDER BY LastName";

        // Act
        bool result = QueryComparer.AreQueriesSimilar(query1, query2);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void AreQueriesSimilar_WhenQueriesHaveDifferentSelectColumns_ReturnsTrue()
    {
        // Arrange
        string query1 = "SELECT FirstName, LastName FROM Customers";
        string query2 = "SELECT FirstName, City FROM Customers";

        // Act
        bool result = QueryComparer.AreQueriesSimilar(query1, query2);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void AreQueriesSimilar_WhenQueriesHaveDifferentFunctionsInSelectColumns_ReturnsTrue()
    {
        // Arrange
        string query1 = "SELECT FirstName, LastName, YEAR(BirthDate) FROM Customers";
        string query2 = "SELECT FirstName, LastName, MONTH(BirthDate) FROM Customers";

        // Act
        bool result = QueryComparer.AreQueriesSimilar(query1, query2);

        // Assert
        Assert.IsTrue(result);
    }
}
