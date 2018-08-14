#pragma once

extern "C" __declspec (dllexport) int Double(int);

extern "C" __declspec (dllexport) int __stdcall DoubleStdCall(int);

extern "C" __declspec (dllexport) void* __stdcall IntPointerCheck(int* value, int count);

extern "C" __declspec (dllexport) void* __stdcall StructPointerCheck(LPPOINT lppt, int count);

extern "C" __declspec (dllexport) POINT __stdcall ReturnPoint(int x, int y);

extern "C" __declspec (dllexport) POINTFLOAT __stdcall ReturnPointFloat(float x, float y);