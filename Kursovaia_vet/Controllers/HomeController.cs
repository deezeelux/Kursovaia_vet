using Kursovaia_vet.Models;
using Kursovaiy_vet_DTO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Kursovaia_vet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //Гет запрос для возвращения главной страницы
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //Пост запрос, который выполняет при нажатии на кнопку Log in
        [HttpPost]
        public IActionResult Index(string login, string pass)
        {
            if (login == "admin" && pass == "admin")
            {
                return RedirectPermanent("/Home/Table");
            }
            else
            {
                return View();
            }

        }
        
        //Добавление данных о питомце 
        public IActionResult AddPet(string name, double age, string breed, string ownersname, string diagnosis)
        {
            //Создание файла связи (труба)
            //Using нужен для уничтожения трубы после использования
            using (var context = new ApplicationContext())
            {
                var pet = new Pet()
                {
                    Name = name,
                    Age = age,
                    Breed = breed,
                    OwnersName = ownersname,
                    Diagnosis = diagnosis
                };
                context.Add(pet);
                context.SaveChanges();  
            }
            TempData["alert"] = "Complete!";
            return RedirectPermanent("/Home/Index");
        }

        //Создание веб-страницы с таблицей зверей из БД
        public IActionResult Table()
        {
            //Тип Pet берем из DTO
            List<Pet> Pets = new List<Pet>();
            using (var context = new ApplicationContext()) 
            {
                Pets = context.Pets.ToList();
            }
            return View(Pets);
        }

        //Удаление выделенной строки
        public IActionResult DelPet(bool[] isChecked, int[] id)
        {
            using (var context = new ApplicationContext())
            {
                for (int i = 0; i < id.Length; i++){
                    if (isChecked[i])
                    {
                        //Метод внутри метода. Удалить диапазон объектов подходящих под условие в скобках
                        context.Pets.RemoveRange(context.Pets.Where(x => x.Id == id[i]));
                    }
                }
                context.SaveChanges();
            }
            return RedirectPermanent("/Home/Table");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
