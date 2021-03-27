// Copyright (c) Bruno Brant. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable SA1201 // Elements should appear in the correct order
#pragma warning disable SA1649 // File name should match first type name
#pragma warning disable SA1402 // File may only contain a single type

namespace TinyJavaParser
{
	/// <summary>
	/// An expression is a code fragment that produces a value.
	/// </summary>
	public interface IExpression
	{
	}

	/// <summary>
	/// Operators that can be applied to expressions.
	/// </summary>
	public enum PrefixOperator
	{
		/// <summary>
		/// A prefix that indicates an incrementation of the value.
		/// </summary>
		[Description("++")]
		Increment,

		/// <summary>
		/// A prefix that performs an decrementation of the value.
		/// </summary>
		[Description("--")]
		Decrement,
	}

	/// <summary>
	/// This is a type of <see cref="IExpression"/> that is prefixed by a <see cref="PrefixOperator"/> operator.
	/// </summary>
	public class PrefixExpression : IExpression
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PrefixExpression"/> class.
		/// </summary>
		/// <param name="operator">The prefix operator that applies to the <paramref name="expression"/>.</param>
		/// <param name="expression">An expression that is modified by a prefix.</param>
		public PrefixExpression(PrefixOperator @operator, IExpression expression)
		{
			Operator = @operator;
			Expression = expression ?? throw new ArgumentNullException(nameof(expression));
		}

		/// <summary>
		/// Gets the prefix operator of the expression.
		/// </summary>
		public PrefixOperator Operator { get; }

