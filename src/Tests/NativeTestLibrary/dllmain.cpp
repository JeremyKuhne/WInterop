// dllmain.cpp : Defines the entry point for the DLL application.
#include "stdafx.h"
#include "commctrl.h"
#include "accctrl.h"
#include "shellapi.h"
#include "shlobj.h"
#include "aclapi.h"
#include "lm.h"
#include "d2d1.h"
#include "dwrite.h"
#include "richedit.h"
#include "shlwapi.h"
#include "uxtheme.h"
#include "Mshtmhst.h"
#include "richole.h"
#include "OleCtl.h"
#include "winternl.h"

BOOL APIENTRY DllMain(
    HMODULE hModule,
    DWORD  ul_reason_for_call,
    LPVOID lpReserved)
{
    switch (ul_reason_for_call)
    {
        case DLL_PROCESS_ATTACH:
        case DLL_THREAD_ATTACH:
        case DLL_THREAD_DETACH:
        case DLL_PROCESS_DETACH:
            break;
    }

    return TRUE;
}
