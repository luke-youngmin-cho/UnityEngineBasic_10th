namespace Collections
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[3];
            arr[1] = 1;

            MyDynamicArray<int> da = new MyDynamicArray<int>();
            da[1] = 1;

            da.Find(x => x > 3);

            // using 구문 : IDisposable 객체의 Dispose() 호출을 보장하는 구문
            using (IEnumerator<int> e = da.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    Console.WriteLine(e.Current);
                }
                e.Reset();
            }

            // IEnumerable 을 순회하는 구문
            foreach (var item in da)
            {
                Console.WriteLine(item);
            }

            List<int> list = new List<int>();

        }
    }
}