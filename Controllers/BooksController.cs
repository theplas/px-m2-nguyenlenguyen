using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Quanlysach.Models;
using Quanlysach.ViewsModels;

namespace QuanLySach.Controllers
{

    public class BooksController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BooksController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            ViewBag.Message = " Manage books ";



            var products = _appDbContext.Books.Include(p => p.Category).ToList();

            return View(products);
        }

        public IActionResult Create()
        {
            BooksCreateVM booksVM = new BooksCreateVM()
            {
                Books = new Books(),
                CategorySelectList = _appDbContext.Categories.Select(item => new SelectListItem
                {
                    Text = item.CategoryName,
                    Value = item.Id.ToString()
                })
            };

            return View(booksVM);
        }

        [HttpPost]
        public IActionResult Create(BooksCreateVM booksCreateVM)
        {


            _appDbContext.Books.Add(booksCreateVM.Books);
            _appDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            BooksCreateVM booksVM = new BooksCreateVM()
            {
                Books = _appDbContext.Books.Find(id),
                CategorySelectList = _appDbContext.Categories.Select(item => new SelectListItem
                {
                    Text = item.CategoryName,
                    Value = item.Id.ToString()
                })
            };

            return View(booksVM);
        }

        [HttpPost]
        public IActionResult Edit(BooksCreateVM booksCreateVM)
        {

            if (ModelState.IsValid)
            {
                _appDbContext.Books.Update(booksCreateVM.Books);
                _appDbContext.SaveChanges();

                // return RedirectToAction("Index");
            }




            return RedirectToAction("booksCreateVM");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var books = _appDbContext.Books.Find(id);
            if (books == null) return NotFound();

            return View(books);
        }

        [HttpPost]
        public IActionResult DeleteProduct(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var books = _appDbContext.Books.Find(id);
            if (books == null) return NotFound();

            _appDbContext.Books.Remove(books);
            _appDbContext.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}