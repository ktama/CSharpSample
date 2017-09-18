using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSample
{
    class CSharp7
    {
        // ---------------------
        // タプル
        // ---------------------
        // NuGet で System.ValueTuple のインストールが必要
        public (int quontient, int remainder) Divsion(int dividend, int divisor)
        {
            int quont = dividend / divisor;
            int remain = dividend % divisor;
            return (quont, remain);
        }

        public void CallDivision1()
        {
            var result = Divsion(6, 2);
            Console.WriteLine($"商：{result.quontient}, 余：{result.remainder}");
        }

        // ---------------------
        // 分解
        // ---------------------
        public void CallDivision2()
        {
            var (q, r) = Divsion(6, 2);
            Console.WriteLine($"商：{q}, 余：{r}");
        }

        // ---------------------
        // 値の破棄
        // ---------------------
        public void CallDivision3()
        {
            var (q, _) = Divsion(6, 2);
            Console.WriteLine($"商：{q}");

            (_, var r) = Divsion(6, 2);
            Console.WriteLine($"余：{r}");
        }

        // ---------------------
        // 出力変数宣言
        // ---------------------
        // C#7.0 以降の実装
        public void CalcMulti(int x1, int x2, out int y)
        {
            y = x1 * x2;
        }

        public int DeclarationVariable1()
        {
            this.CalcMulti(2, 3, out var y);

            return y;
        }

        // C#6.0 以前の実装
        public int DeclarationVariable2()
        {
            int y;
            this.CalcMulti(2, 3, out y);

            return y;
        }


        // ---------------------
        // 型スイッチ
        // ---------------------
        public void CheckType(object obj)
        {
            if(obj is float f)
            {
                Console.WriteLine($"float型：{f}");
            }

            switch(obj)
            {
                case int n when n > 0:
                    Console.WriteLine($"正の値のint型：{n}");
                    break;
                case int n:
                    Console.WriteLine($"0以下の値のint型：{n}");
                    break;
                default:
                    Console.WriteLine("int型以外");
                    break;
            }
        }

        // ---------------------
        // 参照戻り値と参照ローカル変数
        // ---------------------
        public void CallRefMethod()
        {
            var x = 10;
            var y = 20;

            ref var n = ref Max(ref x, ref y);

            // yに1が加えられる
            n++;

            Console.WriteLine($"x：{x}, y：{y}");
        }

        public ref int Max(ref int x, ref int y)
        {
            if (x > y)
            {
                return ref x;
            }
            else
            {
                return ref y;
            }
        }

        // ---------------------
        // ローカル関数
        // ---------------------
        public int CallLocalMethod()
        {
            int func(int n) => n >= 1 ? n * func(n - 1) : 1;

            return func(10);
        }

        // ---------------------
        // 非同期メソッドの戻り値に任意の型を使用可
        // ---------------------
        // NuGet で System.Threading.Tasks.Extensions のインストールが必要
        public static async ValueTask<int> DoAsync(Random r)
        {
            if (r.NextDouble() < 0.99)
            {
                // 99%の確率でこちらの処理
                // awaitがないため、同期処理となる。
                // 同期処理のため、重い処理であるTask<int>の作成を回避する。
                return 1;
            }
            // 非同期処理のため、Task<int>を作成する。
            await Task.Delay(100);
            return 0;
        }

        // ---------------------
        // 数値リテラルの改善
        // ---------------------
        public void CallLiteral()
        {
            byte mask1 = 0b1010_1010;
            Console.WriteLine(mask1);

            uint mask2 = 0xFFFE_0000;
            Console.WriteLine(mask2);
            Console.WriteLine(mask2.ToString("X"));
        }

        // ---------------------
        // throw式
        // ---------------------
        void func() => throw new NotImplementedException();
        public string CallThrowFormula(object obj)
        {
            var s = obj as string ?? throw new ArgumentException(nameof(obj));

            return s.Length == 0 ? "empty" :
                s.Length < 5 ? "short" :
                throw new InvalidOperationException("too long");
        }

        // ---------------------
        // 式形式のメンバーの拡充
        // ---------------------
        // もうちょっと書く
        int param = 0;
        CSharp7(int p) => param = p;

    }

    public class MemberTest
    {
        private static int x = 0;
        private Dictionary<int, int> xDictionary = new Dictionary<int, int>():

        public MemberTest() => x = 0;
        public MemberTest(int p) => x = p;

        public int X
        {
            get => x;
            set => x = value;
        }

        public int this[int key]
        {
            get => xDictionary[key];
            set => xDictionary[key] = value;
        }

        public event Action EventTest
        {
            add => ++x;
            remove => --x;
        }
    }
}
