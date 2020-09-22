// 本プロジェクト内のinternalクラスを参照できるプロジェクトを指定する
//   1. AssemblyInfo.csファイルを追加して以下を書く
//   2. 参照したいプロジェクト側から本プロジェクトを参照する
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("CSharpStandardSamples.Tests")]
[assembly: InternalsVisibleTo("CSharpStandardSamples.Benchmarks")]
