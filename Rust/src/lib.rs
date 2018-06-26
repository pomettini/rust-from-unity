#[no_mangle]
pub extern fn rust_fibonacci(n: i32) -> u64 
{
    let mut i = 0;
    let mut sum = 0;
    let mut last = 0;
    let mut curr = 1;

    while i < n - 1
    {
        sum = last + curr;
        last = curr;
        curr = sum;
        i += 1;
    }
    
    sum
}

#[test]
fn test_fibonacci_green()
{
    assert_eq!(rust_fibonacci(10), 55);
}

#[test]
#[should_panic]
fn test_fibonacci_red()
{
    assert_eq!(rust_fibonacci(10), 54);
}

#[test]
fn test_fibonacci_overflow_green()
{
    assert_eq!(rust_fibonacci(90), 2880067194370816120);
}

#[test]
#[should_panic]
fn test_fibonacci_overflow_red()
{
    assert_eq!(rust_fibonacci(100), 0);
}