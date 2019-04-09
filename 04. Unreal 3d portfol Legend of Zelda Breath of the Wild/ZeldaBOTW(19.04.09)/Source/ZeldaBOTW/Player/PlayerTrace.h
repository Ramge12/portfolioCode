// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ZeldaBOTW.h"
#include "Components/ActorComponent.h"
#include "PlayerTrace.generated.h"

UCLASS( ClassGroup=(Custom), meta=(BlueprintSpawnableComponent) )
class ZELDABOTW_API UPlayerTrace : public UActorComponent
{
	GENERATED_BODY()

private:
	bool ThrowValue = false;
	bool ViewDebugLine = false;
	bool TraceClose = false;
	bool TraceForward = false;
	bool TracePush = false;
	bool TraceClimming = false;
	bool TraceHeights = false;

protected:
	virtual void BeginPlay() override;
public:
	UPlayerTrace();

	void PlayerTraceCheck(FVector playerLocation, FVector playerForwardVector, AActor* playerActer);
	void PlayerForwardTrace(FVector playerLocation, FVector playerForwardVector, AActor* playerActer);
	void PlayerCloseForwardTrace(FVector playerLocation, FVector playerForwardVector, AActor* playerActer);
	void PlayerHeightTrace(FVector playerLocation, FVector playerForwardVector, AActor* playerActer);
	void PlayerCounterAttack(FVector playerLocation, FVector playerForwardVector, AActor* playerActer);

	void TraceCheckActor(FHitResult TraceActorHitResult, AActor* PlayerActor);
	void ThrowObject();

	bool getTraceClose() { return TraceClose; }
	bool getTraceForward() { return TraceForward; }
	bool getTracePush() { return TracePush; }
	bool getTraceClimming() { return TraceClimming; }
	bool getTraceHeight() { return TraceHeights; }
};
