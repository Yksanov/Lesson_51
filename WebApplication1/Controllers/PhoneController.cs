using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class PhoneController : Controller
{
    private readonly PhoneStoreContext _context;
    private readonly IWebHostEnvironment _environment;

    public PhoneController(PhoneStoreContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }
    
    public IActionResult Index()
    {
        List<Phone> phones = _context.Phones.ToList();
        return View(phones);
    }

    public IActionResult Create()
    {
        return View();
    }

    public IActionResult GetFile(string phoneName)
    {
        string filename = $"{phoneName}.txt";
        string path = Path.Combine(_environment.WebRootPath, "file", filename);
        string fileType = "text/txt";
        
        if (System.IO.File.Exists(path))
        {
            return PhysicalFile(path, fileType, filename);
        }
        else
        {
            return NotFound($"{phoneName} такой файл не найдено!");
        }
    }
    
    //task2
    public IActionResult Test(string company)
    {
        Phone phones = _context.Phones.FirstOrDefault(x => x.Company == company);
        if (phones.Company == "Apple")
            return RedirectPermanent("https://www.apple.com/");
        else if (phones.Company == "Samsung")
            return RedirectPermanent("https://www.samsung.com/ru/");
        else if (phones.Company == "MI")
            return RedirectPermanent("https://www.mi.com/ru/");
        else
            return NotFound();
    }

    [HttpPost]
    public IActionResult Create(Phone phone)
    {
        if (phone != null)
        {
            _context.Add(phone);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(phone);
    }

    //Details
    [HttpGet]
    public IActionResult Details(int id)
    {
        List<Phone> phones = _context.Phones.ToList();
        var findId = phones.FirstOrDefault(p => p.Id == id);
        return View(findId);
    }
}