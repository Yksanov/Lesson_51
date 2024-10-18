using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    public IActionResult GetMessage()
    {
        return PartialView("_GetMessage");
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
    
    //task3
    public IActionResult Edit(int id)
    {
        Phone p = _context.Phones.FirstOrDefault(x => x.Id == id);
        if (p == null)
        {
            return NotFound($"Phone with this id: {id} not found");
        }
        else
        {
            return View(p);
        }
    }

    [HttpPost]
    public IActionResult Edit(Phone phone)
    {
        try
        {
            if (phone != null)
            {
                _context.Update(phone);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return View(phone);
    }
    //-------------------------------------------
    
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
    //-------------------------------------------
    //Details
    [HttpGet]
    public IActionResult Details(int id)
    {
        List<Phone> phones = _context.Phones.ToList();
        var findId = phones.FirstOrDefault(p => p.Id == id);
        return View(findId);
    }

    public async Task<IActionResult> Details2(int id)
    {
        Phone? phone = await _context.Phones.FirstOrDefaultAsync(p => p.Id == id);
        if (phone != null)
        {
            return View(phone);
        }

        return NotFound();
    }
    //-------------------------------------------
    //Delete

    public async Task<IActionResult> Delete(int id)
    {
        Phone? phone = await _context.Phones.FirstOrDefaultAsync(p => p.Id == id);
        if (phone != null)
        {
            _context.Remove(phone);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index");
    }
    //------------------------------------------
    //Edit2

    public async Task<IActionResult> Edit2(int id)
    {
        Phone? phone = await _context.Phones.FirstOrDefaultAsync(p => p.Id == id);
        if (phone != null)
        {
            return View(phone);
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Edit2(Phone? phone)
    {
        if (phone != null)
        {
            _context.Update(phone);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        return View(phone);
    }
}