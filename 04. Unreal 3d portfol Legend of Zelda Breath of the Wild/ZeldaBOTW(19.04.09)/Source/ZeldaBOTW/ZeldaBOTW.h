// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "EngineMinimal.h"

DECLARE_LOG_CATEGORY_EXTERN(ZeldaBOTW, Log, All);

#define AB_LOG_CALLINFO (FString(__FUNCTION__) + TEXT("(") + FString::FromInt(__LINE__) + TEXT(")"))

#define ABLOG_S(Verbosity) UE_LOG(ZeldaBOTW,Verbosity,TEXT("%s"),*AB_LOG_CALLINFO)

#define ABLOG(Verbosity,Format,...)UE_LOG(ZeldaBOTW,Verbosity,TEXT("%s %s"),*AB_LOG_CALLINFO,*FString::Printf(Format,##__VA_ARGS__))

#define ABCHECK(Expr,...){if(!(Expr)){ABLOG(Error,TEXT("ASSERTION : %s"),TEXT("'"#Expr"'")); return __VA_ARGS__;}}
