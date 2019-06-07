using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Common.Core.Interfaces;

namespace TestProject.TaskLibrary.Tasks.Lesson1
{
    public class Task2 : IRunnable
    {
        public void Run()
        {

        }

        public struct Rational : IComparable<Rational>, IEquatable<Rational>
        {
            int gcd;
            public int Nominator { get; set; }
            public int Denominator { get; set; }
            public double quotient;
            public string rationalNumber;
            public string reductedRationalNumber;

            public Rational(int nom, int denom)
            {
                if (denom != 0)
                {
                    Nominator = nom;
                    Denominator = denom;
                    quotient = nom / denom;
                    rationalNumber = nom.ToString() + "/" + denom.ToString();
                    gcd = GetGCD(nom, denom);
                    reductedRationalNumber = (nom / gcd).ToString() + "/" + (denom / gcd).ToString();

                }
                else
                {
                    throw new ArgumentException("The denominator cannot be 0", "denom");
                }

            }
            //GCD
            static int GetGCD(int x, int y)
            {
                return y == 0 ? x : GetGCD(y, x % y);
            }
            //IComparable
            public int CompareTo(Rational other)
            {
                return this.quotient.CompareTo(other.quotient);
            }
            //IEquatable
            public bool Equals(Rational other)
            {
                if (this.quotient == other.quotient)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //overrided ToString()
            public override string ToString()
            {
                if (Denominator == 1)
                {
                    return Nominator.ToString();
                }
                else
                {
                    return this.Nominator + "r" + this.Denominator;
                }
            }
            //Addition
            public static Rational operator + (Rational r1, Rational r2)
            {
                int newNominator = r1.Nominator * r2.Denominator + r2.Nominator * r1.Denominator;
                int newDenominator = r1.Denominator * r2.Denominator;
                return new Rational(newNominator, newDenominator);
            }
            //Subtraction
            public static Rational operator - (Rational r1, Rational r2)
            {
                int newNominator = r1.Nominator * r2.Denominator - r2.Nominator * r1.Denominator;
                int newDenominator = r1.Denominator * r2.Denominator;
                return new Rational(newNominator, newDenominator);
            }
            //Multiplication
            public static Rational operator * (Rational r1, Rational r2)
            {
                int newNominator = r1.Nominator * r2.Denominator * r2.Nominator * r1.Denominator;
                int newDenominator = r1.Denominator * r2.Denominator;
                return new Rational(newNominator, newDenominator);
            }
            //Division
            public static Rational operator / (Rational r1, Rational r2)
            {
                int newNominator = r1.Nominator * r2.Denominator;
                int newDenominator = r1.Denominator * r2.Nominator;
                return new Rational(newNominator, newDenominator);
            }
            //Negation
            public static Rational operator - (Rational r1)
            {
                int newNominator = r1.Nominator * (-1);
                return new Rational(newNominator, r1.Denominator);
            }
            //To treat integers like rationals
            public static Rational ConvertIntToRational(int i)
            {
                return new Rational(i, 1);
            }
        }
    }
}
