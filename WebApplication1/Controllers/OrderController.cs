using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class OrderController : Controller
{
    private readonly PhoneStoreContext _context;

    public OrderController(PhoneStoreContext context)
    {
        _context = context;
    }
    // GET
    public IActionResult Index()
    {
        List<Order> orders = _context.Orders.Include(o => o.Phone).ToList(); 
        return View(orders);
    }

    //Edit
    public IActionResult Edit(int id)
    {
        Order o = _context.Orders.FirstOrDefault(x => x.Id == id);
        return View(o);
    }

    [HttpPost]
    public IActionResult Edit(Order order)
    {
        _context.Update(order);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Create(int id)
    {
        Phone p = _context.Phones.FirstOrDefault(x => x.Id == id);
        ViewBag.Phone = p;
        return View();
    }

    [HttpPost]
    public IActionResult Create(Order order)
    {
        if (order != null)
        {
            Order o = new Order()
            {
                Name = order.Name,
                Address = order.Address,
                ContactPhone = order.ContactPhone,
                PhoneId = order.PhoneId
            };
            
            _context.Add(o);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return NotFound();
    }
    
}