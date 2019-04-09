// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ZeldaBOTW.h"
#include "BehaviorTree/BTService.h"
#include "BossPatrol.generated.h"

/**
 * 
 */
UCLASS()
class ZELDABOTW_API UBossPatrol : public UBTService
{
	GENERATED_BODY()
	
	
public:
	UBossPatrol();

protected:
	virtual void TickNode(UBehaviorTreeComponent& OwnerComp, uint8* NodeMemory, float DeltaSeconds) override;


	
};
