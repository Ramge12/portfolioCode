// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ZeldaBOTW.h"
#include "BehaviorTree/BTService.h"
#include "BTService_EnemyDetect.generated.h"

UCLASS()
class ZELDABOTW_API UBTService_EnemyDetect : public UBTService
{
	GENERATED_BODY()
	
public:
	UBTService_EnemyDetect();

protected:
	virtual void TickNode(UBehaviorTreeComponent& OwnerComp, uint8* NodeMemory, float DeltaSeconds) override;
	
};
