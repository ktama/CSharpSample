# CSharpSample
C#6、C#7の機能サンプル

## 初期化子（C#6.0）
プロパティを初期化できるようになり、短いコードで簡潔に表現できる。
フィールドを別で用意しなくて良いので、自動プロパティをそのまま利用でき、コードの記述量も減る。

初期化子がない場合、下記のいずれかの記述により、初期化を記述する必要がある。
* プロパティ用にフィールドを用意してフィールドを初期化する。
* コンストラクタでプロパティを初期化する。

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
文字列を整形するための構文ができた。
今まで、string.Format()で記述していた整形処理を$で記述できる。

利点は以下の通り。
* コード量が短くなった。（string.Format → $)
* 値を埋め込みたい場所にそのまま記述できる。（string.Formatは長い記述や埋め込む値が多い場合、可読性が落ちる。）

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
フィールド、クラス、メソッド、プロパティなどの識別子をnameof演算子で取得できる。

nameofで記述すると、識別子として扱われる。
すなわち、リネームの対象やコード解析の対象となる。

例えば、デバッグ、Exceptionなどの識別子を文字列で記述していた場合、識別子をリネームして変更漏れになる可能性がある。nameofの場合は、リネームの変更対象となるため、変更漏れのリスクを軽減できる。

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

## using static
using staticを使用すると、静的メンバーをメンバー名だけで参照できる。

Mathクラスなど、静的メンバーを持つクラスに対して、コード量を削減できる。

### C#6.0 以降の実装

    using static System.Math;
    public void CallStatic()
    {
        var pi = Min(1, 2);
    }

### C#5.0 以前の実装

    public void CallStatic()
    {
        var pi = Math.Min(1, 2);
    }

## インデックス初期化子
オブジェクト初期化子にインデクサーを記述できるようになった。
Dictionaryなどインデックスのあるオブジェクトで使用できる。

特に式しかかけないときに効果的に記述できる。
例えば、GetDictionary2のようにexpression-bodiedな関数定義で使用することができる。

### C#6.0 以降の実装

    public Dictionary<string, string> GetDictinary1()
    {
        var myDictionary = new Dictionary<string, string>
        {
            ["A"] = "ABC"
        };

        return myDictionary;
    }

    Dictionary<string, string> GetDictionary2(string x) => new Dictionary<string, string>
    {
        ["A"] = x,
        ["B"] = x + x
    };

    public Dictionary<string, string> CallDictinary2()
    {

        return GetDictionary2("a");
    }

### C#5.0 以前の実装

    public Dictionary<string, string> GetDictinary()
    {
        var myDictionary = new Dictionary<string, string>();
        myDictionary["A"] = "ABC";
    }


## 例外フィルター
catch句に条件を記述できるようになった。
catchした例外に加えて、例外の中身を見て分岐できる。

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
catch/finally句でawaitを記述できるようになった。
例えば、ログの非同期処理をcatch句に記述するなどの用途に使用できる。

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
C#7.0 からタプルを使えるようになった。
関数の戻り値で複数の値を取得したいときに役立つ。

使用するには、 NuGet で System.ValueTuple をインストールする必要がある。

### C#7.0 以降の実装

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

## 分解
タプルを分けて受け取りたい場合に、分解構文が追加された。
タプルで使用した同関数に対して、戻り値の受け取りを分解構文で受けた場合は下記の実装となる。

### C#7.0 以降の実装
    public void CallDivision2()
    {
        var (q, r) = Divsion(6, 2);
        Console.WriteLine($"商：{q}, 余：{r}");
    }

## 値の破棄
分解するときに、一部の値が不要なときは破棄ができる。
_を記述すると、値を受け取らずに処理できる。

### C#7.0 以降の実装
    public void CallDivision3()
    {
        var (q, _) = Divsion(6, 2);
        Console.WriteLine($"商：{q}");

        (_, var r) = Divsion(6, 2);
        Console.WriteLine($"余：{r}");
    }

