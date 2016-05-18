#include "stdafx.h"
#include "TestExports.h"

extern "C" __declspec (dllexport) int Double(int value)
{
    return value * 2;
}