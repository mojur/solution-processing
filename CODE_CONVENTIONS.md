Code Conventions
================================================================================

Code consistency helps a codebase look uniform and familiar to those who work
with it no matter who contributed. As a rule of thumb, this repository generally
sticks to the coding conventions within [Microsoft's C# Coding Conventions], but
this document exists to further detail what code conventions are expected.

Many of the nuanced conventions stated here cannot be consistently enforced and
therefore are **not required** to pass code review.

You can save yourself some time and headache by using the
[`.editorconfig`](https://editorconfig.org/) file in the root folder to
automate formatting and enforce consistency.

* [Rider EditorConfig Settings]
* [Visual Studio EditorConfig Settings]
* [Visual Studio Code EditorConfig Extension]

> Many of the below examples attempt to stretch the limits of these conventions
> to demonstrate useful scenarios. In doing so, many are ridiculous, overly
> complex, and should be broken down into smaller parts, renamed, etc.

Table of Contents
--------------------------------------------------------------------------------

* [Naming](#naming)
* [Spacing](#spacing)
* [Line Wrapping](#line-wrapping)
* [File Structure](#file-structure)
* [Comments](#comments)
* [Verbosity](#verbosity)

Naming
--------------------------------------------------------------------------------

* In general, use [Microsoft's .NET Naming Guidelines] to maintain a consistent
 naming scheme.
* Ensure variable names are **descriptive** and infer clear **purpose**
  * **NO** single letter variables (use short abbreviations instead)

| `PascalCasing` | `camelCasing` | `_camelCasing` |
| -------------- | ------------- | -------------- |
| All methods    | Parameters    | `protected` & `private`, `static` fields, properties, & events
| All constants  | Local variables
| All type names | `protected` & `private` fields, properties, & events
| `public` fields, properties, & events

### Prefixes

* `this.` prefix for all nonstatic member access
* 'I' prefix for interfaces
* 'T' prefix for type parameters

```csharp
public class ExampleClass<TSource>
{
    public int PublicProperty { get; set; };
    protected static string _nonpublicStaticField;
    protected List<TSource> protectedField;

    public string GetMember(int memberIndex)
    {
        var selectedMember = this.protectedField[memberIndex] + _nonpublicStaticField;
        return selectedMember.Substring(this.PublicProperty);
    }
}
```

Spacing
--------------------------------------------------------------------------------

* An indent is worth **4 spaces**
* No tabs, only **spaces**
* Control flow operators (`if`, `else`, `switch`, `while`, `for`, etc.) have a
  space between the operator and the condition to distinguish from function
  calls.

```csharp
 if (iq > 0) /* vs. */ while(PersonIsSmart(isGoodSpeaker, isGoodLooking));
// ^ space ✔               ^ no space❌  ^ no space ✔
```

* `if` statements and the following action *may* be on the same line if:
  * Line content at or under 60 characters
  * Only one statement after the `if` condition

```csharp
                    // ... nested indentation ...
                    {
                        if (gameObject.activeInHierarchy) Shoot(gameObject, 50); // <= 60 characters ✔
                    }
// ... vs. ...
if (gameObject.name.Contains("Player")) transform.position = newPos; // > 60 characters ❌
```

* Brackets should **always** be on a separate line
* Case sections within switch are indented to indicate program flow.

```csharp
switch (product.Quality)
{
    case Quality.Pathetic:
    case Quality.Meh:
        willKeep = IsCheap(product);
        break;
    case Quality.Great:
        willKeep = WithinBudget(product);
        break;
}
```

Line Wrapping
--------------------------------------------------------------------------------

* Line margin at **column 120**
* Respect the line margin by wrapping lines at proper positions
  * Ensure that scope is shown by indentation and alignment
* When statements are forced to be on multiple lines, connecting...
  * **Boolean**, **assignment**, and **comparison** operators should be on the
    **right** side
  * **Dot** and **ternary** operators should be on the **left** side
* When a function call goes over the margin, put function call, dot operator,
  and arguments on the next line indented once
  * If this still goes over the line, give each argument its own line

```csharp
//                                     v On the right side ✔
if (gameObject.name.Contains("Person") ||
    gameObject.name.Contains("Place") ||
    (gameObject.GetComponent<Thing>() != null &&
     gameObject.transform.GetSiblingIndex !=
/*  ^ */ FighterConstants.Fighter.Index * FighterConstants.Siblings.Fighter))
/*  | Extra space to align ✔ */
{
    var actionChildrenNames = transform
        .Cast<Transform>()
            .Where(child => child.GetComponent<ActionController>() != null)
            .Select(actionChild => actionChild.name));
     // ^ Multi-level indentation ✔

    transform.GetChild(0).name = Importance > 2
        ? FighterConstants.ImportantChildName
        : FighterConstants.UnimportantChildName;

    var newChild = Instantiate( // No parameter on the first line ✔
            transform.GetChild(0).gameObject,
            transform.position,
            GetComponentInChildren<Thing>().transform.rotation,
            transform);
    // ^ Indented twice for future method chains (ex: .Foo().Bar())

    var newChild = transform
        .GetChild(0).gameObject
        .GetComponent<Thing>()
        .MakeAt(transform.position, Quaternion.identity)
}
```

File Structure
--------------------------------------------------------------------------------

* Files should be named after the classes they contain
* A **single** file should contain a **single** class and if necessary other
  extremely small, tightly-coupled supporting classes
* `using` statements should be placed **outside** the `namespace` to ensure
  namespace references are used over non-project related references
* End-of-line character should be on its own line
  * Provides a nice end of file cursor position and works with older editors

```csharp
// filename: SpitWadFighter
using System;
using UnityEngine;

namespace Fighting
{
    public class SpitWadEventArgs : EventArgs // Small, tightly-related class
    {
        float damage;
        GameObject target;
        GameObject source;
    }

    public class SpitWadFighter : MonoBehaviour // Main class
    {
        public event EventHandler<SpitWadEventArgs> SpitWadLaunched;

        // Rest of the class...
    }
}
// empty line here
```

Comments
--------------------------------------------------------------------------------

* All comments and documentation should have proper **spelling** and
  **capitalization**
  * Punctuation can be hit or miss, so attempt to structure comments/docs so
    that they will read the correct way
* Documentation should be as clear as possible and have **periods**
* Comments *may* have periods if more than one sentence
  * Single line comments explaining clean code are preferable to multiline
    comments explaining overly complex code
* **All** comments/docs should have 1 space after the `//` or `///` to separate
  the text

```csharp
// This is a regular comment
acceptButton.transform = oldPos; // Reset
```

* Use [Microsoft's C# XML] documentation with summary, parameters, and return
  data if applicable
* The following are required to have documentation
  * `public` classes and interfaces
  * Methods and properties of `public` interfaces
  * Any other nonobvious object, property, or method
* All nonobvious [serializable] field members must have tooltips using the
  [tooltip attribute]
* Use [XML Tags] like `<see cref=""/>` and `<paramref name=""/>` within
  documentation comments
  * Provides detailed, linked information to readers
  * Helps when refactoring names

```csharp
/// <summary>List of options to show.</summary>
[Tooltip("List of options to show the player")]
public List<Option> AvailableOptions;

/// <summary>
/// Creates a new <see cref="Vector3Bool"/> with the first, second, and third bits
/// of <paramref name="xyz"/>.
/// </summary>
/// <remarks>The forth bit is ignored and names are left empty.</remarks>
/// <param name="xyz">Byte containing the bit values.</param>
/// <example><code>
/// var vecBool = new Vector3Bool(0b110);  // x: 1, y: 1, z: 0
/// </code></example>
public Vector3Bool Create(byte xyz)
{
    // ...
}
```

Verbosity
--------------------------------------------------------------------------------

* All members and types should **always** denote their accessibility
  level using `public`, `protected`, and `private` keywords
* Keyword `var` is reserved for shortening declarations of non-primitives
  * Use `var` when the inferred type is obvious if you had a vague idea what
    might be returned
  * Primitives like `int`, `string`, `float`, etc should be declared explicitly

```csharp
public struct Sphere
{
    public float Radius;  // Note verbose use of public even though
                          // public is default for structs
    private Vector3 Center;

    public bool IsOnBorder(GameObject go)
    {
        var pos = go.transform.position;  // Inferred
        var diff = Center - pos;
        float sqrLen = Mathf.Abs(diff.sqrMagnitude);  // Verbose

        return sqrLen == (Radius * Radius);
    }
}
```

[Microsoft's C# Coding Conventions]: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions
[Microsoft's .NET Naming Guidelines]: https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/naming-guidelines
[Microsoft's C# XML]: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc/xml-documentation-comments
[XML Tags]: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc/recommended-tags-for-documentation-comments
[serializable]: https://docs.unity3d.com/ScriptReference/Serializable.html
[tooltip attribute]: https://docs.unity3d.com/ScriptReference/TooltipAttribute.html
[Visual Studio EditorConfig Settings]: https://docs.microsoft.com/en-us/visualstudio/ide/create-portable-custom-editor-options?view=vs-2019
[Visual Studio Code EditorConfig Extension]: https://marketplace.visualstudio.com/items?itemName=EditorConfig.EditorConfig
[Rider EditorConfig Settings]: https://www.jetbrains.com/help/rider/Using_EditorConfig.html
