// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ZeldaBOTW.h"
#include "Blueprint/UserWidget.h"
#include "BossUIClass.generated.h"

UCLASS()
class ZELDABOTW_API UBossUIClass : public UUserWidget
{
	GENERATED_BODY()
	
public:
	UBossUIClass(const FObjectInitializer& ObjectInitializer);
	void SetHP(int curHP, int maxHP);
	
};
