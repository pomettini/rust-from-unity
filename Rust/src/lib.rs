extern crate libc;

use std::ffi::{CString, CStr};
use libc::{c_char, uint32_t};

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

// Credits to: http://jakegoulding.com/rust-ffi-omnibus/

#[no_mangle]
pub extern fn rust_count_characters(s: *const c_char) -> uint32_t 
{
    let c_str = unsafe 
    {
        assert!(!s.is_null());
        CStr::from_ptr(s)
    };

    let r_str = c_str.to_str().unwrap();
    r_str.chars().count() as uint32_t
}

#[no_mangle]
pub extern fn rust_reverse_string(s: *const c_char) -> *mut c_char 
{
    unsafe 
    {
        assert!(!s.is_null());
        let r_string = CStr::from_ptr(s).to_string_lossy().into_owned();
        let r_string_reversed = r_string.chars().rev().collect::<String>();
        let c_string_reversed = CString::new(r_string_reversed).unwrap();
        c_string_reversed.into_raw()
    }
}

#[no_mangle]
pub extern fn rust_reverse_string_free(s: *mut c_char) 
{
    unsafe 
    {
        if s.is_null() { return }
        CString::from_raw(s)
    };
}

// Fibonacci Tests

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

// Count Chars Tests

#[test]
fn test_count_chars_green()
{
    let s = CString::new("Gatto").unwrap();
    assert_eq!(rust_count_characters(s.as_ptr()), 5);
}

#[test]
#[should_panic]
fn test_count_chars_red()
{
    let s = CString::new("Camaleonte").unwrap();
    assert_eq!(rust_count_characters(s.as_ptr()), 5);
}

// Reverse String Tests

// #[test]
// fn test_reverse_string_green()
// {
//     let input = CString::new("Ciao").unwrap();
//     assert_eq!(rust_reverse_string(input.as_ptr()), 0);
// }