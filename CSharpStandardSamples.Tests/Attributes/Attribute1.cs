using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace CSharpStandardSamples.Tests.Attributes
{
    /// <summary>作者情報を残すための属性</summary>
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method,
        AllowMultiple = false, Inherited = false)]
    class AuthorAttribute : Attribute
    {
        public string Name { get; }
        public AuthorAttribute(string name) { Name = name; }

        /// <summary>クラスやメソッドの作者情報を取得</summary>
        public static string GetAuthor(MemberInfo info)
        {
            var attributes = GetCustomAttributes(info, typeof(AuthorAttribute));
            var author = attributes.OfType<AuthorAttribute>().FirstOrDefault();
            return author?.Name ?? "";
        }
    }

    public class Attribute1
    {
        [Author("Joestar")]
        private class AuthorTest
        {
            [Author("Jonathan")]
            public static void Method1() { }
            [Author("Joseph")]
            public static void Method2() { }
        }

        [Fact]
        public void GetAuthorAttribute()
        {
            var type = typeof(AuthorTest);
            AuthorAttribute.GetAuthor(type).Should().Be("Joestar");

            var method1 = type.GetMethod("Method1");
            AuthorAttribute.GetAuthor(method1).Should().Be("Jonathan");

            var method2 = type.GetMethod("Method2");
            AuthorAttribute.GetAuthor(method2).Should().Be("Joseph");
        }

    }
}
