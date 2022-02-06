using BLL.Entities;
using BLL.Services;
using DAL;
using DAL.Entities;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace LibraryAudit.Tests.BLL.Services
{
    [TestFixture]
    public class BookServiceTest
    {
        private List<Book> books;
        private List<BookDTO> booksDto;
        private Mock<IUnitOfWork> UnitOfWork;
        [SetUp]
        public void Setup()
        {
            books = new List<Book>{
                new Book { Author = "Author1", Title = "Title1", Id = 1, IsArchived = false, IsReserved = false },
                new Book { Author = "Author2", Title = "Title2", Id = 2, IsArchived = false, IsReserved = false },
                new Book { Author = "Author3", Title = "Title3", Id = 3, IsArchived = false, IsReserved = false },
                new Book { Author = "Author4", Title = "Title4", Id = 4, IsArchived = false, IsReserved = false },
                new Book { Author = "Author5", Title = "Title5", Id = 5, IsArchived = false, IsReserved = false }
            };
            booksDto = new List<BookDTO>{
                new BookDTO { Author = "Author1", Title = "Title1", Id = 1, IsArchived = false, IsReserved = false },
                new BookDTO { Author = "Author2", Title = "Title2", Id = 2, IsArchived = false, IsReserved = false },
                new BookDTO { Author = "Author3", Title = "Title3", Id = 3, IsArchived = false, IsReserved = false },
                new BookDTO { Author = "Author4", Title = "Title4", Id = 4, IsArchived = false, IsReserved = false },
                new BookDTO { Author = "Author5", Title = "Title5", Id = 5, IsArchived = false, IsReserved = false }
            };
            UnitOfWork = new Mock<IUnitOfWork>();
            UnitOfWork.Setup(u => u.Books.GetAllAsync()).ReturnsAsync(books);
            UnitOfWork.Setup(u => u.Books.CreateAsync(It.IsAny<Book>())).ReturnsAsync(true);

        }

        [Test]
        public async Task GetAllAsync_GettingAllBooks_Success()
        {
            //arange
            BookService service = new BookService(UnitOfWork.Object);
            //act
            var actual = await service.GetAllAsync();
            //assert
            string actualJson = JsonConvert.SerializeObject(actual);
            string expectedJson = JsonConvert.SerializeObject(booksDto);
            Assert.AreEqual(actualJson, expectedJson);
        }

        [Test]
        public async Task CreateAsync_CreatingBook_Success()
        {
            //arange
            BookService service = new BookService(UnitOfWork.Object);
            //act
            bool result = await service.CreateAsync(new BookDTO());
            //assert
            Assert.IsTrue(result);
            UnitOfWork.Verify(u => u.Books.CreateAsync(It.IsAny<Book>()), Times.Once);
        }

        [Test]
        public void CreateAsync_CreatingNullBook_ThrowsExeption()
        {
            //arange
            BookService service = new BookService(UnitOfWork.Object);
            //assert act
            Assert.ThrowsAsync<ArgumentNullException>(async () => await service.CreateAsync(null));
            UnitOfWork.Verify(u => u.Books.CreateAsync(It.IsAny<Book>()), Times.Never);
        }

        [TestCase(null)]
        [TestCase(-1)]
        [TestCase(-2)]
        public void GetByIdAsync_NullOrIncorrectId_ThrowsException(int? id)
        {
            //arrange
            BookService service = new BookService(UnitOfWork.Object);
            //act assert
            Assert.ThrowsAsync<ArgumentException>(async () => await service.GetByIdAsync(id));
            UnitOfWork.Verify(u => u.Books.GetByIdAsync(It.IsAny<int>()), Times.Never);
        }
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void GetByIdAsync_NotExistingId_ThrowsException(int? id)
        {
            //arrange
            UnitOfWork.Setup(u => u.Books.GetByIdAsync((int)id)).ReturnsAsync(value: null);
            BookService service = new BookService(UnitOfWork.Object);
            //act assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await service.GetByIdAsync((int)id));
            UnitOfWork.Verify(u => u.Books.GetByIdAsync((int)id), Times.Once);
        }
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task GetByIdAsync_ReturnBookById_Ok(int? id)
        {
            //arrange
            Book book = new Book();
            UnitOfWork.Setup(u => u.Books.GetByIdAsync((int)id)).ReturnsAsync(book);
            BookService service = new BookService(UnitOfWork.Object);
            //act 
            BookDTO actualBook = await service.GetByIdAsync((int)id);
            //assert
            Assert.IsTrue(actualBook.Equals(book));
            UnitOfWork.Verify(u => u.Books.GetByIdAsync((int)id), Times.Once);
        }
        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(-3)]
        public void DeleteAsync_DeleteByIncorrectId_ThrowsException(int id)
        {
            //arrange
            BookService service = new BookService(UnitOfWork.Object);
            //act assert
            Assert.ThrowsAsync<ArgumentException>(async () => await service.DeleteAsync(id));
            UnitOfWork.Verify(u => u.Books.DeleteAsync(id), Times.Never);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void ValidateId_UseValidId_Success(int? id)
        {
            //arrange
            BookService obj = new BookService(UnitOfWork.Object);
            Type bookService = typeof(BookService);
            MethodInfo method = bookService.GetMethod("ValidateId", BindingFlags.NonPublic | BindingFlags.Instance);
            //act
            bool res = (bool)method.Invoke(obj, new object[] { id });
            //assert
            Assert.IsTrue(res);
        }

        [TestCase(null)]
        [TestCase(-1)]
        [TestCase(-2)]
        public void ValidateId_UseInValidId_ThrowsException(int? id)
        {
            //arrange
            BookService obj = new BookService(UnitOfWork.Object);
            Type bookService = typeof(BookService);
            MethodInfo method = bookService.GetMethod("ValidateId", BindingFlags.NonPublic | BindingFlags.Instance);
            //act assert
            Assert.Throws<Exception>(() => method.Invoke(obj, new object[] { id }));
        }

    }
}
