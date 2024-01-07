using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Maciko84.ObjectArithmetic
{
    /// <summary>
    /// Represents a math operation
    /// </summary>
    public class Operation : IComparable<Operation>
    {
        #region Constructors
        /***************
        | Constructors |
        ***************/
        /// <summary>
        /// Creates a Operation object from string with expression 
        /// e. g. 1 + 2, 2.1 * 2.3 etc.
        /// </summary>
        /// <param name="expression">Expression to parse</param>
        /// <exception cref="ArgumentException"></exception>
        /// <example>
        /// Example of use:
        /// <code>
        /// var operation = new Operation("1 + 2");
        /// Console.WriteLine(operation.Result); // print 3
        /// </code>
        /// </example>
        public Operation(string expression)
        {
            var parts = expression.Split(' ', '\t');
            if (parts.Length != 3)
            {
                throw new ArgumentException("Expression must be in the format: [number] [operator] [number]");
            }

            if (!double.TryParse(parts[0], out double a) || !double.TryParse(parts[2], out double b))
            {
                throw new ArgumentException("Both numbers must be valid doubles");
            }

            this.a = a;
            this.b = b;

            switch (parts[1])
            {
                case "+":
                    Mode = OperationMode.Addition;
                    break;
                case "-":
                    Mode = OperationMode.Subtraction;
                    break;
                case "*":
                    Mode = OperationMode.Multiplication;
                    break;
                case "/":
                    if (b == 0)
                    {
                        throw new ArgumentException("Cannot divide by zero");
                    }
                    Mode = OperationMode.Division;
                    break;
                case "%":
                    Mode = OperationMode.Modulo;
                    break;
                default:
                    throw new ArgumentException($"Invalid operation: {parts[1]}");
            }
        }

        /// <summary>
        /// Construct a Operation class object from digits and operator
        /// </summary>
        /// <param name="a">first digit</param>
        /// <param name="mode">mode of operation</param>
        /// <param name="b">second digit</param>
        /// <example>
        /// Example of use:
        /// <code>
        /// var operation = new Operation(1, OperationMode.Addition, 2);
        /// Console.WriteLine(operation.Result); // print 3
        /// </code>
        /// </example>
        public Operation(double a, OperationMode mode, double b)
        {
            this.a = a;
            Mode = mode;
            this.b = b;
        }
        /// <summary>
        /// Constructs a new, empty Operation class object.
        /// </summary>
        public Operation()
        {

        }
        #endregion


        #region Properties
        /*********************
        |     Properties     |
        *********************/

        /// <summary>
        /// First digit of expression
        /// </summary>
        public double a { get; init; }
        /// <summary>
        /// Second digit of expression.
        /// </summary>
        public double b { get; init; }
        /// <summary>
        /// Operation mode.
        /// </summary>
        public OperationMode Mode { get; init; }
        /// <summary>
        /// Symbol of the operation
        /// </summary>
        public char OperationSymbol
        {
            get =>
                Mode switch
                {
                    OperationMode.Addition => '+',
                    OperationMode.Subtraction => '-',
                    OperationMode.Multiplication => '*',
                    OperationMode.Division => '/',
                    OperationMode.Modulo => '%',
                    _ => '?',
                };
        }

        
        /// <summary>
        /// Result of expression
        /// </summary>
        public double Result
        {
            get
            {
                switch (Mode)
                {
                    case OperationMode.Addition:
                        return a + b;
                    case OperationMode.Subtraction:
                        return a - b;
                    case OperationMode.Division:
                        return a / b;
                    case OperationMode.Multiplication:
                        return a * b;
                    case OperationMode.Modulo:
                        return a % b;
                    default:
                        return double.NaN;
                }
            }
        }
        #endregion



        #region Default Methods
        /******************
        | Default Methods |
        ******************/

        public override string ToString()
        {
            return $"{a} {OperationSymbol} {b} = {Result}";
        }




        public override bool Equals(object? obj)
        {
            if (obj is Operation other)
            {
                return a == other.a && b == other.b && Mode == other.Mode;
            }

            return false;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + a.GetHashCode();
            hash = hash * 23 + b.GetHashCode();
            hash = hash * 23 + Mode.GetHashCode();
            return hash;
        }
        #endregion

        #region IComparable
        /******************
        |   IComparable   |
        ******************/

        public int CompareTo(Operation? other)
        {
            if (other == null) return 1;

            return Result.CompareTo(other.Result);
        }
        #endregion
    }
}