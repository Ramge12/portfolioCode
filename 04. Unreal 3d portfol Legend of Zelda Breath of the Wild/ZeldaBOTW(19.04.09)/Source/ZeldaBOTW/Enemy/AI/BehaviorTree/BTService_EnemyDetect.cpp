// Fill out your copyright notice in the Description page of Project Settings.

#include "BTService_EnemyDetect.h"
#include "Player/PlayerCharacter.h"
#include "Enemy/EnemyCharacter.h"
#include "Enemy/EnemyCharacterState.h"
#include "Enemy/AI/EnemyAIController.h"
#include "BehaviorTree/BlackboardComponent.h"
#include "DrawDebugHelpers.h"

UBTService_EnemyDetect::UBTService_EnemyDetect()
{
	NodeName = TEXT("Detect");
	Interval = 1.0f;
}

void UBTService_EnemyDetect::TickNode(UBehaviorTreeComponent & OwnerComp, uint8 * NodeMemory, float DeltaSeconds)
{
	APawn* ControllingPawn = OwnerComp.GetAIOwner()->GetPawn();
	if (nullptr == ControllingPawn) return;
	UWorld* World = ControllingPawn->GetWorld();
	if (nullptr == World)return;
	AEnemyCharacter* EnemyCharacter = Cast< AEnemyCharacter>(ControllingPawn);
	if (nullptr == EnemyCharacter)return;
	UEnemyCharacterState* EnemyState = EnemyCharacter->getEnemyState();
	if (nullptr == EnemyState)return;

	float distanceToHome = FVector::Dist(EnemyCharacter->GetActorLocation(), OwnerComp.GetBlackboardComponent()->GetValueAsVector(AEnemyAIController::HomePosKey));
	if (EnemyState->getMaxRage() < distanceToHome) {
		EnemyState->setEnemyMaxRange(true);
		EnemyState->setOnBattle(false);
	}
	if (distanceToHome < EnemyState->getIdleRange()) {
		EnemyState->setEnemyMaxRange(false);
	}

	float distanceToPlayer = ControllingPawn->GetDistanceTo(EnemyCharacter->getPlayerCharacter());
	if (distanceToPlayer < EnemyState->getEnemyAttackRange()) {
		EnemyState->setInAttackRange(true);
	}
	else {
		EnemyState->setInAttackRange(false);
	}

	bool E_OnBattle = EnemyState->getOnBattle();
	bool E_IsWeapon = EnemyState->getHasWeapon();
	bool E_isMaxRange = EnemyState->getEnemyMaxRage();
	bool E_IsAttackRange = EnemyState->getInAttackRange();

	OwnerComp.GetBlackboardComponent()->SetValueAsBool(AEnemyAIController::MaxRange, E_isMaxRange);
	OwnerComp.GetBlackboardComponent()->SetValueAsBool(AEnemyAIController::OnBattle, E_OnBattle);
	OwnerComp.GetBlackboardComponent()->SetValueAsBool(AEnemyAIController::IsWeapon, E_IsWeapon);
	OwnerComp.GetBlackboardComponent()->SetValueAsBool(AEnemyAIController::AttackRange, E_IsAttackRange);

	OwnerComp.GetBlackboardComponent()->SetValueAsObject(AEnemyAIController::TargetKey, EnemyCharacter->getPlayerCharacter());
	if (E_OnBattle && !E_IsWeapon)OwnerComp.GetBlackboardComponent()->SetValueAsVector(AEnemyAIController::PatrolPosKey, EnemyCharacter->getWeapon()->GetActorLocation());
}