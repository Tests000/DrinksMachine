using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using DrinksMachine.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DrinksMachine.Controllers
{


    public class HomeController : Controller
    {

        public DrinksMachineDBContext db;

        public IWebHostEnvironment env;

        public HomeController(DrinksMachineDBContext context, IWebHostEnvironment env)
        {
            db = context;
            this.env = env;
        }

        [HttpGet]
        public IActionResult Index(string res=null, List<int> change=null)
        {
            var drinks = db.Drinks.ToList<Drink>();
            ViewData["money"] = db.Coins.ToList<Coin>();
            ViewData["res"] = res;

            ViewData["change"] = change;
            return View(drinks);
        }

        [HttpPost]
        public IActionResult Index(int r1, int r2, int r5, int r10, int DrinkID)
        {
            int[] money = { r1, r2, r5, r10 };

            var coins = db.Coins.ToArray<Coin>();
            int i = 0;
            foreach (var item in coins)
            {
                item.Count += money[i];
                i++;
            }

            coins = coins.Reverse().ToArray<Coin>();

            var sum = money[0] + money[1] * 2 + money[2] * 5 + money[3] * 10;

            var Change = new List<int>();

            var drink = db.Drinks.FirstOrDefault(u => u.Id == DrinkID);

            foreach (var item in coins)
            {
                for (int j = 0; item.Count > 0 && sum - drink.Price >= item.Dignity; j++)
                {
                    sum -= item.Dignity;
                    item.Count--;
                    Change.Add(item.Dignity);
                }
            }

            drink.Count--;
            db.Coins.UpdateRange(coins);
            db.Drinks.Update(drink);
            db.SaveChanges();
            var drinks = db.Drinks.ToList<Drink>();
            ViewData["money"] = db.Coins.ToList<Coin>();
            if (sum == drink.Price)
                ViewData["res"] = null;
            else
                ViewData["res"] = "Денег в кассе не достаточно,\n обратитесь в поддержку 8(800) 555-35-35";

            ViewData["change"] = Change;
            return View(drinks);
        }

        [HttpGet]
        public IActionResult Admin()
        {
            ViewData["coins"] = db.Coins.ToList<Coin>();
            ViewData["drinks"] = db.Drinks.ToList<Drink>();
            return View();
        }

        [HttpPost]
        public IActionResult Admin(int r1, int r2, int r5, int r10, string r1enable, string r2enable, string r5enable, string r10enable, IFormFile Image, string Name, int? Count, int? Cost, int? objId)
        {
            if (objId != null && objId == -1 && Cost != null && Count != null && Count > 0 && Cost > 0 && Name != null && Image != null)
            {
                var drink = new Drink();
                using (var stream = new MemoryStream((int)Image.Length))
                {
                    Image.CopyTo(stream);
                    drink.image = stream.ToArray();
                }
                drink.Count = (int)Count;
                drink.Name = Name;
                drink.Price = (int)Cost;
                db.Drinks.Add(drink);

            }
            else if (objId != null && objId != -1)
            {
                var drink = db.Drinks.FirstOrDefault(u => u.Id == objId);
                if (Count != null && Count > 0)
                    drink.Count = (int)Count;
                if (Image != null)
                {
                    using (var stream = new MemoryStream((int)Image.Length))
                    {
                        Image.CopyTo(stream);
                        drink.image = stream.ToArray();
                    }
                }
                if (Name != null)
                    drink.Name = Name;
                if (Cost != null && Cost > 0)
                    drink.Price = (int)Cost;
                db.Drinks.Update(drink);
            }
            else
            {
                var coins = db.Coins.ToArray<Coin>();
                coins[0].Count = r1;
                coins[0].Enabled = r1enable == null ? false : true;
                coins[1].Count = r2;
                coins[1].Enabled = r2enable == null ? false : true;
                coins[2].Count = r5;
                coins[2].Enabled = r5enable == null ? false : true;
                coins[3].Count = r10;
                coins[3].Enabled = r10enable == null ? false : true;
                db.Coins.UpdateRange(coins);
            }
            db.SaveChanges();
            ViewData["drinks"] = db.Drinks.ToList<Drink>();
            ViewData["coins"] = db.Coins.ToList<Coin>();
            return View();
        }

    }
}
