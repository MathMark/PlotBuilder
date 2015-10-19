# Smart Builder
![](https://pp.vk.me/c628323/v628323649/1e19d/Nu03aCp-PkU.jpg)

<html>
<body>
<h1>About "Smart Builder"</h1>

<dl>
<dt>Smart Builder</dt>
<dd>Is program, which allows you to create a mathematical graphs. Program's abilities:
<ul>
<li>Build two different representations of functions (explicit and parametric)</li>
<li>Build many functions on one sheet and scale them.</li>
<li>Build functions with different colors and dash styles</li>
etc.
</ul>
</dd>
</dl>
<h1>Units of program</h1>
<p>Program includes two main units:</p>
<ol>
<li>
<dl>
<dt>Formula's Parser</dt>
<dd>Allows user to enter a lot of different formulas due to built-in keywords, which denote specific symbols, for example:
<ul>
<li>&radic; x &rarr; sqrt(x)</li>
<li>|x| &rarr; abs(x)</li>
</ul>
<p>Can read many mathematical operations, such as: <b>+</b>, <b>-</b>, <b>/</b>, <b>*</b>; trigonometry functions: <i>sin(x)</i>, <i>cos(x)</i>, <i>tan(x)</i> etc; hyperbolical functions: <i>sinh(x)</i>, <i>cosh(x)</i> etc; mathematical constants: <b>&pi;</b> and <b>e</b>.</p>
<p>Parser uses <i>Reverse Polish Nonation</i> (RPN) for reading formula. This notation very comfortable for writing code, which can analyze any formulas, so I has written two methods in this program:
<ul>
<li><b>ConvertToRPN</b></li> - converts usual formula to RPN-formula;
<li><b>Solve</b></li> - reads RPN-formula and expose digits in formula;
</ul>
Then comes into effect another unit - <b>Graphic unit</b>
</p>
</dd>
</dl>
</li>
<li>
<dl>
<dt>Graphic unit</dt>
<dd>This unit immediately builds your function on sheet. It includes two methods:
<ul>
<li><b>DrawGraphic</b></li> - for usual functions;
<li><b>Overloaded DrawGrapic</b></li> - for parametric functions;
</ul>
</dd>
</dl>
</li>
</ol>
</body>
</html>
