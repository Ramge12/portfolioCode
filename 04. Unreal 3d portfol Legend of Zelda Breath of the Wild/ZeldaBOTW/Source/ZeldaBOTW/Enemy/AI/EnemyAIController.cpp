// Fill out your copyright notice in the Description page of Project Settings.

#include "EnemyAIController.h"
#include "BehaviorTree/BehaviorTree.h"
#include "BehaviorTree/BlackboardData.h"
#include "BehaviorTree/BlackboardComponent.h"

const FName AEnemyAIController::HomePosKey(TEXT("HomePos"));
const FName AEnemyAIController::PatrolPosKey(TEXT("PatrolPos"));
const FName AEnemyAIController::OnBattle(TEXT("OnBattle"));
const FName AEnemyAIController::TargetKey(TEXT("Target"));
const FName AEnemyAIController::IsWeapon(TEXT("IsWeapon"));
const FName AEnemyAIController::AttackRange(TEXT("AttackRange"));
const FName AEnemyAIController::MaxRange(TEXT("MaxRange"));

AEnemyAIController::AEnemyAIController()
{
	static ConstructorHelpers::FObjectFinder<UBlackboardData> BBObject(TEXT("/Game/EnemyCharacter/Behavior/EnemyBlackBorad.EnemyBlackBorad"));
	if (BBObject.Succeeded()) {
		BBAsset = BBObject.Object;
	}
	static ConstructorHelpers::FObjectFinder<UBehaviorTree> BTObject(TEXT("/Game/EnemyCharacter/Behavior/EnemyBehaviorTree.EnemyBehaviorTree"));
	if (BTObject.Succeeded()) {
		BTAsset = BTObject.Object;
	}
}

void AEnemyAIController::Possess(APawn * InPawn)
{
	Super::Possess(InPawn);
	if (UseBlackboard(BBAsset, Blackboard)) {
		Blackboard->SetValueAsVector(HomePosKey, InPawn->GetActorLocation());
		if (!RunBehaviorTree(BTAsset)) {
			ABLOG(Error, TEXT("AIController couldn't run behavior tree!"));
		}
	}
}
