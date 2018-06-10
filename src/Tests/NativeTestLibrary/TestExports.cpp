#include "stdafx.h"
#include "TestExports.h"

int Double(int value)
{
    return value * 2;
}

int DoubleStdCall(int value)
{
    return value * 2;
}

void* StructPointerCheck(LPPOINT lppt, int count)
{
    if (lppt && count > 1)
        lppt[0].x = lppt[1].x;

    return lppt;
}

void* IntPointerCheck(int* value, int count)
{
    if (value && count > 0) *value = *value * 2;
    return value;
}