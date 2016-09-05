Structs
=======

Deeper details on interop struct handling.

Passing Structs
---------------

Given a simple Windows API that takes a struct parameter:

``` C
typedef struct tagPOINT
{
    LONG  x;
    LONG  y;
} POINT, *PPOINT, NEAR *NPPOINT, FAR *LPPOINT;

BOOL WINAPI GetCursorPos(
  _Out_ LPPOINT lpPoint
);
```

The managed equivalent is pretty similar:

``` C#
public struct Point
{
    public int x;
    public int y;
}

[DllImport("user32.dll", SetLastError = true)]
public static extern bool GetCursorPos(out Point lpPoint);
```

The managed `Point` struct is blittable as it only contains blittable fields and has a fixed layout
(as `[StructLayout(LayoutKind.Sequential)]` is implicit on struct definitions in C#).
Because it is blittable _and_ it is being passed by reference (via `out`) a direct pointer to the managed struct
will be passed. `Point` won't need to be copied in this case.

You can alternatively use a formatted class instead of a struct:

``` C#
[StructLayout(LayoutKind.Sequential)]
public class Point
{
    public int x;
    public int y;
}

[DllImport("user32.dll", SetLastError = true)]
public static extern bool GetCursorPos(Point lpPoint);
```

The managed `Point` class is also blittable and will be pinned and a direct pointer will be passed.
As shown, mapping of the parameters in the managed `[DllImport]` is slightly different. Here is a
table

| native parameter | `struct` managed parameter | `class` managed parameter |
|-|-|-|
| `POINT p` | `Point p` | N/A |
| `POINT* p` | `ref` or `out Point p` | `Point p` |
| `POINT** p` | `IntPtr` | `ref` or `out Point p` |


Why use a formatted class instead of a struct? Here are a few reasons:

1. Allows passing `null`
2. Can create a default constructor
3. Can create a finalizer
4. Avoid field copying when passing to other managed methods

##### Passing `struct` as null

This frequently is useful as many APIs allow or even require passing null for arguments.
Other possible solutions:

- Define multiple `[DllImport]`'s for the same API (bloated)
- Use `unsafe` and `Point* p`
- Use `IntPtr p` instead of `Point p` and `GCHandle` to pin or
