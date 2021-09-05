using System;
using System.Threading.Tasks;

namespace Lecture_CSharpStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            AsyncAwait asyncAwait = new AsyncAwait();
            asyncAwait.Execute(3); // 1, 2
            */

            Linq linq = new Linq();
            linq.Execute();
        }
    }
}
