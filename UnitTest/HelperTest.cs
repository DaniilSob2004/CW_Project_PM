using Microsoft.VisualStudio.TestTools.UnitTesting;
using App;

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
    }
}