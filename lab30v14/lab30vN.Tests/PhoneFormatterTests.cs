using System;
using Xunit;
using lab30vN;

namespace lab30vN.Tests
{
    public class PhoneFormatterTests
    {
        private readonly PhoneFormatter _formatter = new PhoneFormatter();

        // ---------- FORMAT TESTS ----------
        //№1
        [Fact]
        public void Format_ValidPhone_ReturnsFormatted()
        {
            var result = _formatter.Format("380991234567");
            Assert.Equal("+380 (99) 123-45-67", result);
        }
        //№2
        [Fact]
        public void Format_WithPlus_ReturnsFormatted()
        {
            var result = _formatter.Format("+380991234567");
            Assert.Equal("+380 (99) 123-45-67", result);
        }
        //№3
        [Fact]
        public void Format_WithDashes_ReturnsFormatted()
        {
            var result = _formatter.Format("380-99-123-45-67");
            Assert.Equal("+380 (99) 123-45-67", result);
        }
        //№4
        [Fact]
        public void Format_WithSpaces_ReturnsFormatted()
        {
            var result = _formatter.Format("380 99 123 45 67");
            Assert.Equal("+380 (99) 123-45-67", result);
        }
        //№5
        [Fact]
        public void Format_InvalidShort_Throws()
        {
            Assert.Throws<ArgumentException>(() => _formatter.Format("123"));
        }
        //№6
        [Fact]
        public void Format_InvalidLong_Throws()
        {
            Assert.Throws<ArgumentException>(() => _formatter.Format("380991234567999"));
        }
        //№7
        [Fact]
        public void Format_Empty_Throws()
        {
            Assert.Throws<ArgumentException>(() => _formatter.Format(""));
        }
        //№8
        [Fact]
        public void Format_Null_Throws()
        {
            Assert.Throws<ArgumentException>(() => _formatter.Format(null));
        }

        // ---------- ISVALID TESTS ----------
        //№9
        [Fact]
        public void IsValid_ValidPhone_ReturnsTrue()
        {
            Assert.True(_formatter.IsValid("380991234567"));
        }
        //№10
        [Fact]
        public void IsValid_WithPlus_ReturnsTrue()
        {
            Assert.True(_formatter.IsValid("+380991234567"));
        }
        //№11
        [Fact]
        public void IsValid_WithSpaces_ReturnsTrue()
        {
            Assert.True(_formatter.IsValid("380 99 123 45 67"));
        }
        //№12
        [Fact]
        public void IsValid_InvalidShort_ReturnsFalse()
        {
            Assert.False(_formatter.IsValid("123"));
        }
        //№13
        [Fact]
        public void IsValid_InvalidLetters_ReturnsFalse()
        {
            Assert.False(_formatter.IsValid("abcdefg"));
        }
        //№14
        [Fact]
        public void IsValid_Empty_ReturnsFalse()
        {
            Assert.False(_formatter.IsValid(""));
        }
        //№15
        [Fact]
        public void IsValid_Null_ReturnsFalse()
        {
            Assert.False(_formatter.IsValid(null));
        }

        // ---------- THEORY TESTS ----------
        //№16-18
        [Theory]
        [InlineData("380991234567", "+380 (99) 123-45-67")]
        [InlineData("+380991234567", "+380 (99) 123-45-67")]
        [InlineData("380-99-123-45-67", "+380 (99) 123-45-67")]
        [InlineData("380 99 123 45 67", "+380 (99) 123-45-67")]
        public void Format_Theory_VariousInputs(string input, string expected)
        {
            var result = _formatter.Format(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("380991234567")]
        [InlineData("+380991234567")]
        [InlineData("380-99-123-45-67")]
        public void IsValid_Theory_ValidPhones(string phone)
        {
            Assert.True(_formatter.IsValid(phone));
        }

        [Theory]
        [InlineData("")]
        [InlineData("123")]
        [InlineData("abc")]
        [InlineData("380123")]
        public void IsValid_Theory_InvalidPhones(string phone)
        {
            Assert.False(_formatter.IsValid(phone));
        }

        // ---------- EDGE CASE ----------
        //№19
        [Fact]
        public void Format_ExtraSymbols_IgnoresAndFormats()
        {
            var result = _formatter.Format("+380(99)1234567");
            Assert.Equal("+380 (99) 123-45-67", result);
        }
    }
}