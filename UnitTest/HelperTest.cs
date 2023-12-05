using Microsoft.VisualStudio.TestTools.UnitTesting;
using App;
using System.Collections.Generic;
using System;

/*
 �������� ������ "�������������" �������� ������, ��� ������ ���. �� ����� ������� �������, �������� "Test",
 ����� ���� � ���. �������.
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
            Assert.IsNotNull(helper, "new Helper() should not be null");  // �������� �� null
            Assert.AreEqual(
                "He...",  // ���������
                helper.Ellipsis("Hello, World!", 5)  // ��� ����������
            );
            Assert.AreEqual(
                "Hel...",  // ���������
                helper.Ellipsis("Hello, World!", 6)  // ��� ����������
            );
            Assert.AreEqual(
                "Test...",  // ���������
                helper.Ellipsis("Test String", 7)  // ��� ����������
            );
        }

        [TestMethod]
        public void EllipsisExceptionTest()
        {
            // ������������ ���������� ����� ��� ������������:
            //   - ��������� ���������� � ���� ��������� ������� ���������� �������� �����.
            //     ���������������, ���������������� ����� ������� � ����������� ������������ ������������.
            //     ! ������ "��������������" � ������.
            //   - �������� ���� ���������� ���������� � "�������" ���������.
            //     �.�. ����� ���� "Exception" �� �������������, ���� �������� ���������� ������� ����.
            //     (����, ���� ��� ��� - ��������� Exception)
            //   - ���� ����������, ������� �������� � ������, ������������ � Assert, ��� ��������� ��������
            //     ��������(�����) �� ��� ���������� ��� ���������.
            Helper helper = new();
            var ex = Assert.ThrowsException<ArgumentNullException>(  // �������� ���������� �� �������� null
                         () => helper.Ellipsis(null!, 1)
                     );
            // ������ - ������ � ����� �������, �������� ��������� ����� � ��������� �������.
            // �������� () - �������, ������ ���� ���� ��� �-���

            Assert.IsTrue(ex.Message.Contains("input"));  // �������� �� ����� ������

            var ex2 = Assert.ThrowsException<ArgumentException>(  // �������� ���������� �� �������� ���� ���-�� ��������
                         () => helper.Ellipsis("Hello, world", 1)
                     );
            Assert.IsTrue(ex2.Message.Contains("len"));

            var ex3 = Assert.ThrowsException<ArgumentOutOfRangeException>(  // �������� ���������� �� �������� ���� ���-�� ��������
                         () => helper.Ellipsis("Hello, world", 100)
                     );
            Assert.IsTrue(ex3.Message.Contains("len"));
        }

        [TestMethod]
        public void FinalizeTest()
        {
            Helper helper = new();
            Assert.IsNotNull(helper, "new Helper() should not be null");  // �������� �� null

            // �������� �� ���������, ����� ������ ������������� �� ����������� ����
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

            // �������, ��� ���������� �������� ������
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
                    testCase.Value,  // �������
                    helper.CombineUrl(testCase.Key[0], testCase.Key[1]),  // ���������
                    $"{testCase.Key[0]} - {testCase.Key[1]}"  // ����� � ������ ������
                );
            }
        }

        [TestMethod]
        public void CombineUrlExceptionTest()
        {
            Helper helper = new();

            // �������� �� �������� null � �����
            Assert.AreEqual("/home", helper.CombineUrl("/home", null!));
            Assert.AreEqual("/home/path", helper.CombineUrl("/home", "///path//", null!));
            Assert.AreEqual("/home/user", helper.CombineUrl("/home", "///path//", "..", "user//", null!));

            // �������� �� ��� �������� null
            var ex = Assert.ThrowsException<ArgumentException>(
                () => helper.CombineUrl(null!, null!)
            );
            Assert.AreEqual("All arguments are null", ex.Message);

            ex = Assert.ThrowsException<ArgumentException>(
                () => helper.CombineUrl(null!, null!, null!, null!, null!, null!)
            );
            Assert.AreEqual("All arguments are null", ex.Message);

            ex = Assert.ThrowsException<ArgumentException>(
                () => helper.CombineUrl()
            );
            Assert.AreEqual("Parts is empty", ex.Message);

            var ex2 = Assert.ThrowsException<NullReferenceException>(
                () => helper.CombineUrl(null!)
            );
            Assert.AreEqual("Parts is null", ex2.Message);

            // �������� �� �������� null � ������ ��� ��������
            var ex3 = Assert.ThrowsException<ArgumentException>(
                () => helper.CombineUrl(null!, "/subsection")
            );
            Assert.AreEqual("Non-Null argument after Null one", ex3.Message);

            ex3 = Assert.ThrowsException<ArgumentException>(
                () => helper.CombineUrl("/path", null!, "/subsection")
            );
            Assert.AreEqual("Non-Null argument after Null one", ex3.Message);

            ex3 = Assert.ThrowsException<ArgumentException>(
                () => helper.CombineUrl("/path", "/path2", null!, "/subsection")
            );
            Assert.AreEqual("Non-Null argument after Null one", ex3.Message);
        }

        [TestMethod]
        public void CombineUrlMultiTest()
        {
            Helper helper = new();

            // �������, ��� ���������� �������� ������
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
                    testCase.Value,  // �������
                    helper.CombineUrl(testCase.Key),  // ���������
                    $"{testCase.Key[0]} + {testCase.Key[1]}"  // ����� � ������ ������
                );
            }
        }
    }
}