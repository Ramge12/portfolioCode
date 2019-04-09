// Fill out your copyright notice in the Description page of Project Settings.

#include "PlayerTrace.h"
#include "DrawDebugHelpers.h"
#include "NPC/NPC_Shop.h"
#include "Player/PlayerCharacterState.h"
#include "Player/PlayerCharacter.h"
#include "Player/PlayerUiWidget.h"
#include "Enemy/EnemyCharacter.h"
#include "Object/BrokenObject.h"
#include "Player/Controller/PlayerCharacterController.h"

void UPlayerTrace::BeginPlay()
{
	Super::BeginPlay();
}

UPlayerTrace::UPlayerTrace()
{
	PrimaryComponentTick.bCanEverTick = false;
	ViewDebugLine = false;
	TraceClose = false;
	TraceForward = false;
	TraceClimming = false;
	TracePush = false;
	TraceHeights = false;
	ThrowValue = false;
}

void UPlayerTrace::PlayerTraceCheck(FVector playerLocation, FVector playerForwardVector, AActor * playerActer)
{
	PlayerForwardTrace(playerLocation, playerForwardVector, playerActer);
	PlayerCloseForwardTrace(playerLocation, playerForwardVector, playerActer);
	PlayerHeightTrace(playerLocation, playerForwardVector, playerActer);
}

void UPlayerTrace::PlayerForwardTrace(FVector playerLocation, FVector playerForwardVector, AActor * playerActer)
{
	FHitResult HitResult;
	float TraceRange = 10.0f;
	float TraceRadius = 5.0f;
	float StartRange = 15.0f;
	FVector StartPosition = StartRange * playerForwardVector;
	FCollisionQueryParams Params(NAME_None, false, playerActer);
	bool bResult =
		GetWorld()->SweepSingleByChannel(
			HitResult,
			playerLocation + StartPosition,
			playerLocation + StartPosition + playerForwardVector * TraceRange,
			FQuat::Identity,
			ECollisionChannel::ECC_GameTraceChannel2,
			FCollisionShape::MakeSphere(TraceRadius),
			Params
		);
#if ENABLE_DRAW_DEBUG
	FVector TraceVec = playerForwardVector * TraceRange;
	FVector Center = playerLocation + TraceVec * 0.5f + StartPosition;
	float HalfHeight = TraceRange * 0.5f;
	FQuat CapsuleRot = FRotationMatrix::MakeFromZ(TraceVec).ToQuat();
	FColor DrawColor = bResult ? FColor::Green : FColor::Red;
	float DebugLifeTime = 0.5f;

	if (ViewDebugLine) {
		DrawDebugCapsule(
			GetWorld(),
			Center,
			HalfHeight,
			TraceRadius,
			CapsuleRot,
			DrawColor,
			false,
			DebugLifeTime
		);
	}
#endif
	static bool CarryNow = false;
	auto Controller = GetWorld()->GetFirstPlayerController();
	ABrokenObject* Object = Cast< ABrokenObject>(HitResult.GetActor());
	ANPC_Shop* NPC = Cast< ANPC_Shop>(HitResult.GetActor());
	APlayerCharacterController* PlayerController = Cast<APlayerCharacterController>(Controller);
	APlayerCharacter* playerCharactor = PlayerController->getPlayerCharacter();

	if (bResult) {
		TraceForward = true;
		TraceCheckActor(HitResult, playerActer);
		if (HitResult.GetComponent()->ComponentHasTag(FName("ExtraObject"))) {
			if (IsValid(Object)) {
				if (IsValid(PlayerController)) {
					PlayerController->getPlayerState()->setExtraState(Object->getPlayerExtraState());
				}
			}
			else if (IsValid(NPC)) {
				if (IsValid(PlayerController)) {
					PlayerController->getPlayerState()->setExtraState(NPC->getPlayerState());
				}
			}
		}
		if (IsValid(playerCharactor) && IsValid(PlayerController) && !CarryNow) {
			if (PlayerController->getPlayerState()->CheckCurState(E_PlayerState::Player_Carry)) {
				if (IsValid(Object)) {
					Object->setCharacterEquip(playerCharactor->getPlayerMeshComponent()->getRootMesh(), TEXT("PlayerForwardSocket"));
					Object->setBrokenColiision(false);
				}
				PlayerController->getPlayerState()->setExtraState(E_PlayerState::Player_NONE);
				CarryNow = true;
			}
		}
		else if (IsValid(playerCharactor) && CarryNow) {
			if (IsValid(Object)) {
				if (!Object->getObjectCarry()) {
					PlayerController->getPlayerState()->setPlayerState(E_PlayerState::Player_IDLE);
					PlayerController->getPlayerState()->setExtraState(E_PlayerState::Player_NONE);
					CarryNow = false;
					Object->setCarryon(true);
				}
			}
		}
		if (ThrowValue) {
			PlayerController->getPlayerState()->setPlayerState(E_PlayerState::Player_IDLE);
			PlayerController->getPlayerState()->setThrowObject(true);
			if (IsValid(Object)) {
				Object->getObjectMesh()->DetachFromParent();
				Object->getObjectMesh()->SetSimulatePhysics(true);
				Object->setBrokenColiision(true);
			}
			ThrowValue = false;
		}
	}
	else {
		CarryNow = false;
		PlayerController->getPlayerState()->setExtraState(E_PlayerState::Player_NONE);
		TraceForward = false;
		TraceClimming = false;
		TracePush = false;
		TraceHeights = false;
	}
}

