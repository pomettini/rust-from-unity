using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using UnityEngine.TestTools;
using NUnit.Framework;

public class Example : MonoBehaviour 
{
	[DllImport("call_rust")]
	private static extern UInt64 rust_fibonacci(int n);

	[DllImport("call_rust")]
	private static extern uint rust_count_characters(string s);

	// Warning: this causes garbage atm, I have to implement free
	[DllImport("call_rust")]
	private static extern string rust_reverse_string(string s);

	[Test]
	public void Test_Fibonacci_Green()
	{
		UInt64 fibonacci = rust_fibonacci(10);
		Assert.AreEqual(fibonacci, 55);
	}

	[Test]
	public void Test_Fibonacci_Red()
	{
		UInt64 fibonacci = rust_fibonacci(11);
		Assert.AreNotEqual(fibonacci, 55);
	}

	[Test]
	public void Test_Count_Chars_Green()
	{
		Assert.AreEqual(rust_count_characters("Cane"), 4);
	}

	[Test]
	public void Test_Count_Chars_Red()
	{
		Assert.AreNotEqual(rust_count_characters("Gatto"), 4);
	}

	[Test]
	public void Test_Reverse_String_Green()
	{
		string s = rust_reverse_string("Ciao");
		Assert.AreEqual(s, "oaiC");
	}

	[Test]
	public void Test_Reverse_String_Red()
	{
		string s = rust_reverse_string("Miao");
		Assert.AreNotEqual(s, "Ciao");
	}
}
