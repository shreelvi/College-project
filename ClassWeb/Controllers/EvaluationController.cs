using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassWeb;
using ClassWeb.Models;
using ClassWeb.Model;

namespace ClassWeb.Controllers
{
    public class EvaluationController : Controller
    {
        //private readonly PeerValContext _context;

        //public EvaluationController(PeerValContext context)
        //{
        //    _context = context;
        //}

        // GET: Evaluation
        public async Task<IActionResult> Index()
        {
            return View(DAL.GetEvaluations());
        }

       

        
    }
}
