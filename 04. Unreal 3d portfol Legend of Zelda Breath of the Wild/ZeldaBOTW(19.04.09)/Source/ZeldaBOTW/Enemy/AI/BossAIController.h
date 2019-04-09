// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ZeldaBOTW.h"
#include "AIController.h"
#include "BossAIController.generated.h"

UCLASS()
class ZELDABOTW_API ABossAIController : public AAIController
{
	GENERATED_BODY()
public:
	static const FName HomePos;
	static const FName NextPos;
	static const FName Target;
	static const FName OnPatrol;
	static const FName OnBattle;
	static const FName PatrolRange;
	static const FName FindRange;
	static const FName AttackRange;
	
private:
	UPROPERTY()
	class UBehaviorTree*  BTAsset;
	UPROPERTY()
	class UBlackboardData* BBAsset;

public:
	ABossAIController();
	virtual void Possess(APawn* InPawn) override;
};
