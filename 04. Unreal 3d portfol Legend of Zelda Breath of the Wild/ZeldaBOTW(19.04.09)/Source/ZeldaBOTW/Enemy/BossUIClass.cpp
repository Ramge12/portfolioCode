// Fill out your copyright notice in the Description page of Project Settings.

#include "BossUIClass.h"
#include "Components/ProgressBar.h"

UBossUIClass::UBossUIClass(const FObjectInitializer& ObjectInitializer) :Super(ObjectInitializer)
{

}

void UBossUIClass::SetHP(int curHP, int maxHP)
{
	auto EnemyHpBar = Cast<UProgressBar>(GetWidgetFromName(TEXT("HPBAR")));
	float percetn = (float)curHP / maxHP;
	if (IsValid(EnemyHpBar)) {
		EnemyHpBar->SetPercent(percetn);
	}
}
