```ebnf
(* A Java compilation unit that can contain several different Java structures *)
JAVA_FILE = PACKAGE IMPORT_LIST CLASS_DECLARATIONS;

(* Declares which package the declarations in the current compilation unit belongs to *)
PACKAGE = "package" PACKAGE_NAME ";";

(* A kind of Identifer that specifies the package name *)
PACKAGE_NAME = IDENTIFIER { "." IDENTIFIER };

(* Valid character string that can be used to identify a declared structure in Java *)
IDENTIFIER = (LETTER | "_") { LETTER | DIGIT | "_" };

(* List of import statements *)
IMPORT_LIST = IMPORT { IMPORT };

(* Brings into the compilation unit's scope all declarations that belong to a package *)
IMPORT = "import" PACKAGE_NAME ";";

(* Declares and defines a Java class *)
CLASS_DECLARATION = [ ANNOTATION ] VISIBILITY "class" IDENTIFIER { "extends" IDENTIFIER } CLASS_BODY;

(* adds metadata to the following structure in code *)
ANNOTATION = "@" IDENTIFIER  "(" [ARGUMENT_LIST] ")";

(* List of arguments/parameters in method calls *)
ARGUMENT_LIST = ARGUMENT { "," ARGUMENT };

ARGUMENT = LITERAL;

LITERAL = INTEGER_LITERAL | STRING;

INTEGER_LITERAL = ["+" | "-"] DIGIT { DIGIT };

VISIBILITY = "public" | "protected" | "private";

FIELD_DECLARATION = [ ANNOTATION ] VISIBILITY [ "static" ] [ "final" ] IDENTIFIER [ "[]", ] ["=" EXPRESSION] ";";

EXPRESSION = LITERAL						(* basic case *)
	| PREFIX { EXPRESSION }					(* prefixing like ++, +, or even new expr. *)
	| { EXPRESSION } INFIX { EXPRESSION }
	| { EXPRESSION } SUFFIX
	| ARRAY_INITIALIZATION
	| METHOD_CALL
	| TERNARY_EXPRESSION
	| CAST_EXPRESSION;
	| INSTANCING

LITERAL = (* Ommited, we've seen this before *);

PREFIX = "++" | "--" | "!" | "~" | "+" | "-";

INFIX = "||" | "&&" | "|" | "^" | "&" | "==" | "!=" 
	| "<" | ">"	| "<=" | ">=" | "<<" | ">>" | ">>>" 
	| "+" | "-" | "*" | "/" | "%";

SUFFIX = "++" | "--";

ARRAY_INITIALIZATION = "{", [{ LITERAL , }] "}"; 		(* this is not exaustive *)

METHOD_CALL = LITERAL, { ".", LITERAL }, "(", ARGUMENT_LIST, ")";			(* again, not exaustive *)

TERNARY_EXPRESSION = EXPRESSION, "?", EXPRESSION, ":", EXPRESSION;

CAST_EXPRESSION = "(", IDENTIFIER, ")", EXPRESSION;

INSTANCING = "new" IDENTIFIER "(" [ARGUMENT_LIST] ")";
```
