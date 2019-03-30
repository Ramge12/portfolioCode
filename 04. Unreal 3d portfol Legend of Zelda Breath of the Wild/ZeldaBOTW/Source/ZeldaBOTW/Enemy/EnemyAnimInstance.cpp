// Fill out your copyright notice in the Description page of Project Settings.

#include "EnemyAnimInstance.h"
#include "DrawDebugHelpers.h"
#include "Enemy/EnemyCharacter.h"
#include "Weapon/Weapon_Equip.h"
#include "Player/Controller/PlayerCharacterController.h"
#include "Player/PlayerCharacterState.h"
#include "Player/PlayerCharacter.h"
#include "Enemy/EnemyCharacterState.h"

UEnemyAnimInstance::UEnemyAnimInstance()
{
}

void UEnemyAnimInstance::AnimNotify_AttackStart()
{
	AEnemyCharacter* EnmeyCharacter = Cast< AEnemyCharacter>(EnemyState->GetOwner());
	AWeapon_Equip* Weapon_Sword = Cast<AWeapon_Equip>(EnmeyCharacter->getWeapon());
	auto PlayerController = GetWorld()->GetFirstPlayerController();
	APlayerCharacterController* PController = Cast< APlayerCharacterController>(PlayerController);
	UPlayerCharacterState* PlayerState = PController->getPlayerState();

	FHitResult HitResult;
	float TraceRange = 70.0f;
	float TraceRadius = 20.0f;
	FCollisionQueryParams Params(NAME_None, false, EnmeyCharacter);
	bool bResult =
		GetWorld()->SweepSingleByChannel
		(
			HitResult,
			EnmeyCharacter->GetActorLocation(),
			EnmeyCharacter->GetActorLocation() + EnmeyCharacter->GetActorForwardVector() * TraceRange,
			FQuat::Identity,
			ECollisionChannel::ECC_GameTraceChannel1,
			FCollisionShape::MakeSphere(TraceRadius),
			Params
		);

	if (bResult) {
		if (HitResult.GetComponent()->ComponentHasTag("PlayerCharacter")) {
			PController->getBattleController()->PlayerHurt();
		}
	}
	else {
		if (Weapon_Sword != nullptr) {
			Weapon_Sword->setHitPlayer(true);
		}
	}
	PlayerState->setCounterPossibility(true);
#if ENABLE_DRAW_DEBUG
	//FVector TraceVec = EnmeyCharacter->GetActorForwardVector() * TraceRange;
	//FVector Center = EnmeyCharacter->GetActorLocation() + TraceVec * 0.5f;
	//float HalfHeight = TraceRange * 0.5f;
	//FQuat CapsuleRot = FRotationMatrix::MakeFromZ(TraceVec).ToQuat();
	//FColor DrawColor = bResult ? FColor::Green : FColor::Red;
	//float DebugLifeTime = 0.5f;
	//DrawDebugCapsule(
	//		GetWorld(),
	//		Center,
	//		HalfHeight,
	//		TraceRadius,
	//		CapsuleRot,
	//		DrawColor,
	//		false,
	//		DebugLifeTime
	//);
#endif
}

void UEnemyAnimInstance::AnimNotify_AttackEnd()
{
	AEnemyCharacter* EnmeyCharacter = Cast< AEnemyCharacter>(EnemyState->GetOwner());
	AWeapon_Equip* Weapon_Sword = Cast<AWeapon_Equip>(EnmeyCharacter->getWeapon());
	if (Weapon_Sword != nullptr) {
		Weapon_Sword->setHitPlayer(false);
		EnemyState->DelayTimer();
	}
	auto PlayerController = GetWorld()->GetFirstPlayerController();
	APlayerCharacterController* PController = Cast< APlayerCharacterController>(PlayerController);
	UPlayerCharacterState* PlayerState = PController->getPlayerState();
	PlayerState->setCounterPossibility(false);
}

void UEnemyAnimInstance::AnimNotify_HurtEnd()
{
	EnemyState->setCurEnemyState(EnemyState::Enemy_IDLE);
}

void UEnemyAnimInstance::AnimNotify_ArrowShotEnd()
{
	AEnemyCharacter* EnmeyCharacter = Cast< AEnemyCharacter>(EnemyState->GetOwner());
	AWeapon_Equip* Weapon_Sword = Cast<AWeapon_Equip>(EnmeyCharacter->getWeapon());
	if (Weapon_Sword != nullptr) {
		Weapon_Sword->setHitPlayer(false);
		EnemyState->DelayTimer();
		EnmeyCharacter->ShootArrow();
	}
}
