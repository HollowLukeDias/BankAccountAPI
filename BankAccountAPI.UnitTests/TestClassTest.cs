using BankAccountAPI.Models;
using BankAccountAPI.Repositories.Testing;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BankAccountAPI.UnitTests
{
    [TestFixture]
    class TestClassTest
    {
        private Mock<DbContext> _context;
        private Mock<DbSet<TestClass>> _dbSetMock;
        private TestClass _testObject;

        [SetUp]
        public void SetUp()
        {
            _testObject = new TestClass { Id = 1 };
            _context = new Mock<DbContext>();
            _dbSetMock = new Mock<DbSet<TestClass>>();
            _context.Setup(x => x.Set<TestClass>()).Returns(_dbSetMock.Object);
        }

       [Test]
        public void Add_TestClassObjectPassed_ProperMethodCalled()
        {
            // Arrange
            _testObject = new TestClass();
            _dbSetMock.Setup(x => x.Add(It.IsAny<TestClass>()));

            // Act
            var repository = new Repository<TestClass>(_context.Object);
            repository.Add(_testObject);

            //Assert
            _context.Verify(x => x.Set<TestClass>());
            _dbSetMock.Verify(x => x.Add(It.Is<TestClass>(y => y == _testObject)));
        }

        [Test]  
        public void Remove_TestClassObjectPassed_ProperMethodCalled()
        {
            // Arrange
            _testObject = new TestClass();
            _dbSetMock.Setup(x => x.Remove(It.IsAny<TestClass>()));

            // Act
            var repository = new Repository<TestClass>(_context.Object);
            repository.Remove(_testObject);

            //Assert
            _context.Verify(x => x.Set<TestClass>());
            _dbSetMock.Verify(x => x.Remove(It.Is<TestClass>(y => y == _testObject)));
        }

        [Test]
        public void Get_TestClassObjectPassed_ProperMethodCalled()
        {
            _dbSetMock.Setup(x => x.Find(It.IsAny<int>())).Returns(_testObject);

            var repository = new Repository<TestClass>(_context.Object);
            repository.Get(1);

            _context.Verify(x => x.Set<TestClass>());
            _dbSetMock.Verify(x => x.Find(It.IsAny<int>()));
        }

        [Test]
        public void GetAll_TestClassObjectPassed_ProperMethodCalled()
        {
            var testList = new List<TestClass> { _testObject };

            _dbSetMock.As<IQueryable<TestClass>>().Setup(x => x.Provider).Returns(testList.AsQueryable().Provider);
            _dbSetMock.As<IQueryable<TestClass>>().Setup(x => x.Expression).Returns(testList.AsQueryable().Expression);
            _dbSetMock.As<IQueryable<TestClass>>().Setup(x => x.ElementType).Returns(testList.AsQueryable().ElementType);
            _dbSetMock.As<IQueryable<TestClass>>().Setup(x => x.GetEnumerator()).Returns(testList.AsQueryable().GetEnumerator());

            _context.Setup(x => x.Set<TestClass>()).Returns(_dbSetMock.Object);

            var repository = new Repository<TestClass>(_context.Object);
            var result = repository.GetAll();

            Assert.That(testList, Is.EqualTo(result.ToList()));
        }

        [Test]
        public void Find_TestClassObjectPassed_ProperMethodCalled()
        {
            var testList = new List<TestClass>() { _testObject };

            _dbSetMock.As<IQueryable<TestClass>>().Setup(x => x.Provider).Returns(testList.AsQueryable().Provider);
            _dbSetMock.As<IQueryable<TestClass>>().Setup(x => x.Expression).Returns(testList.AsQueryable().Expression);
            _dbSetMock.As<IQueryable<TestClass>>().Setup(x => x.ElementType).Returns(testList.AsQueryable().ElementType);
            _dbSetMock.As<IQueryable<TestClass>>().Setup(x => x.GetEnumerator()).Returns(testList.AsQueryable().GetEnumerator());

            _context.Setup(x => x.Set<TestClass>()).Returns(_dbSetMock.Object);

            var repository = new Repository<TestClass>(_context.Object);

            var result = repository.Find(x => x.Id == 1);

            Assert.That(testList, Is.EqualTo(result.ToList()));
        }
    }
}
