// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ZeldaBOTW.h"
#include "BehaviorTree/BTTaskNode.h"
#include "BossBT_Attack.generated.h"

/**
 * 
 */
UCLASS()
class ZELDABOTW_API UBossBT_Attack : public UBTTaskNode
{
	GENERATED_BODY()
public:
	UBossBT_Attack();
	virtual EBTNodeResult::Type ExecuteTask(UBehaviorTreeComponent& OwnerComp, uint8* NodeMemory)override;
};
