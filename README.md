# Smart Builder
![Screenshot](https://pp.vk.me/c628324/v628324649/224ee/x9U4wMQ0K-c.jpg)

## Features

- Create a mathematical graphs
- Build two different representations of functions (explicit and parametric)
- Build many functions on one sheet and scale them
- Build functions with different colors and dash styles

## Units of program

Program includes two main units:

- Formula's Parser

  Allows user to enter a lot of different formulas due to built-in keywords, which denote specific symbols, for example:
  - &radic; x &rarr; sqrt(x)
  - |x| &rarr; abs(x)
  
 Can read many mathematical operations, such as: `+, -, /, *`; trigonometry functions: `sin(x), cos(x), tan(x)` etc; hyperbolical functions: `sinh(x), cosh(x)` etc; mathematical constants: &pi; and exponenta.
 Parser uses *Reverse Polish Nonation* (RPN) for reading formula. This notation very comfortable for writing code, which can analyze any formulas, so I has written two methods in this program:
  - **ConvertToRPN** - converts usual formula to RPN-formula;
  - **Solve** - reads RPN-formula and expose digits in formula;
- Graphic unit

 This unit immediately builds your function on sheet. It includes two methods:
 - **DrawGraphic** - for usual functions;
 - **Overloaded DrawGraphic** - for parametric functions;

## Chart of functions

| Mathematical symbol or name of function | Keyword (or symbol) | Description |
|-----------------------------------------|---------------------|-------------|
| \|x\| | abs(x) | The absolute value of x | &radic;x | sqrt(x) | The square root of x |
| sign x | sign(x) | **The function "signum" of x.** Returns 1 if x greatest or equal 1 Returns 0 if x equal 0 Returns -1 if x less or equal -1 |
| log<sub>a</sub> x | log(a;x) | Returns the logarithm of x, which has base number a |
| ln x | ln(x) | Returns the natural logarithm, which has base number e |
| lg x | lg(x) | Returns the logarithm, which has base number 10 |
| [x] | E(x) | Returns the integer part of x |
| {x} | R(x) | Returns the fractional part of x |
| x<sup>y</sup> | x^y | Returns the x powered by y |
| <sup>n</sup>&radic;x | x^(1/n) | The n<sup>th</sup> root of x |
