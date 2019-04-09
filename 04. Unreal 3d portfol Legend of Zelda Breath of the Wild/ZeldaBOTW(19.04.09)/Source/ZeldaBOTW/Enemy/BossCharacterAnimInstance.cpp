// Fill out your copyright notice in the Description page of Project Settings.

#include "BossCharacterAnimInstance.h"


UBossCharacterAnimInstance::UBossCharacterAnimInstance()
{

}

void UBossCharacterAnimInstance::NativeUpdateAnimation(float DeltaSeconds)
{
	Super::NativeUpdateAnimation(DeltaSeconds);

	auto Pawn = TryGetPawnOwner();
	if (::IsValid(Pawn)) {
		curSpeed = Pawn->GetVelocity().Size();
	}

}


