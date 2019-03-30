// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ZeldaBOTW.h"
#include "BehaviorTree/BTTaskNode.h"
#include "BTTask_EnemyIdle.generated.h"

UCLASS()
class ZELDABOTW_API UBTTask_EnemyIdle : public UBTTaskNode
{
	GENERATED_BODY()
public:
	UBTTask_EnemyIdle();
	virtual EBTNodeResult::Type ExecuteTask(UBehaviorTreeComponent& OwnerComp, uint8* NodeMemory)override;

};
