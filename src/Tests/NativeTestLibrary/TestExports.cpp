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

void* StructPointerCheck(LPPOINT lppt)
{
    lppt[0].x = lppt[1].x;
    return lppt;
}

void* IntPointerCheck(int* value)
{
    return value;
}