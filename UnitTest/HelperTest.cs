using Microsoft.VisualStudio.TestTools.UnitTesting;
using App;
using System.Collections.Generic;

/*
 Тестовый проект "отзеркаливает" основной проект, его классы наз. от имени классов проекта, добавляя "Test",
 также само и наз. методов.
*/

namespace UnitTest
{
    [TestClass]
    public class HelperTest
    {
        [TestMethod]
        public void EllipsisTest()
        {
            Helper helper = new();
            Assert.IsNotNull(helper, "new Helper() should not be null");
            Assert.AreEqual(
                "He...",  // ожидается
                helper.Ellipsis("Hello, World!", 5)  // что получиться
            );
            Assert.AreEqual(
                "Hel...",  // ожидается
                helper.Ellipsis("Hello, World!", 6)  // что получиться
            );
            Assert.AreEqual(
                "Test...",  // ожидается
                helper.Ellipsis("Test String", 7)  // что получиться
            );
        }

        [TestMethod]
        public void FinalizeTest()
        {
            Helper helper = new();
            Assert.IsNotNull(helper, "new Helper() should not be null");

            Assert.AreEqual(
                "Hello, Daniil.",
                helper.Finalize("Hello, Daniil")
            );
            Assert.AreEqual(
                "Hello, friend.",
                helper.Finalize("Hello, friend.")
            );
            Assert.AreEqual(
                "Hello, dir friend!",
                helper.Finalize("Hello, dir friend!")
            );
            Assert.AreEqual(
                "How are you?",
                helper.Finalize("How are you?")
            );
            Assert.AreEqual(
                "Hello, hello, hello.",
                helper.Finalize("Hello, hello, hello")
            );
        }

        [TestMethod]
        public void CombineUrlTest()
        {
            Helper helper = new();
            Dictionary< string[], string> testCases = new()
            {
                { new[] { "/home", "index" }, "/home/index" },
                { new[] { "/shop", "/cart" }, "/shop/cart" },
                { new[] { "auth/", "logout" }, "/auth/logout" },
                { new[] { "forum/", "topic/" }, "/forum/topic" },
                { new[] { "/home///", "index" }, "/home/index" },
                { new[] { "///home/", "/index" }, "/home/index" },
                { new[] { "home/", "////index" }, "/home/index" },
                { new[] { "///home/////", "////index///" }, "/home/index" },
            };
            foreach (var testCase in testCases)
            {
                Assert.AreEqual(
                    testCase.Value,
                    helper.CombineUrl(testCase.Key[0], testCase.Key[1]),
                    $"{testCase.Key[0]} - {testCase.Key[1]}"
                );
            }
        }
        [TestMethod]
        public void CombineUrlMultiTest()
        {
            Helper helper = new();
            Dictionary<string[], string> testCases = new()
            {
                { new[] { "/home", "/index", "/gmail" }, "/home/index/gmail" },
                { new[] { "/shop", "/cart/", "com" }, "/shop/cart/com" },
                { new[] { "auth/", "logout" }, "/auth/logout" },
                { new[] { "forum", "topic/", "/com/" }, "/forum/topic/com" },
                { new[] { "//forum////", "topic////", "///com" }, "/forum/topic/com" },
                { new[] { "forum", "topic", "com" }, "/forum/topic/com" },
                { new[] { "/forum/", "/topic///////////", "//com////////////////" }, "/forum/topic/com" },
                { new[] { "/shop", "/cart", "/user", "..", "/123" }, "/shop/cart/123" },
                { new[] { "/shop///", "///cart", "user", "..", "////123///" }, "/shop/cart/123" },
                { new[] { "/shop///", "///cart", "user", "..", "////123///", "456" }, "/shop/cart/123/456" },
                { new[] { "/shop///", "///cart", "..", "user//", "///123", "456//" }, "/shop/user/123/456" },
            };
            foreach (var testCase in testCases)
            {
                Assert.AreEqual(
                    testCase.Value,
                    helper.CombineUrl(testCase.Key),
                    $"{testCase.Key[0]} + {testCase.Key[1]}"
                );
            }
        }
    }
}