		/// <summary>
		/// Gets the expression over which the operator applies.
		/// </summary>
		public IExpression Expression { get; }
	}

	/// <summary>
	/// Operators that appear between two values.
	/// </summary>
	public enum InfixOperator
	{
		/// <summary>
		/// An logical "or" operation between two booleans.
		/// </summary>
		[Description("||")]
		BooleanOr,

		/// <summary>
		/// A logical "and" operation between two booleans.
		/// </summary>
		[Description("&&")]
		BooleanAnd,

		/// <summary>
		/// A bitwise "or" operation between primitives such as numbers, chars/bytes, etc.
		/// </summary>
		[Description("|")]
		BitwiseOr,

		/// <summary>
		/// A bitwise "exclusive or" operation between primitives such as numbers, chars/bytes, etc.
		/// </summary>
		[Description("^")]
		BitwiseXor,

		/// <summary>
		/// A bitwise "exclusive and" operation between two values.
		/// </summary>
		[Description("&")]
		BitwiseAnd,

		/// <summary>
		/// A logical equality comparision between two values.
		/// </summary>
		[Description("==")]
		BooleanEqual,

		/// <summary>
		/// A logical non-equality comparision between two values.
		/// </summary>
		[Description("!=")]
		BooleanNotEqual,

		/// <summary>
		/// A logical "less than" operation between two values.
		/// </summary>
		[Description("<")]
		LessThan,

		/// <summary>
		/// A logical "greater than" operation between two values.
		/// </summary>
		[Description(">")]
		GreaterThan,

		/// <summary>
		/// A logical "greater than or equal to" operation between two values.
		/// </summary>
		[Description(">=")]
		GreaterOrEqualTo,

		/// <summary>
		/// A bitwise-shift to the left operation on a value.
		/// </summary>
		[Description("<<")]
		BitwiseShiftLeft,

		/// <summary>
		/// A bitwise-shift to the right operation on a value.
		/// </summary>
		[Description(">>")]
		BitwiseShiftRight,

		/// <summary>
		/// An addition between two values.
		/// </summary>
		[Description("+")]
		Addition,

		/// <summary>
		/// A subtraction between two values.
		/// </summary>
		[Description("-")]
		Subtraction,

		/// <summary>
		/// A multiplication between two values.
		/// </summary>
		[Description("*")]
		Multiplication,

		/// <summary>
		/// A division between two values.
		/// </summary>
		[Description("/")]
		Division,

		/// <summary>
		/// A remainder (mod) operation between two values.
		/// </summary>
		[Description("%")]
		Remainder,
	}

	/// <summary>
	/// An <see cref="IExpression"/> that has two sides with an operator between them.
	/// </summary>
	public class InfixExpression : IExpression
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InfixExpression"/> class.
		/// </summary>
		/// <param name="left">The left value of the infix expression.</param>
		/// <param name="operator">The operator that specifies which operation to apply between left and right values.</param>
		/// <param name="right">The right side of the operation.</param>
		public InfixExpression(IExpression left, InfixOperator @operator, IExpression right)
		{
			Left = left ?? throw new ArgumentNullException(nameof(left));
			Operator = @operator;
			Right = right ?? throw new ArgumentNullException(nameof(right));
		}

		/// <summary>
		/// Gets the left value of the infix expression.
		/// </summary>
		public IExpression Left { get; }

		/// <summary>
		/// Gets the operator that specifies which operation to apply between left and right values.
		/// </summary>
		public InfixOperator Operator { get; }

		/// <summary>
		/// Gets the right side of the operation.
		/// </summary>
		public IExpression Right { get; }
	}

	/// <summary>
	/// Operators that can be after an expression.
	/// </summary>
	public enum SuffixOperator
	{
		/// <summary>
		/// A prefix that indicates an incrementation of the value.
		/// </summary>
		[Description("++")]
		Increment,

		/// <summary>
		/// A prefix that performs an decrementation of the value.
		/// </summary>
		[Description("--")]
		Decrement,
	}

	/// <summary>
	/// An expression that has a suffix applied after it.
	/// </summary>
	public class SuffixExpression : IExpression
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SuffixExpression"/> class.
		/// </summary>
		/// <param name="expression">The expression that the suffix operator will be applied to.</param>
		/// <param name="operator">The operator to apply to the expression.</param>
		public SuffixExpression(IExpression expression, SuffixOperator @operator)
		{
			Expression = expression ?? throw new ArgumentNullException(nameof(expression));
			Operator = @operator;
		}

		/// <summary>
		/// Gets the expression that the suffix operator will be applied to.
		/// </summary>
		public IExpression Expression { get; }

		/// <summary>
		/// Gets the operator to apply to the expression.
		/// </summary>
		public SuffixOperator Operator { get; }
	}

	/// <summary>
	/// An expression that initializes an array, only used after assignments.
	/// </summary>
	public class ArrayInitialization : IExpression
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ArrayInitialization"/> class.
		/// </summary>
		/// <param name="literals">The list of literals that initializes the array.</param>
		public ArrayInitialization(List<ILiteral> literals)
		{
			Literals = literals ?? throw new ArgumentNullException(nameof(literals));
		}

		/// <summary>
		/// Gets the list of literals that initializes the array.
		/// </summary>
		public List<ILiteral> Literals { get; }
	}

	/// <summary>
	/// An expression that calls a method.
	/// </summary>
	public class MethodCall : IExpression
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="MethodCall"/> class.
		/// </summary>
		/// <param name="indentifier">
		///     Identifies the method to be called.
		/// </param>
		/// <param name="arguments">
		///     List of expression that are provided as arguments.
		/// </param>
		public MethodCall(ComposedIdentifier indentifier, List<IExpression> arguments)
		{
			Indentifier = indentifier ?? throw new ArgumentNullException(nameof(indentifier));
			Arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
		}

		/// <summary>
		/// Gets identifies the method to be called.
		/// </summary>
		public ComposedIdentifier Indentifier { get; }

		/// <summary>
		/// Gets arguments are valued expressions.
		/// </summary>
		public List<IExpression> Arguments { get; }
	}

	/// <summary>
	/// Expression that is a ternary comparision.
	/// </summary>
	public class TernaryExpression : IExpression
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TernaryExpression"/> class.
		/// </summary>
		/// <param name="condition">
		/// The condition whose result decides whether the final value of
		/// the ternary is true or false.
		/// </param>
		/// <param name="resultWhenTrue">
		/// The value of this expression will be the result of the ternary when the
		/// <see cref="Condition"/> evaluates to <see langword="true"/>.
		/// </param>
		/// <param name="resultWhenFalse">
		/// The value of this expression will be the result of the ternary when the
		/// <see cref="Condition"/> evaluates to <see langword="false"/>.
		/// </param>
		public TernaryExpression(IExpression condition, IExpression resultWhenTrue, IExpression resultWhenFalse)
		{
			Condition = condition ?? throw new ArgumentNullException(nameof(condition));
			ResultWhenTrue = resultWhenTrue ?? throw new ArgumentNullException(nameof(resultWhenTrue));
			ResultWhenFalse = resultWhenFalse ?? throw new ArgumentNullException(nameof(resultWhenFalse));
		}

		/// <summary>
		/// Gets the condition whose result decides whether the final value of
		/// the ternary is true or false.
		/// </summary>
		public IExpression Condition { get; }

		/// <summary>
		/// Gets the value of this expression will be the result of the ternary when the
		/// <see cref="Condition"/> evaluates to <see langword="true"/>.
		/// </summary>
		public IExpression ResultWhenTrue { get; }

		/// <summary>
		/// Gets the value of this expression will be the result of the ternary when the
		/// <see cref="Condition"/> evaluates to <see langword="false"/>.
		/// </summary>
		public IExpression ResultWhenFalse { get; }
	}

	/// <summary>
	/// A expression that casts a value to a type.
	/// </summary>
	public class CastExpression : IExpression
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CastExpression"/> class.
		/// </summary>
		/// <param name="type">
		/// The type that the value of the expression will be cast to.
		/// </param>
		/// <param name="expression">
		/// The expression whose value will be cast to a certain type.
		/// </param>
		public CastExpression(ComposedIdentifier type, IExpression expression)
		{
			Type = type ?? throw new ArgumentNullException(nameof(type));
			Expression = expression ?? throw new ArgumentNullException(nameof(expression));
		}

		/// <summary>
		/// Gets the type that the value of the expression will be cast to.
		/// </summary>
		public ComposedIdentifier Type { get; }

		/// <summary>
		/// Gets the expression whose value will be cast to a certain type.
		/// </summary>
		public IExpression Expression { get; }
	}

	/// <summary>
	/// A expression that creates a new instance of an object.
	/// </summary>
	public class Instancing : IExpression
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Instancing"/> class.
		/// </summary>
		/// <param name="type">The type that will be instantiated.</param>
		/// <param name="arguments">The arguments that will be provided to the type constructor.</param>
		public Instancing(ComposedIdentifier type, List<IExpression> arguments)
		{
			Type = type;
			Arguments = arguments;
		}

		/// <summary>
		/// Gets the type that will be instantiated.
		/// </summary>
		public ComposedIdentifier Type { get; }

		/// <summary>
		/// Gets the arguments that will be provided to the type constructor.
		/// </summary>
		public List<IExpression> Arguments { get; }
	}
}

#pragma warning restore SA1201 // Elements should appear in the correct order
#pragma warning restore SA1649 // File name should match first type name
#pragma warning restore SA1402 // File may only contain a single type
