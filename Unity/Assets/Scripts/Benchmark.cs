using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

public class Benchmark : MonoBehaviour 
{
	[DllImport("call_rust")]
	private static extern UInt64 rust_fibonacci (int n);
	
	private static UInt64 CSharpFibonacci (int n)
	{
		int i = 0;
		UInt64 sum = 0;
		UInt64 last = 0;
		UInt64 curr = 1;

		while (i < n - 1)
		{
			sum = last + curr;
			last = curr;
			curr = sum;
			i += 1;
		}

		return sum;
	}

	void Start () 
	{
		// Init stopwatches for benchmarking
		Stopwatch rust_clock = new Stopwatch();
		Stopwatch csharp_clock = new Stopwatch();

		// Run Rust implementation
		rust_clock.Start();
		UInt64[] rust_seq = new UInt64[90];
		for (int i = 0; i < 90; i++)
		{
			rust_seq[i] = rust_fibonacci(90);
		}
		rust_clock.Stop();

		// Run CSharp implementation
		csharp_clock.Start();
		UInt64[] csharp_seq = new UInt64[90];
		for (int i = 0; i < 90; i++)
		{
			csharp_seq[i] = CSharpFibonacci(90);
		}
		csharp_clock.Stop();

		// Rust Test: if first and last element in the index are incorrect throws exception
		if (rust_seq[0] != 0 && rust_seq[89] != 2880067194370816120)
			throw new Exception("Invalid Rust result");

		// CSharp Test: if first and last element in the index are incorrect throws exception
		if (csharp_seq[0] != 0 && csharp_seq[89] != 2880067194370816120)
			throw new Exception("Invalid CSharp result");

		// Return benchmark result
		UnityEngine.Debug.Log("Rust benchmark: " + rust_clock.Elapsed.TotalMilliseconds);
		UnityEngine.Debug.Log("CSharp benchmark: " + csharp_clock.Elapsed.TotalMilliseconds);
	}
}
