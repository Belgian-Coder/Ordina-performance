using System;

namespace PerformanceConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            const string s = "Ordina's .NET Performance Workshop";

            while (true)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        StringBuilderPerformanceTester.DetermineStringBuilderPerformanceVsConcat(s, 2);
                        StringBuilderPerformanceTester.DetermineStringBuilderPerformanceVsConcat(s, 5);
                        StringBuilderPerformanceTester.DetermineStringBuilderPerformanceVsConcat(s, 100);
                        StringBuilderPerformanceTester.DetermineStringBuilderPerformanceVsConcat(s, 1000);
                        //NOTE: Performance of adding 2 items is a lot lower than creating multiple items due to object initialization overhead
                        Console.WriteLine();
                        break;
                    case "2":
                        Console.Clear();
                        StringBuilderPerformanceTester.ExecuteSubstringVsAppendOverload(s);
                        Console.WriteLine();
                        break;
                    case "3":
                        Console.Clear();
                        StringBuilderPerformanceTester.CompareDataTypes();
                        Console.WriteLine();
                        break;
                    case "4":
                        Console.Clear();
                        StringBuilderPerformanceTester.DetermineStringBuilderReusagePerformance();
                        Console.WriteLine();
                        break;
                    case "5":
                        Console.Clear();
                        StringBuilderPerformanceTester.ShowStringBuilderConcatenationPerformance();
                        Console.WriteLine();
                        break;
                    case "6":
                        Console.Clear();
                        StringBuilderPerformanceTester.ShowStringBuilderEqualsMistake();
                        Console.WriteLine();
                        break;
                    case "7":
                        Console.Clear();
                        StringPerformanceTester.LoopOverCharacters();
                        Console.WriteLine();
                        break;
                    case "8":
                        Console.Clear();
                        StringPerformanceTester.DemoLookupTableToLower();
                        Console.WriteLine();
                        break;
                    case "9":
                        Console.Clear();
                        BoxingPerformanceTester.ExecuteDemoBoxingVsGenerics();
                        Console.WriteLine();
                        break;
                    case "10":
                        Console.Clear();
                        ListPerformanceTester.Count();
                        Console.WriteLine();
                        break;
                    case "11":
                        Console.Clear();
                        ListPerformanceTester.DemoCustomContains();
                        Console.WriteLine();
                        break;
                    case "12":
                        Console.Clear();
                        ListPerformanceTester.DemoRemoveListVsRemoveDictionary();
                        Console.WriteLine();
                        break;
                    case "13":
                        Console.Clear();
                        DictionaryPerformanceTester.DemoKeyLengthPerformance();
                        Console.WriteLine();
                        break;
                    case "14":
                        Console.Clear();
                        DictionaryPerformanceTester.DemoMemoization();
                        Console.WriteLine();
                        break;
                    case "15":
                        Console.Clear();
                        DictionaryPerformanceTester.DemoTryGetValueVsContainsKey();
                        Console.WriteLine();
                        break;
                    case "16":
                        Console.Clear();
                        ArrayPerformanceTester.DemoArrayFlattening();
                        Console.WriteLine();
                        break;
                    case "17":
                        Console.Clear();
                        ArrayPerformanceTester.DemoJaggedArray();
                        Console.WriteLine();
                        break;
                    case "18":
                        Console.Clear();
                        ArrayPerformanceTester.DemoJaggedTemporalLocality();
                        Console.WriteLine();
                        break;
                    case "19":
                        Console.Clear();
                        MethodPerformanceTester.DemoOutVsReturn();
                        Console.WriteLine();
                        break;
                    case "20":
                        Console.Clear();
                        MethodPerformanceTester.DemoNestedVsSequential();
                        Console.WriteLine();
                        break;
                    case "21":
                        Console.Clear();
                        MethodPerformanceTester.DemoRecursiveInline();
                        Console.WriteLine();
                        break;
                    case "22":
                        Console.Clear();
                        MethodPerformanceTester.DemoMultipleReturnValues();
                        Console.WriteLine();
                        break;
                    case "23":
                        Console.Clear();
                        MethodPerformanceTester.DemoStaticFieldVsLocalVariable();
                        Console.WriteLine();
                        break;
                    case "24":
                        Console.Clear();
                        MethodPerformanceTester.DemoInlining();
                        Console.WriteLine();
                        break;
                    case "25":
                        Console.Clear();
                        ClassStructPerformanceTester.DemoClassVsStruct();
                        Console.WriteLine();
                        break;
                    case "26":
                        Console.Clear();
                        ExceptionPerformanceTester.DemoTryCatchInnerLoop();
                        Console.WriteLine();
                        break;
                    case "27":
                        Console.Clear();
                        BranchPerformanceTester.DemoCommonSenseIfOrdering();
                        Console.WriteLine();
                        break;
                    case "28":
                        Console.Clear();
                        BranchPerformanceTester.DemoLoopIncrementVsDecrement();
                        Console.WriteLine();
                        break;
                    case "29":
                        Console.Clear();
                        BranchPerformanceTester.DemoForVsForEach();
                        Console.WriteLine();
                        break;
                    case "30":
                        Console.Clear();
                        BranchPerformanceTester.DemoIfVsSwitch();
                        Console.WriteLine();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
