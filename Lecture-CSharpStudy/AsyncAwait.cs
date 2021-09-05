using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lecture_CSharpStudy
{
    // 참고 link.
    // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/

    // Async, Await
    // 비동기 프로그래밍에서 사용.
    // Unity로 생각하면,
    // Coroutine과 유사. ( 비동기이나 Single Thread )

    class AsyncAwait
    {
        Task Test()
        {
            Console.WriteLine("Start Test");
            Task t = Task.Delay(3000);
            return t;
        }

        void TestAsync()
        {
            Console.WriteLine("Start TestAsync");
            Task t = Task.Delay(3000);  // 복잡한 작업 ( ex. DB나 FILE 작업. )

            t.Wait();  // 즉, 위의 작업이 끝날 때까지 기다리면, 다른 일이 DELAY에 빠진다.
            Console.WriteLine("End TestAsync");
        }

        async Task<int> TestAsyncAndAwait()
        {
            Console.WriteLine("Start TestAsyncAndAwait");
            Task t = Task.Delay(3000);

            // 다른 작업은 실행 시키되, 위의 task는 따로 대기 시킨다.
            // ( 내부적으로, 다른 새로운 Thread를 생성하여 실행하는 것. )
            await t;
            Console.WriteLine("End TestAsync");

            return 1;
        }

        void TaskTest()
        {
            Task t = Test();

            // Task인 t의 모든 동작이 끝날 때까지,
            // 기다리게 한다.
            t.Wait();
        }

        void TaskTestAsync()
        {
            TestAsync();
        }

        async void TaskTestAsyncAndAwait()
        {
            Task<int> resTask = TestAsyncAndAwait();

            // task를 돌리는 도중, 다른 일을 수행한다.
            Console.WriteLine("Do Somthing...");

            // 추후, 결과를 얻어올 때까지 기다린다.
            int result = await resTask;
            Console.WriteLine($"result: {result}");
        }

        public void Execute(int _id = 3)
        {
            switch (_id)
            {
                case 1:
                    TaskTest();
                    break;

                case 2:
                    TaskTestAsync();
                    break;

                case 3:
                    TaskTestAsyncAndAwait();
                    break;

                default:
                    break;
            }

            Console.WriteLine("Start While");
            while (true)
            {

            }
        }
    }
}