void UPlayerTrace::PlayerCloseForwardTrace(FVector playerLocation, FVector playerForwardVector, AActor * playerActer)
{
	FHitResult HitResult;
	float TraceRange = 20.0f;
	float TraceRadius = 5.0f;
	FCollisionQueryParams Params(NAME_None, false, playerActer);
	bool bResult =
		GetWorld()->SweepSingleByChannel(
			HitResult,
			playerLocation,
			playerLocation + playerForwardVector * TraceRange,
			FQuat::Identity,
			ECollisionChannel::ECC_GameTraceChannel2,
			FCollisionShape::MakeSphere(TraceRadius),
			Params
		);
#if ENABLE_DRAW_DEBUG
	FVector TraceVec = playerForwardVector * TraceRange;
	FVector Center = playerLocation + TraceVec * 0.5f;
	float HalfHeight = TraceRange * 0.5f;
	FQuat CapsuleRot = FRotationMatrix::MakeFromZ(TraceVec).ToQuat();
	FColor DrawColor = bResult ? FColor::Green : FColor::Red;
	float DebugLifeTime = 0.5f;

	if (ViewDebugLine) {
		DrawDebugCapsule(
			GetWorld(),
			Center,
			HalfHeight,
			TraceRadius,
			CapsuleRot,
			DrawColor,
			false,
			DebugLifeTime
		);
	}
#endif
	if (bResult) {
		TraceClose = true;
	}
	else {
		TraceClose = false;
	}
}

void UPlayerTrace::PlayerHeightTrace(FVector playerLocation, FVector playerForwardVector, AActor * playerActer)
{
	FHitResult HitResult;
	float TraceRange = 10.0f;
	float TraceRadius = 5.0f;
	float StartRange = 15.0f;
	float TraceHeight = 80.0f;
	FVector StartPosition = FVector(0, 0, TraceHeight) + playerForwardVector * StartRange;
	FCollisionQueryParams Params(NAME_None, false, playerActer);
	bool bResult =
		GetWorld()->SweepSingleByChannel
		(
			HitResult,
			playerLocation + StartPosition,
			playerLocation + StartPosition + playerForwardVector * TraceRange,
			FQuat::Identity,
			ECollisionChannel::ECC_GameTraceChannel2,
			FCollisionShape::MakeSphere(TraceRadius),
			Params
		);
	TraceHeights = bResult;
#if ENABLE_DRAW_DEBUG
	FVector TraceVec = playerForwardVector * TraceRange;
	FVector Center = playerLocation + TraceVec * 0.5f + StartPosition;
	float HalfHeight = TraceRange * 0.5f;
	FQuat CapsuleRot = FRotationMatrix::MakeFromZ(TraceVec).ToQuat();
	FColor DrawColor = bResult ? FColor::Green : FColor::Red;
	float DebugLifeTime = 0.5f;

	if (ViewDebugLine) {
		DrawDebugCapsule(
			GetWorld(),
			Center,
			HalfHeight,
			TraceRadius,
			CapsuleRot,
			DrawColor,
			false,
			DebugLifeTime
		);
	}
#endif
}

