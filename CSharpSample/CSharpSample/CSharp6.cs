using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace CSharpSample
{
    public class CSharp6
    {
        // ---------------------
        // 初期化子
        // ---------------------
        // C#6.0 以降の実装
        public int A { get; set; } = 1;

        // C#5.0 以前の実装
        //public int A { get; set; }

        //public CSharp6()
        //{
        //    A = 1;
        //}

        // ---------------------
        // getterのみのプロパティ
        // ---------------------
        // C#6.0 以降の実装
        public int B { get; } = 2;

        // コンストラクタ内では書き換え可能
        //public CSharp6()
        //{
        //    B = 5;
        //}

        // C#5.0 以前の実装
        //public int B { get; private set; }

        //public CSharp6()
        //{
        //    B = 5;
        //}

        // ---------------------
        // null演算子
        // ---------------------
        // C#6.0 以降の実装
        public string GetInitial(string value)
        {
            return value?.Substring(0, 1);
        }

        // C#5.0 以前の実装
        //public string GetInitial(string value)
        //{
        //    string result = null;
        //    if (value != null)
        //    {
        //        result = value.Substring(0, 1);
        //    }
        //    return value.Substring(0, 1);
        //}


        // ---------------------
        // 式形式の関数
        // expression-bodiedな関数メンバー
        // ---------------------
        // C#6.0 以降の実装
        public int CalcMulti(int x, int y) => x * y;

        // C#5.0 以前の実装
        //public int CalcMulti(int x, int y)
        //{
        //    return x * y;
        //}

        // ---------------------
        // 文字列挿入（後で書き直す）
        // ---------------------
        // C#6.0 以降の実装
        public string FormatString(string x, string y)
        {
            var formatted = $"({x},{y})";
            return formatted;
        }

        // C#5.0 以前の実装
        //public string FormatString(string x, string y)
        //{
        //    var formatted = string.Format("({0}, {1})", x, y);
        //    return formatted;
        //}

        // ---------------------
        // nameof演算子（後で書き直す）
        // ---------------------
        // C#6.0 以降の実装
        public void CallNameOf()
        {
            Console.WriteLine(nameof(CallNameOf));
        }

        // C#5.0 以前の実装
        //public void CallNameOf()
        //{
        //    Console.WriteLine("CallNameOf");
        //}

        // ---------------------
        // using static
        // ---------------------
        // C#6.0 以降の実装
        public void CallStatic()
        {
            // using static System.Math; // を読み込む
            var pi = Min(1, 2);
        }

        // C#5.0 以前の実装
        //public void CallStatic()
        //{
        //    var pi = Math.Min(1, 2);
        //}

        // ---------------------
        // インデックス初期化子
        // ---------------------
        // C#6.0 以降の実装
        public Dictionary<string, string> GetDictinary()
        {
            var myDictionary = new Dictionary<string, string>
            {
                ["A"] = "ABC"
            };

            return myDictionary;
        }

        // C#5.0 以前の実装
        //public Dictionary<string, string> GetDictinary()
        //{
        //    var myDictionary = new Dictionary<string, string>();
        //    myDictionary["A"] = "ABC";
        //}

        // ---------------------
        // 例外フィルター
        // ---------------------
        // C#6.0 以降の実装
        public void FilterException(int x, int y)
        {
            try
            {
                // 関数

            }
            catch (ArgumentException e) when (e.ParamName == "x")
            {
                // xのときは無視
            }
            catch (ArgumentException e)
            {
                throw;
            }
        }

        // ---------------------
        // catch/finally句内でのawait演算子
        // ---------------------
        // C#6.0 以降の実装
        public static async Task MyAsync()
        {
            try
            {
                // 関数
            }
            catch (ArgumentException e)
            {
                using (var writer = new StreamWriter("log.txt"))
                    await writer.WriteAsync(e.ParamName);
            }
            finally
            {
                using (var writer = new StreamWriter("log.txt"))
                    await writer.WriteAsync("Done");
            }
        }
    }
}
