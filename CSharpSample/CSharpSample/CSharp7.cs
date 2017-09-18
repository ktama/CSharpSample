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
        // 参照できない 調べる
        // .NET Framework 4.7 または .NET Standard 1.7 以降は標準ライブラリ
        // 以前は System.ValueTuple を参照
        public (int quontient, int remainder) Divsion(int dividend, int divisor)
        {
            int quont = dividend / divisor;
            int remain = dividend % divisor;
            return (quont, remain);
        }

        // ---------------------
        // 出力変数宣言
        // ---------------------
        public int DeclarationVariable()
        {
            // C#7.0 以降の実装
            this.CalcMulti(2, 3, out var y);
            // C#6.0 以前の実装
            //int y;
            //this.CalcMulti(2, 3, out y);

            return y;
        }

        public void CalcMulti(int x1, int x2, out int y)
        {
            y = x1 * x2;
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
        // 値の破棄
        // ---------------------

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
        // コンパイルエラーになる
        public static async ValueTask<int> DoAsync(Random r)
        {
            if (r.NextDouble() < 0.99)
            {
                return 1;
            }
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
}