void UPlayerTrace::PlayerCounterAttack(FVector playerLocation, FVector playerForwardVector, AActor * playerActer)
{
	FHitResult HitResult;
	float TraceRadius = 15.0f;
	float StartRange = 20.0f;
	float CounterRange = 40.0f;
	FVector StartPosition = StartRange * playerForwardVector;
	FCollisionQueryParams Params(NAME_None, false, playerActer);
	bool bResult =
		GetWorld()->SweepSingleByChannel(
			HitResult,
			playerLocation + StartPosition,
			playerLocation + StartPosition + playerForwardVector * CounterRange,
			FQuat::Identity,
			ECollisionChannel::ECC_GameTraceChannel1,
			FCollisionShape::MakeSphere(TraceRadius),
			Params
		);
#if ENABLE_DRAW_DEBUG
	FVector TraceVec = playerForwardVector * CounterRange;
	FVector Center = playerLocation + TraceVec * 0.5f + StartPosition;
	float HalfHeight = CounterRange * 0.5f;
	FQuat CapsuleRot = FRotationMatrix::MakeFromZ(TraceVec).ToQuat();
	FColor DrawColor = bResult ? FColor::Green : FColor::Red;
	float DebugLifeTime = 0.5f;

	if (ViewDebugLine) {
		DrawDebugCapsule(
			GetWorld(),
			Center,
			HalfHeight,
			TraceRadius,
			CapsuleRot,
			DrawColor,
			false,
			DebugLifeTime
		);
	}
#endif
	if (HitResult.GetActor()) {
		AEnemyCharacter* EnemyCharacter = Cast< AEnemyCharacter>(HitResult.GetActor());
		if (IsValid(EnemyCharacter)) {
			EnemyCharacter->Hurt();
			EnemyCharacter->GetMesh()->AddForce(playerForwardVector*100.0f);
		}
	}
}

void UPlayerTrace::TraceCheckActor(FHitResult TraceActorHitResult, AActor * PlayerActor)
{
	if (IsValid(TraceActorHitResult.GetActor())) {
		if (TraceActorHitResult.GetActor()->ActorHasTag("PushObject")) {
			if (TraceClose) {
				TracePush = true;
			}
		}
		if (TraceActorHitResult.GetActor()->ActorHasTag("Climing")) {
			if (TraceClose) {
				if (!TraceClimming && TraceHeights && TraceForward && TraceClose) {
					auto Character = Cast<ACharacter>(PlayerActor);
					auto PlayerController = GetWorld()->GetFirstPlayerController();
					auto playerState = Cast< APlayerCharacterController>(PlayerController)->getPlayerState();
					if (playerState->getCurPlayerState() != E_PlayerState::Player_CLIMING)
					{
						FRotator playerRo = TraceActorHitResult.Normal.Rotation();
						Character->SetActorRelativeRotation(FRotator(Character->GetActorRotation().Pitch, playerRo.Yaw - 180.0f, Character->GetActorRotation().Roll));
						Character->SetActorRelativeLocation(FVector(TraceActorHitResult.Normal.X*30.0f + TraceActorHitResult.Location.X, TraceActorHitResult.Normal.Y*30.0f + TraceActorHitResult.Location.Y, TraceActorHitResult.Location.Z));
					}
				}
				TraceClimming = true;
			}
		}
	}
}

void UPlayerTrace::ThrowObject()
{
	ThrowValue = true;
}
