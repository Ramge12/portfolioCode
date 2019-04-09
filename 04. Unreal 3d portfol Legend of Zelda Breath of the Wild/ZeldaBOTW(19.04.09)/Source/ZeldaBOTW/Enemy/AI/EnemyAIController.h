// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ZeldaBOTW.h"
#include "AIController.h"
#include "EnemyAIController.generated.h"

UCLASS()
class ZELDABOTW_API AEnemyAIController : public AAIController
{
	GENERATED_BODY()
public:
	static const FName HomePosKey;
	static const FName PatrolPosKey;
	static const FName TargetKey;
	static const FName OnBattle;
	static const FName IsWeapon;
	static const FName AttackRange;
	static const FName MaxRange;

private:
	UPROPERTY()
	class UBehaviorTree*  BTAsset;

	UPROPERTY()
	class UBlackboardData* BBAsset;

public:
	AEnemyAIController();
	virtual void Possess(APawn* InPawn) override;

};
