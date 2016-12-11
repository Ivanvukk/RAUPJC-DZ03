using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Model;

namespace DZ3
{
    class Program
    {
        static void Main(string[] args)
        {
            TodoDbContext context = new TodoDbContext();
            Guid Id = Guid.NewGuid();
            Guid Id2 = Guid.NewGuid();
            TodoSqlRepository repozitorij = new TodoSqlRepository(context);
            TodoItem zadatak = new TodoItem("zadatak", Id);
            TodoItem zadatak2 = new TodoItem("zadatak2", Id);

            repozitorij.Add(zadatak);
            repozitorij.Add(zadatak2);
            context.SaveChanges();

            Console.WriteLine(repozitorij.GetActive(Id).Count() + " Aktivnih");
            Console.WriteLine(repozitorij.GetCompleted(Id).Count() + " Završenih");

            Console.WriteLine(repozitorij.MarkAsCompleted(zadatak.Id, Id));
            Console.WriteLine("Označen");
            context.SaveChanges();

            Console.WriteLine(repozitorij.GetActive(Id).Count() + " Aktivnih");
            Console.WriteLine(repozitorij.GetCompleted(Id).Count() + " Završenih");
            Console.WriteLine(repozitorij.GetAll(Id).Count() + " ukupno");

            Console.WriteLine(repozitorij.Remove(zadatak.Id, Id));
            Console.WriteLine("Obrisan");
            context.SaveChanges();

            Console.WriteLine(repozitorij.GetAll(Id).Count() + " ukupno");
            Console.WriteLine(repozitorij.GetActive(Id).Count() + " Aktivnih");
            Console.WriteLine(repozitorij.GetCompleted(Id).Count() + " Završenih");

            zadatak2.IsCompleted = true;
            repozitorij.Update(zadatak2, Id);
            context.SaveChanges();
            Console.WriteLine("Update");

            Console.WriteLine(repozitorij.GetAll(Id).Count() + " ukupno");
            Console.WriteLine(repozitorij.GetActive(Id).Count() + " Aktivnih");
            Console.WriteLine(repozitorij.GetCompleted(Id).Count() + " Završenih");
            Console.ReadLine();
        }
    }
}
