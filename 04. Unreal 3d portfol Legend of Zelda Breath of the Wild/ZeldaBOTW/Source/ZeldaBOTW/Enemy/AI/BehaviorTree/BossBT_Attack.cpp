// Fill out your copyright notice in the Description page of Project Settings.

#include "BossBT_Attack.h"
#include "Player/Controller/PlayerCharacterController.h"
#include "Player/PlayerCharacter.h"
#include "Enemy/AI/BossAIController.h"
#include "Enemy/BossCharacter.h"
#include "BehaviorTree/BlackboardComponent.h"
#include "DrawDebugHelpers.h"

UBossBT_Attack::UBossBT_Attack() 
{
	NodeName = TEXT("BossAttack");
}


EBTNodeResult::Type UBossBT_Attack::ExecuteTask(UBehaviorTreeComponent & OwnerComp, uint8 * NodeMemory)
{
	auto ControllingPawn = OwnerComp.GetAIOwner()->GetPawn();
	if (nullptr == ControllingPawn) {
		return EBTNodeResult::Failed;
	}
	auto BossCharacter = Cast<ABossCharacter>(ControllingPawn);
	if (nullptr == BossCharacter) {
		return EBTNodeResult::Failed;
	}
	auto PlayerController = GetWorld()->GetFirstPlayerController();
	auto PController = Cast< APlayerCharacterController>(PlayerController);
	auto PlayerCharacter = PController->getPlayerCharacter();
	if (nullptr == PlayerCharacter) {
		return EBTNodeResult::Failed;
	}
	FVector Xrotator = (BossCharacter->GetActorLocation() - PlayerCharacter->GetActorLocation());
	
	FRotator headRotator = FMath::RInterpTo(BossCharacter->getBossHeadMesh()->GetComponentRotation(), Xrotator.Rotation() + FRotator(0, 90.0f, 0), GetWorld()->GetDeltaSeconds(),3.f);
	BossCharacter->getBossHeadMesh()->SetWorldRotation(headRotator);

	FVector EyeLocation = BossCharacter->getBossHeadMesh()->GetSocketLocation("Light");
	FVector PlayerLocation = PlayerCharacter->GetActorLocation() + FVector(0, 0, 10.0f);
	static float thickness = 0.0f;
	thickness += GetWorld()->GetDeltaSeconds();
	
	FHitResult HitResult;
	FCollisionQueryParams Params(NAME_None, false, BossCharacter);
	bool bResult =
		GetWorld()->SweepSingleByChannel
		(
			HitResult,
			EyeLocation,
			PlayerLocation,
			FQuat::Identity,
			ECollisionChannel::ECC_GameTraceChannel1,
			FCollisionShape::MakeSphere(3.f),
			Params
		);
#if ENABLE_DRAW_DEBUG
	FColor DrawColor = bResult ? FColor::Green : FColor::Red;
	float DebugLifeTime = GetWorld()->GetDeltaSeconds();
	FQuat CapsuleRot = FRotationMatrix::MakeFromZ(Xrotator).ToQuat();

	/*DrawDebugCircle(
		GetWorld(),
		HitResult.ImpactPoint,
		(5.f - thickness)*5.0f,
		500,
		DrawColor,
		false,
		DebugLifeTime,
		0,
		0,
		Xrotator.GetSafeNormal()
	);*/
	DrawDebugCapsule(
		GetWorld(),
		HitResult.ImpactPoint,
		0,
		(5.f - thickness)*5.0f,
		CapsuleRot,
		FColor::Green,
		false,
		DebugLifeTime
	);
#endif
	if (bResult) {
		DrawDebugLine(GetWorld(), EyeLocation, HitResult.ImpactPoint, FColor::Red, true, GetWorld()->GetDeltaSeconds(), 0, thickness);
	}
	else {
		thickness = 0.0f;
	}
	if (thickness > 5.f) {
		PlayerCharacter->getPlayerCamera()->ShakeCameraActor();
		auto hitCaracter = Cast<APlayerCharacter>(HitResult.GetActor());
		if (hitCaracter != nullptr) {
			PController->getBattleController()->PlayerHurt();
		}
		thickness = 0.0f;
	}
	return EBTNodeResult::Succeeded;
}
