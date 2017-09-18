# CSharpSample
C#6、C#7の機能サンプル

## 初期化子（C#6.0）
プロパティを初期化できるようになり、短いコードで簡潔に表現できる。
フィールドを別で用意しなくて良いので、自動プロパティをそのまま利用でき、コードの記述量も減る。

初期化子がない場合、下記のいずれかの記述により、初期化を記述する必要がある。
* プロパティ用にフィールドを用意してフィールドを初期化
* コンストラクタでプロパティを初期化

### C#6.0 以降の実装
    public int A { get; set; } = 1;

### C#5.0 以前の実装
    public int A { get; set; }

    public CSharp6()
    {
        A = 1;
    }

## getterのみのプロパティ
getterのみすると、setがreadonlyで宣言できる。
getterのみのプロパティはコンストラクタでのみ初期化できる。
（コンパイルが展開すると、setterには、readonlyがつくため。）

本機能がない場合、下記のいずれかの記述が必要である。
* プロパティ用にreadonlyのフィールドを用意する。
* private set などにして、外部から書き換えられないようにする。（ただしクラス内からは操作可能）

### C#6.0 以降の実装

    public int B { get; } = 2;

    // コンストラクタ内では書き換え可能
    public CSharp6()
    {
        B = 5;
    }

### C#5.0 以前の実装

    public int B { get; private set; }

    public CSharp6()
    {
        B = 5;
    }

## null演算子
null演算子はnullのときは、nullを返し、null以外のときはメソッドやプロパティを参照した結果を返す演算子である。
例えば、nullのときだけ処理したいときに、null演算子を使用して簡潔に表現できる。

### C#6.0 以降の実装

    public string GetInitial(string value)
    {
        return value?.Substring(0, 1);
    }

### C#5.0 以前の実装

    public string GetInitial(string value)
    {
        string result = null;
        if (value != null)
        {
            result = value.Substring(0, 1);
        }
        return value.Substring(0, 1);
    }


## 式形式の関数（expression-bodiedな関数メンバー）
関数メンバーの実装が1つの式の場合のみ、=>を使った簡易文法で関数定義できる。
単一式なら、簡潔に短く定義ができる。

### C#6.0 以降の実装

    public int CalcMulti(int x, int y) => x * y;


### C#5.0 以前の実装

    public int CalcMulti(int x, int y)
    {
        return x * y;
    }


## 文字列挿入

### C#6.0 以降の実装

    public string FormatString(string x, string y)
    {
        var formatted = $"({x},{y})";
        return formatted;
    }

### C#5.0 以前の実装

    public string FormatString(string x, string y)
    {
        var formatted = string.Format("({0}, {1})", x, y);
        return formatted;
    }

## nameof演算子

### C#6.0 以降の実装

    public void CallNameOf()
    {
        Console.WriteLine(nameof(CallNameOf));
    }

### C#5.0 以前の実装

    public void CallNameOf()
    {
        Console.WriteLine("CallNameOf");
    }

## using statig

### C#6.0 以降の実装

    public void CallStatic()
    {
        // using static System.Math; // を読み込む
        var pi = Min(1, 2);
    }

### C#5.0 以前の実装

    public void CallStatic()
    {
        var pi = Math.Min(1, 2);
    }

## インデックス初期化子

### C#6.0 以降の実装

    public Dictionary<string, string> GetDictinary()
    {
        var myDictionary = new Dictionary<string, string>
        {
            ["A"] = "ABC"
        };

        return myDictionary;
    }

### C#5.0 以前の実装

    public Dictionary<string, string> GetDictinary()
    {
        var myDictionary = new Dictionary<string, string>();
        myDictionary["A"] = "ABC";
    }


## 例外フィルター

### C#6.0 以降の実装

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

## catch/finally句内でのawait演算子

### C#6.0 以降の実装

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


## タプル

### C#7.0 以降の実装

### C#6.0 以前の実装


## 出力変数宣言

### C#7.0 以降の実装

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


## 型スイッチ

### C#7.0 以降の実装

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

## 値の破棄

### C#7.0 以降の実装


## 参照戻り値と参照ローカル変数

### C#7.0 以降の実装

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


## ローカル関数

### C#7.0 以降の実装

    public int CallLocalMethod()
    {
        int func(int n) => n >= 1 ? n * func(n - 1) : 1;

        return func(10);
    }


## 非同期メソッドの戻り値に任意の型を使用可

### C#7.0 以降の実装


## 数値リテラルの改善

### C#7.0 以降の実装

    public void CallLiteral()
    {
        byte mask1 = 0b1010_1010;
        Console.WriteLine(mask1);

        uint mask2 = 0xFFFE_0000;
        Console.WriteLine(mask2);
        Console.WriteLine(mask2.ToString("X"));
    }


## throw式

### C#7.0 以降の実装

    void func() => throw new NotImplementedException;
    public string CallThrowFormula(object obj)
    {
        var s = obj as string ?? throw new ArgumentException(nameof(obj));

        return s.Length == 0 ? "empty" :
            s.Length < 5 ? "short" :
            throw new InvalidOperationException("too long");
    }


## 式形式メンバーの拡充

### C#7.0 以降の実装

    int param = 0;
    CSharp7(int p) => param = p;



#参考

http://ufcpp.net/study/csharp/ap_ver6.html

http://www.buildinsider.net/language/csharplang/0600

https://chomado.com/programming/c-sharp/new-features-in-c-sharp-6/
