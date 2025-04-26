using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrnekProje.Data;
using OrnekProje.Interfaces;
using OrnekProje.Models.Entities;
using OrnekProje.Models.ViewModels;
using System.Threading.Tasks;

namespace OrnekProje.Controllers
{
    
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly OrnekProjeDbContext _context;

        public BookController(IBookRepository bookRepository, IGenericRepository<Category> categoryRepository, OrnekProjeDbContext context)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
            _context = context;
        }

        // Tüm kitapları listeleme
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            
            var books = await _bookRepository.GetBooksWithCategoriesAsync();

            var model = books.Select(b => new BookList_VM
            {
                Id = b.Id,
                Title = b.Title,
                Price = b.Price,
                Summary = b.Summary,
                PageCount = b.PageCount,
                OwnerFullName = $"{b.User.FirstName} {b.User.LastName}",
                OwnerId = b.User.Id, 
                Categories = b.BookCategories?                
                .Select(bc => bc.Category.Name)
                .ToList() ?? new List<string>()
            }).ToList();
            return View(model);
        }

        // Sadece ullanıcıya ait olan kitapları listeleme
        [Authorize]

        public async Task<IActionResult> MyBooks()
        {
            var userName = User.Identity.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user == null) return Unauthorized();

            var books = await _bookRepository.GetBooksWithCategoriesByUserAsync(user.Id);

            var model = books.Select(b => new BookList_VM
            {
                Id = b.Id,
                Title = b.Title,
                Price = b.Price,
                Summary = b.Summary,
                PageCount = b.PageCount,
                OwnerFullName = user.FirstName + " " + user.LastName,
                Categories = b.BookCategories?.Select(bc => bc.Category.Name).ToList() ?? new List<string>()
            }).ToList();

            return View(model);
        }


        // Yeni kitap ekleme
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryRepository.GetAllAsync();

            var vm = new AddBookForm_VM
            {
                Book = new AddBook_VM(),
                Categories = new SelectList(categories, "Id", "Name")
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddBookForm_VM model)
        {
            Console.WriteLine("🔥 Create POST çağrıldı");

            if (!ModelState.IsValid)
            {
                var cats = await _categoryRepository.GetAllAsync();
                model.Categories = new SelectList(cats, "Id", "Name");
                return View(model);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            if (user == null) return Unauthorized();

            var book = new Book
            {
                Title = model.Book.Title,
                Price = model.Book.Price,
                Summary = model.Book.Summary,
                PageCount = model.Book.PageCount,
                UserId = user.Id,
                BookCategories = model.SelectCategoryIds
                    .Select(id => new BookCategory { CategoryId = id })
                    .ToList()
            };

            await _bookRepository.AddAsync(book);
            await _bookRepository.SaveAsync();

            return RedirectToAction("Index", "Book");
        }


        // Bir kitabın detayını görüntüleme
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookRepository.GetBookWithCategoriesAsync(id);
            if (book == null) return NotFound();

                     

            var model = new BookDetails_VM
            {
                Id = book.Id,
                Title = book.Title,
                Price = book.Price,
                Summary = book.Summary,
                PageCount = book.PageCount,
                OwnerEmail = book.User.Email,
                OwnerId = book.User.Id,
                Categories = book.BookCategories?
            .Select(bc => bc.Category.Name)
            .ToList() ?? new List<string>()
            };
            return View(model);
        }

        // Bir kitabı güncelleme
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            // Sadece kendi eklediğini güncelleyebilmesi için UserId kontrolü
            // Tek kitap alması için GetBookWithCategoriesAsync kullanıldı
            var book = await _bookRepository.GetBookWithCategoriesAsync(id);
            if (book == null) return NotFound();

            var userName = User.Identity.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user == null) return NotFound();

            if(book.UserId != user.Id)
            {
                return View("AccessDenied");
            }

            var allCategories = await _categoryRepository.GetAllAsync();
            var selectedCategoryIds = book.BookCategories?
                .Select(bc => bc.CategoryId)
                .ToList() ?? new List<int>();

            
         
            var vm = new AddBookForm_VM
            {
                Book = new AddBook_VM
                {
                    Id = book.Id,
                    Title = book.Title,
                    Price = book.Price,
                    Summary = book.Summary,
                    PageCount = book.PageCount
                },
                SelectCategoryIds = selectedCategoryIds,
                Categories = new SelectList(allCategories, "Id", "Name")
            };

            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AddBookForm_VM model)
        {
            if (!ModelState.IsValid)
            {
                var allCats = await _categoryRepository.GetAllAsync();
                model.Categories = new SelectList(allCats, "Id", "Name");
                return View(model);
            }

            var book = await _bookRepository.GetByIdAsync(model.Book.Id);
            if (book == null) return NotFound();

            // Güncelleme
            book.Title = model.Book.Title;
            book.Price = model.Book.Price;
            book.Summary = model.Book.Summary;
            book.PageCount = model.Book.PageCount;

            // Kategorileri sıfırla ve yeniden ekle
            book.BookCategories = model.SelectCategoryIds
                .Select(catId => new BookCategory { CategoryId = catId, BookId = book.Id })
                .ToList();

            _bookRepository.Update(book);
            await _bookRepository.SaveAsync();

            return RedirectToAction("Index");
        }

        // Bir kitabı silme
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var book = await _context.Books
                 .Include(b => b.BookCategories)
                 .ThenInclude(bc => bc.Category)
                 .Include(b => b.User)
                 .FirstOrDefaultAsync(b => b.Id == id);


            if (book == null) return NotFound();

            var model = new BookDetails_VM
            {
                Id = book.Id,
                Title = book.Title,
                Price = book.Price,
                Summary = book.Summary,
                PageCount = book.PageCount,
                OwnerEmail = book.User.Email,
                Categories = book.BookCategories?
                      .Where(bc => bc.Category != null)
                     .Select(bc => bc.Category.Name)
                     .ToList() ?? new List<string>()

            };

            return View(model);
        }

        // Silme kontrolü
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _bookRepository.GetBookWithCategoriesAsync(id);
            if (book == null) return NotFound();

            var userName = User.Identity.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user == null) return NotFound();

            if (book.UserId != user.Id)
            {
                return View("AccessDenied");
            }

            _bookRepository.Remove(book);
            await _bookRepository.SaveAsync();

            return RedirectToAction("Index");
        }




    }
}
