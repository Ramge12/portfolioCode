// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ZeldaBOTW.h"
#include "Enemy/BossCharacter.h"
#include "Animation/AnimInstance.h"
#include "BossCharacterAnimInstance.generated.h"

UCLASS()
class ZELDABOTW_API UBossCharacterAnimInstance : public UAnimInstance
{
	GENERATED_BODY()

private:
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Pawn, Meta = (AllowPrivateAccess = true))
	ABossCharacter* BossCharacter;
	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Pawn, Meta = (AllowPrivateAccess = true))
	float curSpeed;

public:
	UBossCharacterAnimInstance();

	virtual void NativeUpdateAnimation(float DeltaSeconds) override;
	
	
};
