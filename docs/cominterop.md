COM Interop
===========

PreserveSig
-----------

By default, C# signatures are not preserved (the opposite of P/Invoke). Any specified return value other than void is considered to be a final `[out, retval]` parameter in the native COM signature.

`HRESULT` return values on native COM signatures are intercepted and failure codes are turned into exceptions. When `[PreservereSig]` is applied, the signature must match (i.e. return `HRESULT`) and the interop layer will not interpret the value. This is particularly useful if you need to differentiate different "success" values (e.g. `S_OK` vs. `S_FALSE`). This is also useful if you want by ref values to be propagated in error conditions. Without `PreserveSig`, by ref values will not be set when an error `HRESULT` is returned.

[Adam Nathan on PreserveSig](https://blogs.msdn.microsoft.com/adam_nathan/2003/04/30/preservesig/)