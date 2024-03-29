﻿namespace NumbersExtension
{
    using System;

    /// <summary>
    /// An application entry point.
    /// </summary>
    public static class NumbersExtension
    {
        /// <summary>Inserts the number into another.</summary>
        /// <param name="numberSource">The number source.</param>
        /// <param name="numberIn">The number in.</param>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <returns>numberSource with inserted numberIn.</returns>
        /// <exception cref="ArgumentOutOfRangeException">i - i is less than zero
        /// or
        /// j - j is less than zero
        /// or
        /// i - i is higher than number of bits in int
        /// or
        /// j - j is higher than number of bits in int.</exception>
        /// <exception cref="ArgumentException">i is higher than j.</exception>
        public static int InsertNumberIntoAnother(int numberSource, int numberIn, int i, int j)
        {
            const int intMaxBit = 31;
            const int intMinBit = 0;

            if (i < intMinBit || i > intMaxBit)
            {
                throw new ArgumentOutOfRangeException(nameof(i), "i is out of range");
            }

            if (j < intMinBit || j > intMaxBit)
            {
                throw new ArgumentOutOfRangeException(nameof(j), "j is out of range");
            }

            if (i > j)
            {
                throw new ArgumentException("i is higher than j");
            }

            if (i == intMinBit && j == intMaxBit)
            {
                return numberIn;
            }

            if (j == intMaxBit)
            {
                numberSource &= int.MaxValue;
            }

            int bitMaskForNumberIn, bitMaskForNumberSource;
            bitMaskForNumberIn = ~(int.MaxValue << (j + 1 - i)) & int.MaxValue;
            int insertValue = (numberIn & bitMaskForNumberIn) << i;
            if (numberSource >= 0)
            {
                bitMaskForNumberSource = (int.MaxValue >> (intMaxBit - i)) | ((int.MaxValue << (j + 1)) & int.MaxValue);
            }
            else
            {
                bitMaskForNumberSource = (int.MaxValue >> (intMaxBit - i)) | (((int.MaxValue << (j + 1)) & int.MaxValue) | (int.MaxValue << (intMaxBit - 1)));
            }

            int putOutValue = numberSource & bitMaskForNumberSource;
            putOutValue |= insertValue;
            return putOutValue;
        }

        /// <summary>Determines whether the specified value is palindrome.</summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is palindrome; otherwise, <c>false</c>.</returns>
        public static bool IsPalindrome(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Value cannot be less than zero");
            }

            int valueCopy = value;
            int decimalPlaces = 1;
            int tenDivider;
            while (valueCopy / 10 != 0)
            {
                valueCopy /= 10;
                decimalPlaces++;
            }

            if (decimalPlaces == 1)
            {
                return true;
            }

            if (decimalPlaces == 2)
            {
                if (value / 10 == value % 10)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            tenDivider = (int)Math.Pow(10, decimalPlaces - 1);
            if (value / tenDivider == value % 10)
            {
                if ((value % tenDivider) / (tenDivider / 10) == 0)
                {
                    if ((value / 10) % 10 == 0)
                    {
                        return IsPalindrome(((value % tenDivider) / 10) + (tenDivider / 100) + 1);
                    }
                    else
                    {
                        return false;
                    }
                }

                return IsPalindrome((value % tenDivider) / 10);
            }
            else
            {
                return false;
            }
        }
    }
}