using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.Models
{
    public abstract class BaseDemo<T> where T : class
    {
        public abstract void Run();
    }

    public interface ICommand<T> where T : class
    {
        void Run();
    }

    public class Internal1<T> : BaseDemo<T>, ICommand<T> where T :  class
    {
        public override void Run()
        {
            Console.WriteLine("1");
        }
    }

    public class Internal2<T> : BaseDemo<T>, ICommand<T> where T : class
    {
        public override void Run()
        {
            Console.WriteLine("2");
        }
    }




}
