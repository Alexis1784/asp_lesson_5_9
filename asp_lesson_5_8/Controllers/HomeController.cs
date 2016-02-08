using asp_lesson_5_8.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace asp_lesson_5_8.Controllers
{
    public class HomeController : Controller
    {
        private StudentsContext db = new StudentsContext();

        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }

        public ActionResult Details(int id = 0)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.Courses = db.Courses.ToList();
            return View(student);
        }

        //http://metanit.com/sharp/mvc5/5.10.php
        [HttpPost]
        public ActionResult Edit(Student student, int[] selectedCourses)
        {
            Student newStudent = db.Students.Find(student.Id);
            newStudent.Name = student.Name;
            newStudent.Surname = student.Surname;

            newStudent.Courses.Clear();
            if (selectedCourses != null)
            {
                //получаем выбранные курсы
                foreach (var c in db.Courses.Where(co => selectedCourses.Contains(co.Id)))
                {
                    newStudent.Courses.Add(c);
                }
            }

            db.Entry(newStudent).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}