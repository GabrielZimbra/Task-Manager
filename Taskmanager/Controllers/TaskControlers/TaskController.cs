using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taskmanager.Models.task;



namespace Taskmanager.Controllers.TaskControlers
{
    public class TaskController : Controller
    {
        // GET: Task

        private static List<TaskViewModel> tasks = new List<TaskViewModel>
        {
            new TaskViewModel { Id = 1, Title = "Tarefa 01", Description = "Primeira Tarefa", IsCompleted = true },
            new TaskViewModel { Id = 2, Title = "Tarefa 02", Description = "Segunda Tarefa", IsCompleted = false },
        };
        public ActionResult Index()
        {
            return View(tasks);
        }

        public ActionResult Create()
        {
            return View(new TaskViewModel());
        }
        [HttpPost]
        public ActionResult Create(TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                task.Id = tasks.Any() ? tasks.Max(tarefa => tarefa.Id) + 1 : 1 ;
                tasks.Add(task);
                return RedirectToAction("Index");
            }
            return View(task);

        }
        
        public ActionResult Edit(int id)
        {
            var task = tasks.FirstOrDefault(tarefa => tarefa.Id == id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);  
        }
        [HttpPost]
        public ActionResult Edit(TaskViewModel updatedTask)
        {
            if (ModelState.IsValid) 
            {
                var task = tasks.FirstOrDefault(tarefa => tarefa.Id == updatedTask.Id);
                if(task !=null)
                {
                    task.Title = updatedTask.Title;
                    task.Description = updatedTask.Description;
                    task.IsCompleted = updatedTask.IsCompleted;
                }
                return RedirectToAction("Index");
            }
            return View(updatedTask);
 
        }

        public ActionResult Details(int id)
        {
            var task = tasks.FirstOrDefault(tarefa =>tarefa.Id == id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        public ActionResult Delete(int id)
        {
            var task = tasks.FirstOrDefault(tarefa => tarefa.Id == id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);

        }
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var task = tasks.FirstOrDefault(tarefa => tarefa.Id == id);
            if (task != null)
            {
                tasks.Remove(task);
            }
            return RedirectToAction("Index");



        }


    }
}