## 出力変数宣言
出力変数は関数の呼び出し時に宣言できるようになった。
C#6.0 以前では、関数の呼び出し前に出力変数を宣言して、関数を呼び出していた。

### C#7.0 以降の実装

    public void CalcMulti(int x1, int x2, out int y)
    {
        y = x1 * x2;
    }

    public int DeclarationVariable1()
    {
        this.CalcMulti(2, 3, out var y);

        return y;
    }

### C#6.0 以前の実装

    public int DeclarationVariable2()
    {
        int y;
        this.CalcMulti(2, 3, out y);

        return y;
    }

## 型スイッチ
インスタンスの型をswitch句のcaseで分岐できるようになった。
is演算子、switch句のcaseでキャスト結果を変数で受け取れるようになった。

* caseでis演算子と同じように、インスタンスの型の分岐できる。
* x is T t、case T tで型が一致していた場合、キャスト結果をtで受け取れる。

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

## 参照戻り値と参照ローカル変数
戻り値とローカル変数でも、参照渡しができるようになった。
戻り値の型の前、値を渡す側、、値を受ける側それぞれにref修飾子を付けて、記述する。

関数などに大きな値を渡すときにコピーなしに実行できるため、パフォーマンスが向上する。

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
関数の中に関数を記述できるようになった。
ラムダ式に比べて、パフォーマンスの良いコードが生成されるように最適化されている。

### C#7.0 以降の実装

    public int CallLocalMethod()
    {
        int func(int n) => n >= 1 ? n * func(n - 1) : 1;

        return func(10);
    }


## 非同期メソッドの戻り値に任意の型を使用可
非同期メソッドの戻り値にValueTask<TResult>が追加された。

非同期メソッドの戻り値は下記のいずれかとなった。
* void
* Task
* Task<TResult>
* ValueTask<TResult>

特定の条件下で任意の型を非同期メソッドの戻り値として使える。
有効な例は、例えば、稀に非同期処理となる場合、Task<T>インスタンスの作成を回避する策として使える。

使用するには、 NuGet で System.Threading.Tasks.Extensions をインストールする必要がある。

### C#7.0 以降の実装

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

## 数値リテラルの改善
数値リテラルが便利になった。
追加された機能は下記の2つがある。
* 2進数リテラル記述できる。
* _で桁区切りできる。

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
一部の式にthrow式を記述できるようになった。
記述できるパターンは下記の3つがある。
* ラムダ式または式形式メンバーの中（=>の後ろ）
* null合体演算子（??）の後ろ
* 条件演算子の条件式以外の部分である第2、第3引数（：の前後）

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
式形式メンバーが増えた。
使えるメンバーは下記のがある。
* メソッド（C#6.0 以降）
* 演算子（C#6.0 以降）
* プロパティ（C#6.0 以降）
* インデクサー（get-only）（C#6.0 以降）
* コンストラクター（C#7.0 以降）
* デストラクター（C#7.0 以降）
* イベント（C#7.0 以降）

### C#7.0 以降の実装

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

# 参考

## C# 6.0
[++C++; // 未確認飛行 C C# 6の新機能](http://ufcpp.net/study/csharp/ap_ver6.html)

[Build INSIDER C# 6.0で知っておくべき12の新機能](http://www.buildinsider.net/language/csharplang/0600)

[ちょまど帳 C# 6.0の新機能](https://chomado.com/programming/c-sharp/new-features-in-c-sharp-6/)


## C# 7.0

[++C++; // 未確認飛行 C C# 7の新機能](http://ufcpp.net/study/csharp/cheatsheet/ap_ver7/)

[Build INSIDER C# 7.0で知っておくべき10の新機能（前編）](http://www.buildinsider.net/language/csharplang/070001)

[Build INSIDER C# 7.0で知っておくべき10の新機能（後編）](http://www.buildinsider.net/language/csharplang/070002)
