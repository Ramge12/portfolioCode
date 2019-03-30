// Fill out your copyright notice in the Description page of Project Settings.

#include "BossPatrol.h"
#include "Player/Controller/PlayerCharacterController.h"
#include "Player/PlayerCharacter.h"
#include "Enemy/BossCharacter.h"
#include "Enemy/AI/BossAIController.h"
#include "BehaviorTree/BlackboardComponent.h"
#include "DrawDebugHelpers.h"

UBossPatrol::UBossPatrol()
{
	NodeName = TEXT("Patrol");
	Interval = 1.0f;
}

void UBossPatrol::TickNode(UBehaviorTreeComponent & OwnerComp, uint8 * NodeMemory, float DeltaSeconds)
{
	APawn* ControllingPawn = OwnerComp.GetAIOwner()->GetPawn();
	if (nullptr == ControllingPawn) return;
	UWorld* World = ControllingPawn->GetWorld();
	if (nullptr == World)return;
	ABossCharacter* BossCharacter = Cast< ABossCharacter>(ControllingPawn);
	if (nullptr == BossCharacter)return;
	UNavigationSystem* NavSystem = UNavigationSystem::GetNavigationSystem(ControllingPawn->GetWorld());
	if (nullptr == NavSystem) return;
	
	FVector Homeposkey = OwnerComp.GetBlackboardComponent()->GetValueAsVector(ABossAIController::HomePos);
	FNavLocation NextPatrol;

	if (NavSystem->GetRandomPointInNavigableRadius(Homeposkey, BossCharacter->getPatrolRange(), NextPatrol)) {
		OwnerComp.GetBlackboardComponent()->SetValueAsVector(ABossAIController::NextPos, NextPatrol.Location);
	}

	auto PlayerController = GetWorld()->GetFirstPlayerController();
	APlayerCharacterController* PController = Cast< APlayerCharacterController>(PlayerController);
	APlayerCharacter* PlayerCharacter = PController->getPlayerCharacter();
	if (IsValid(PlayerCharacter)) {
		OwnerComp.GetBlackboardComponent()->SetValueAsObject(ABossAIController::Target, PlayerCharacter);
	}

	float distanceToPlayer = FVector::Dist(BossCharacter->GetActorLocation(), PlayerCharacter->GetActorLocation());
	if (distanceToPlayer < BossCharacter->getAttackRange()) {
		BossCharacter->setOnBattle(true);
		BossCharacter->setBossHPUI(true);
		OwnerComp.GetBlackboardComponent()->SetValueAsBool(ABossAIController::OnBattle, true);

	}
	else {
		BossCharacter->setOnBattle(false);
		BossCharacter->setBossHPUI(false);
		OwnerComp.GetBlackboardComponent()->SetValueAsBool(ABossAIController::OnBattle, false);

	}

}
